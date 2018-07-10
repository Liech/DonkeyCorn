using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevCandy : MonoBehaviour {

	// Use this for initialization
	void Start ()
  {
    GameObject.Find("Canvas/SugarLevel").GetComponent<SugarLevel>().CurrentLevel = 1.5f;
    GameObject.Find("Canvas/SugarLevel").GetComponent<SugarLevel>().Wach_DecreasePerSecond = 0;
  }
	
	// Update is called once per frame
	void Update () {
		
	}
}
