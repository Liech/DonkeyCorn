using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateOnStart : MonoBehaviour {
  public GameObject obj;
  bool init = false;
	// Use this for initialization
	void Awake () {
    if (init) return;
    init = true;
    GameObject O = Instantiate(obj);
    O.name = obj.name;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
