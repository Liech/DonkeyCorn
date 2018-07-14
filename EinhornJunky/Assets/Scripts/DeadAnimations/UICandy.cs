using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICandy : MonoBehaviour {

  Rigidbody2D r;
  public float Velocity;
  public float angularVelocity;
  public List<Sprite> Sprites;

  // Use this for initialization
  void Start () {
    r = GetComponent<Rigidbody2D>();
    r.angularVelocity = (Random.value * angularVelocity) - angularVelocity/2;
    r.velocity = new Vector2(Random.value * Velocity - Velocity/2, Random.value * Velocity - Velocity/2);
    GetComponent<Image>().sprite = Sprites[Random.Range(0, Sprites.Count)];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
