using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICornDead : MonoBehaviour {
  public Sprite Alive;
  public Sprite Dead;

  public float deadDist = 4;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
  {
    float resolutionFactor = 1;
    float dist = (transform.parent.GetChild(2).position - transform.position).magnitude;
    if (dist < deadDist * resolutionFactor)
      GetComponent<Image>().sprite = Dead;
    else
      GetComponent<Image>().sprite = Alive;
	}
}
