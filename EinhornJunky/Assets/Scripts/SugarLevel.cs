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
      return targetLevel;
    }
    set
    {
      targetLevel = value;
    }
  }

  public float effectLevel
  {
    get
    {
      return currentLevel;
    }
  }

  public void addSugar(float amount)
  {
    changedSinceStart = true;
    if (targetLevel + amount > 1 && targetLevel < 1)
      targetLevel = 1.5f;
    else
    if (targetLevel + amount > 2 && targetLevel < 2)
      targetLevel = 2.5f;
    else
      targetLevel += amount;
  }

  public float targetLevel;
  public float currentLevel;
  bool changedSinceStart = false;
  public float startSugar;


  public float Depri_DecreasePerSecond = 0.05f;
  public float Wach_DecreasePerSecond = 0.05f;
  public float Overdrive_DecreasePerSecond = 0.05f;

  private HashSet<SugarLevelDependent> registry;

  public SugarLevel()
  {
    registry = new HashSet<SugarLevelDependent>();
    currentLevel = 0.5f;
    targetLevel = currentLevel;
    changedSinceStart = false;
  }

  public bool hasEatenOnce()
  {
    return changedSinceStart;
  }

  public SugarStatus Status
  {
    get{
      if (GameObject.Find("Player") != null)
        if (GameObject.Find("Player").GetComponent<PlayerController>().Glitch) return SugarStatus.Depri;
      if (GameObject.Find("Canvas/LooseScreen"))
      {
        targetLevel = 0;
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
	void FixedUpdate () {
    float maxSpeed = 0.015f;
    float dir = (targetLevel - currentLevel);
    if (dir > maxSpeed) dir = maxSpeed;
    if (dir < -maxSpeed) dir =- maxSpeed;
    currentLevel = targetLevel;// currentLevel +  dir;

    GameObject plr = GameObject.Find("Player");
    if (changedSinceStart && !plr.GetComponent<PlayerController>().Dead)
    {
      if (Status == SugarStatus.Depri) { targetLevel -= Depri_DecreasePerSecond * Time.deltaTime; SetStatus(SugarStatus.Depri); }
      if (Status == SugarStatus.Wach) { targetLevel -= Wach_DecreasePerSecond * Time.deltaTime; SetStatus(SugarStatus.Wach); }
      if (Status == SugarStatus.Overdrive) { targetLevel -= Overdrive_DecreasePerSecond * Time.deltaTime; SetStatus(SugarStatus.Overdrive); }
    }
    if (plr.GetComponent<PlayerController>().Dead)
    {
      targetLevel = targetLevel * 0.99f;
    }
    if (plr != null)
    {
      DeadHandle deathHandler = GameObject.Find("DeadHandler").GetComponent<DeadHandle>();
      if (CurrentLevel < 0)
        deathHandler.Dead(DeathReason.Undersugar);
      else if (CurrentLevel > 3)
        deathHandler.Dead(DeathReason.OverSugar);
      else if (plr.transform.position.y < GameObject.Find("Abgrund").transform.position.y)
        deathHandler.Dead(DeathReason.Fall);
      
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
