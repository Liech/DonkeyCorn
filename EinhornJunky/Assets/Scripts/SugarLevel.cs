using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SugarStatus
{
  Depri, Wach, Overdrive
}

public class SugarLevel : MonoBehaviour {
  //0-1 Esel Müde
  //1-2 Esel Wach 
  //2-3 Esel Very Much Overdrive
  public float CurrentLevel;

  public float Depri_DecreasePerSecond = 0.05f;
  public float Wach_DecreasePerSecond = 0.05f;
  public float Overdrive_DecreasePerSecond = 0.05f;

  private HashSet<SugarLevelDependent> registry;

  public SugarLevel()
  {
    registry = new HashSet<SugarLevelDependent>();
    CurrentLevel = 0.5f;
  }

  public SugarStatus Status
  {
    get{
      if (CurrentLevel < 1) return SugarStatus.Depri;
      if (CurrentLevel > 2) return SugarStatus.Overdrive;
      return SugarStatus.Wach;
    }
  }

  // Use this for initialization
  void Start () {
    
	}
	
	// Update is called once per frame
	void Update () {
    if (Status == SugarStatus.Depri) { CurrentLevel -= Depri_DecreasePerSecond * Time.deltaTime; SetStatus(SugarStatus.Depri); }
    if (Status == SugarStatus.Wach) { CurrentLevel -= Wach_DecreasePerSecond * Time.deltaTime; SetStatus(SugarStatus.Wach); }
    if (Status == SugarStatus.Overdrive) { CurrentLevel -= Overdrive_DecreasePerSecond * Time.deltaTime; SetStatus(SugarStatus.Overdrive); }
    GameObject plr = GameObject.Find("Player");
    if (plr != null)
    {
      if (CurrentLevel < 0 || CurrentLevel > 3|| plr.transform.position.y < GameObject.Find("Abgrund").transform.position.y) {
        Destroy(plr.gameObject);
        GameObject.Find("Canvas/LooseScreen").GetComponent<Image>().enabled = true;
      }
    }
    GetComponent<ShowLife>().CurrentDamage = CurrentLevel * GetComponent<ShowLife>().MaxLife / 3.0f;
  }

  void SetStatus(SugarStatus status)
  {
    foreach (SugarLevelDependent s in registry)
      s.CurrentLevel = status;
  }

  public void Register(SugarLevelDependent s)
  {
    registry.Add(s);
  }

  public void Unregister(SugarLevelDependent s)
  {
    registry.Remove(s);
  }
}
