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
  public float CurrentLevel
  {
    get
    {
      return currentLevel;
    }
    set
    {
      currentLevel = value;
      changedSinceStart = true;
    }
  }
  float currentLevel;
  bool changedSinceStart = false;
  public float startSugar;


  public float Depri_DecreasePerSecond = 0.05f;
  public float Wach_DecreasePerSecond = 0.05f;
  public float Overdrive_DecreasePerSecond = 0.05f;

  private HashSet<SugarLevelDependent> registry;

  public SugarLevel()
  {
    registry = new HashSet<SugarLevelDependent>();
    CurrentLevel = startSugar;
    changedSinceStart = false;
  }

  public SugarStatus Status
  {
    get{
      if (GameObject.Find("Player") != null)
        if (GameObject.Find("Player").GetComponent<PlayerController>().Glitch) return SugarStatus.Depri;
      if (GameObject.Find("Canvas/LooseScreen").GetComponent<Image>().enabled)
      {
        CurrentLevel = 0;
        return SugarStatus.Depri;
      }
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
    if (changedSinceStart)
    {
      if (Status == SugarStatus.Depri) { CurrentLevel -= Depri_DecreasePerSecond * Time.deltaTime; SetStatus(SugarStatus.Depri); }
      if (Status == SugarStatus.Wach) { CurrentLevel -= Wach_DecreasePerSecond * Time.deltaTime; SetStatus(SugarStatus.Wach); }
      if (Status == SugarStatus.Overdrive) { CurrentLevel -= Overdrive_DecreasePerSecond * Time.deltaTime; SetStatus(SugarStatus.Overdrive); }
    }
    GameObject plr = GameObject.Find("Player");
    if (plr != null)
    {
      if (CurrentLevel < 0 || CurrentLevel > 3|| plr.transform.position.y < GameObject.Find("Abgrund").transform.position.y) {
        plr.GetComponent<PlayerController>().Dead = true;
        GameObject.Find("Canvas/LooseScreen").GetComponent<Image>().enabled = true;
      }
    }
    GetComponent<ShowLife>().CurrentDamage = CurrentLevel * GetComponent<ShowLife>().MaxLife / 3.0f + 0.5f;
    if (CurrentLevel <= 0) GetComponent<ShowLife>().CurrentDamage = 0; 
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
