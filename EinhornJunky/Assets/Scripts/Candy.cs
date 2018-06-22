using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour {

  public float SugarBoost = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    GameObject g = gameObject.transform.GetChild(0).gameObject;

    g.transform.localPosition = new Vector2(0,Mathf.Sin(Time.time)*0.2f);
    //https://forum.unity.com/threads/2d-pixel-art-particles-tutorial.361315/
  }
}
