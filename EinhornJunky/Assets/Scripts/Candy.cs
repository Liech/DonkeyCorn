using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    GameObject g = gameObject.transform.GetChild(0).gameObject;

    g.transform.localPosition = new Vector2(0,Mathf.Sin(Time.time)*0.2f);
	}
}
