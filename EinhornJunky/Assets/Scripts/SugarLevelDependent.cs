using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugarLevelDependent : MonoBehaviour {

  public void Start()
  {
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
    g.GetComponent<SugarLevel>().Unregister(this);
  }
}
