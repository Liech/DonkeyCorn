using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

  public float JumpForce = 50;
  public float WalkingSpeed = 5;

  private float distanceToGround;

	// Use this for initialization
	void Start () {
    distanceToGround = GetComponent<BoxCollider2D>().bounds.extents.y;
	}

  public float isGroundedRayLength = 0.1f;
  //public LayerMask layerMaskForGrounded;
  public bool isGrounded
  {
    get
    {
      Vector3 position = transform.position;
      position.y = GetComponent<Collider2D>().bounds.min.y - 0.1f;
      float length = isGroundedRayLength - 0.1f;
      Debug.DrawRay(position, Vector3.down * length);
      bool grounded = Physics2D.Raycast(position, Vector3.down, length, ~0);
      return grounded;
    }
  }

  public void Jump()
  {
    Rigidbody2D body = gameObject.GetComponent<Rigidbody2D>();
    body.velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, 0);
    body.AddForce(new Vector2(0,JumpForce));
  }

  // Update is called once per frame
  void FixedUpdate () {
    if (isGrounded)
      Jump();

    var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
    Rigidbody2D body = gameObject.GetComponent<Rigidbody2D>();
    if (isGrounded)
      body.AddForce(new Vector2(move.x * WalkingSpeed, 0));

  }
}
