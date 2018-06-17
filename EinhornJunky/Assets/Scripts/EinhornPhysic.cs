using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EinhornPhysic : MonoBehaviour {
  //public float Gravity = 1;
  //public float JumpDuration = 0.3f;
  //public float JumpForce = 0;

  public Vector2 Velocity;
  public LayerMask mask = -1;
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

  public bool LeftCol
  {
    get
    {
      Collider2D c = GetComponent<Collider2D>();
      Vector3 position = c.bounds.min;
      position.x = c.bounds.min.x - 0.1f;
      float length = 0;
      RaycastHit2D grounded = Physics2D.Raycast(position, Vector3.left, length, mask);
      return grounded.collider != null;
    }
  }

  public bool RightCol
  {
    get
    {
      Collider2D c = GetComponent<Collider2D>();
      Vector3 position = new Vector2(c.bounds.max.x,c.bounds.min.y);
      position.x = c.bounds.max.x + 0.1f;
      float length = 0.0f;
      RaycastHit2D grounded = Physics2D.Raycast(position, Vector3.right, length, mask);
      Debug.Log(grounded.collider != null);
      return grounded.collider != null;
    }
  }

  public bool IsGrounded
  {
    get
    {
      Vector3 position = transform.position;
      Collider2D c = GetComponent<Collider2D>();
      position.y = c.bounds.min.y - 0.1f;
      float length = 0.1f - 0.1f;
      Debug.DrawRay(position, Vector3.down * length);
      float from  = c.bounds.min.x;
      float width = c.bounds.max.x - from;

      RaycastHit2D grounded = Physics2D.Raycast(c.bounds.min+new Vector3(0,-0.1f), Vector3.down, length, mask);
      if (grounded.collider != null)
      {
        if (grounded.collider.gameObject.tag != "Candy" &&
          grounded.collider.gameObject.name != "Player"
          ) return true;
      }
      grounded = Physics2D.Raycast(new Vector3(c.bounds.max.x, c.bounds.min.y-0.1f), Vector3.down, length, mask);
      if (grounded.collider != null)
      {
        if (grounded.collider.gameObject.tag != "Candy" &&
          grounded.collider.gameObject.name != "Player"
          ) return true;
      }
      grounded = Physics2D.Raycast(new Vector3(c.bounds.center.x, c.bounds.min.y-0.1f), Vector3.down, length, mask);
      if (grounded.collider != null)
      {
        if (grounded.collider.gameObject.tag != "Candy" &&
          grounded.collider.gameObject.name != "Player"
          ) return true;
      }



      return false;

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
