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
  public float         WalkSpeed;
  public bool          KillableByJump;

  private Rigidbody2D body;
  private SpriteRenderer rnd;
  private CollisionSide side;

	// Use this for initialization
	void Start () {
    body = transform.GetComponent<Rigidbody2D>();
    rnd = transform.GetChild(0).GetComponent<SpriteRenderer>();
    side = GetComponent<CollisionSide>();
	}
	


	// Update is called once per frame
	void Update () {

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
    if (side.CollideFromTop && KillableByJump) Destroy(gameObject);
  }
}
