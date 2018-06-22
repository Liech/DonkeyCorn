using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugarLevelDependent : MonoBehaviour {

  public void Start()
  {
    CurrentLevel = SugarStatus.Depri;
    GameObject g = GameObject.Find("Canvas/SugarLevel");
    g.GetComponent<SugarLevel>().Register(this);
  }

  public SugarStatus CurrentLevel
  {
    get;
    set;
  }

  void OnDestroy()
  {
    GameObject g = GameObject.Find("Canvas/SugarLevel");
    if (g == null) return;
    g.GetComponent<SugarLevel>().Unregister(this);
  }
}
