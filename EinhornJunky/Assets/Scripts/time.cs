using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class time : MonoBehaviour {

  float startTime;
	// Use this for initialization
	void Start () {
    startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
    if (GameObject.Find("Canvas/WinScreen").GetComponent<Image>().enabled) return;
    if (GameObject.Find("Canvas/LooseScreen").GetComponent<Image>().enabled) return;
    float t = Time.time - startTime;
    int minutes = (int)(t / 60.0f);
    t -= minutes * 60;
    int seconds = (int)(t);
    t -= seconds;
    int thenthseconds = (int)(t * 100);


    GetComponent<Text>().text = minutes.ToString() + ":" + seconds + ":" + thenthseconds;
	}
}
