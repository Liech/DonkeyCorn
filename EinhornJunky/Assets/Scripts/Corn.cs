using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corn : MonoBehaviour {
  public bool CanDrop = true;



	// Use this for initialization
	void Start () {
		
	}

  bool released = false;

	// Update is called once per frame
	void Update () {
    GameObject Player = GameObject.Find("Player");
    if (Player == null) return;
    BoxCollider2D col = GetComponent<BoxCollider2D>();

    if (CanDrop)
    {
      if (Player.transform.position.y < transform.position.y && Player.transform.position.x < col.bounds.max.x
        && Player.transform.position.x > col.bounds.min.x && !released)
      {
        gameObject.AddComponent<Rigidbody2D>();
        released = true;
      }
    }

    CollisionList cl = GetComponent<CollisionList>();
    if (cl.currentCollisions.Count > 0)
    {
      foreach(GameObject c in cl.currentCollisions)
      {
        if (c == Player)
        {
          GameObject.Find("Canvas/SugarLevel").GetComponent<SugarLevel>().CurrentLevel = 3.5f;
        }
      }
      Destroy(gameObject);
    }

	}
}
