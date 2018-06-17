using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
  Left, Right, None
}

public class Enemy : MonoBehaviour {
  public Direction     WalkDirection;
  public bool          WalkOfCliffs;
  public bool          KillableByJump;
  public float         DepriSpeed;
  public float WachSpeed;
  public float OverdriveSpeed;
  public float BounceForce = 20;
  public bool SugarRushOnCollision;
  public GameObject Leiche;
  public float OnTopBounceForce = 15;

  private Rigidbody2D body;
  private SpriteRenderer rnd;
  private CollisionSide side;
  private SugarStatus sugar;

	// Use this for initialization
	void Start () {
    body = transform.GetComponent<Rigidbody2D>();
    rnd = transform.GetChild(0).GetComponent<SpriteRenderer>();
    side = GetComponent<CollisionSide>();
	}
	
	// Update is called once per frame
	void Update () {
    sugar = GetComponent<SugarLevelDependent>().CurrentLevel;    
    float WalkSpeed = 0;
    if (sugar == SugarStatus.Depri)
    {
      WalkSpeed = DepriSpeed;
      transform.GetChild(0).GetComponent<Animator>().SetBool("Sugar", false);
    }
    else if (sugar == SugarStatus.Wach)
    {
      WalkSpeed = WachSpeed;
      transform.GetChild(0).GetComponent<Animator>().SetBool("Sugar", true);
    }
    else if (sugar == SugarStatus.Overdrive)
    {
      WalkSpeed = OverdriveSpeed;
      transform.GetChild(0).GetComponent<Animator>().SetBool("Sugar", true);
    }



    if (WalkDirection == Direction.Left)
    {
      body.velocity = new Vector2(-WalkSpeed, body.velocity.y);
      rnd.flipX = false;
    }
    if (WalkDirection == Direction.Right)
    {
      body.velocity = new Vector2(WalkSpeed, body.velocity.y);
      rnd.flipX = true;
    }


    BoxCollider2D r = GetComponent<BoxCollider2D>();
    RaycastHit2D left = Physics2D.Raycast(new Vector2(r.bounds.min.x - 2, r.bounds.center.y), new Vector2(0, -1), 3f);
    RaycastHit2D right = Physics2D.Raycast(new Vector2(r.bounds.max.x + 2, r.bounds.center.y), new Vector2(0, -1), 3f);

    if (side.CollideFromLeft ||left.collider == null) {
      WalkDirection = Direction.Right;
      foreach(GameObject g in side.colFromLeft)
        if (g.tag == "Player")
          Collide(g);
    }
    if (side.CollideFromRight || right.collider == null) {
      WalkDirection = Direction.Left;
      foreach (GameObject g in side.colFromLeft)
        if (g.tag == "Player")
          Collide(g);
    }
    if (side.CollideFromTop && !KillableByJump)
    {
      foreach (GameObject g in side.colFromTop)
        if (g.tag == "Player")
          Collide(g);
    }
    if (side.CollideFromTop && KillableByJump && sugar != SugarStatus.Depri)
    {
      Kill(side.colFromTop);
    }
    else
    {
      foreach (GameObject g in side.colFromLeft)
        Collide(g);
      foreach (GameObject g in side.colFromRight)
        Collide(g);
    }

  }

  public void Kill(HashSet<GameObject> killer)
  {
    foreach (GameObject fg in killer)
      if (fg.tag == "Player")
        fg.GetComponent<Rigidbody2D>().velocity = new Vector2(fg.GetComponent<Rigidbody2D>().velocity.x, OnTopBounceForce);
    if (Leiche == null) return;
    GameObject g = Instantiate(Leiche);
    g.transform.position = transform.position;
    Destroy(gameObject);
  }


  float lastCollision;
  public void Collide(GameObject g)
  {
    if (g.tag == "Player" && lastCollision < Time.time + 0.5f)
    {
      lastCollision = Time.time;
      SugarLevel sl = GameObject.Find("Canvas/SugarLevel").GetComponent<SugarLevel>();
      g.GetComponent<Rigidbody2D>().velocity = new Vector3(g.transform.position.x < transform.position.x?-BounceForce: BounceForce, BounceForce);
      g.GetComponent<PlayerController>().Stun();
      if (SugarRushOnCollision)
        if(sl.CurrentLevel < 2)
          sl.CurrentLevel = 2.5f;
        else
          sl.CurrentLevel = 3.5f; //tot

    }
  }

  public void OnDrawGizmos()
  {
    BoxCollider2D r = GetComponent<BoxCollider2D>();
    RaycastHit2D left = Physics2D.Raycast(new Vector2(r.bounds.min.x - 2, r.bounds.center.y), new Vector2(0, -1), 3f);
    RaycastHit2D right = Physics2D.Raycast(new Vector2(r.bounds.max.x + 2, r.bounds.center.y), new Vector2(0, -1), 3f);
    Gizmos.DrawCube(left.point, new Vector3(0.1f, 0.1f, 0.1f));
    Gizmos.DrawCube(right.point, new Vector3(0.1f, 0.1f, 0.1f));
  }
}
