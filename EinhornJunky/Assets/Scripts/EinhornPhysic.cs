using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EinhornPhysic : MonoBehaviour {
  //public float Gravity = 1;
  //public float JumpDuration = 0.3f;
  //public float JumpForce = 0;

  public Vector2 Velocity; 

  public Rigidbody2D body
  {
    get
    {
      return gameObject.GetComponent<Rigidbody2D>();
    }
  }

  public float VX
  {
    get
    {
      return body.velocity.x;
    }
    set
    {
      body.velocity = new Vector2(value, body.velocity.y);
    }
  }
  public float VY
  {
    get
    {
      return body.velocity.y;
    }
    set
    {
      body.velocity = new Vector2(body.velocity.x, value);
    }
  }

  public bool IsGrounded
  {
    get
    {
      Vector3 position = transform.position;
      position.y = GetComponent<Collider2D>().bounds.min.y - 0.1f;
      float length = 0.1f - 0.1f;
      Debug.DrawRay(position, Vector3.down * length);
      bool grounded = Physics2D.Raycast(position, Vector3.down, length, ~0);
      return grounded;
    }
  }

  //public void Jump()
  //{
  //  Velocity = new Vector2(0,-JumpForce);

  //}

  // Use this for initialization
  void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//if (!IsGrounded)
  //  {
  //    Velocity = Velocity - new Vector2(0,Gravity);
  //  }

  //  Vector2 velTodo = Velocity;
  //  float stepsize = 0.1f;
  //  while (velTodo.magnitude > 0)
  //  {
  //    transform.position = transform.position + new Vector3(0.1f, 0.1f, 0);
  //    //velTodo-= 
    //}

  }
}
