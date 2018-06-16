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
      WalkSpeed = DepriSpeed;
    else if (sugar == SugarStatus.Wach)
      WalkSpeed = WachSpeed;
    else if (sugar == SugarStatus.Overdrive)
      WalkSpeed = OverdriveSpeed;

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

    if (side.CollideFromLeft) WalkDirection = Direction.Right;
    if (side.CollideFromRight) WalkDirection = Direction.Left;
    if (side.CollideFromTop && KillableByJump && sugar != SugarStatus.Depri) Destroy(gameObject);
  }
}
