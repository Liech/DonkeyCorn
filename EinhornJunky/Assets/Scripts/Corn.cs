using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corn : MonoBehaviour {
  public bool CanDrop = true;
  public GameObject OnKillSound;

  public float Gravity = 1;

	// Use this for initialization
	void Start () {
		
	}

  bool released = false;

  bool useless = false;
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
        gameObject.GetComponent<Rigidbody2D>().gravityScale = Gravity;
        released = true;
      }
    }

    CollisionList cl = GetComponent<CollisionList>();
    if (cl.currentCollisions.Count > 0 && !useless)
    {
      foreach(GameObject c in cl.currentCollisions)
      {
        if (c == Player && !useless)
        {
          Instantiate(OnKillSound).GetComponent<AudioSource>().Play();
          GameObject.Find("DeadHandler").GetComponent<DeadHandle>().Dead(DeathReason.CandyCorn);
          useless = true;
        }
      }
      if (!useless)
        Destroy(gameObject);
    }

  }
}
