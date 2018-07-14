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
    if (GameObject.Find("Canvas/LooseScreen")) return;
    float t = Time.time - startTime;
    int minutes = (int)(t / 60.0f);
    t -= minutes * 60;
    int seconds = (int)(t);
    t -= seconds;
    int thenthseconds = (int)(t * 100);

    //% angabe
    float start = GameObject.Find("Start").transform.position.x;
    float end = GameObject.Find("End").transform.position.x;
    float plr = GameObject.Find("Player").transform.position.x;

    float perc = (plr - start) / (end - start);

    GetComponent<Text>().text = ((int)(perc* 100)).ToString() + "%   " + minutes.ToString() + ":" + seconds + ":" + thenthseconds;
	}
}
