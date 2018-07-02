using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VanishOnEat : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    if (GameObject.Find("Canvas/SugarLevel").GetComponent<SugarLevel>().hasEatenOnce())
      GetComponent<Text>().enabled = false;
	}
}
