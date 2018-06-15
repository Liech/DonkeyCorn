using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
  public float WalkSpeed = 1;
  public float JumpForce = 5;

	// Use this for initialization
	void Start () {
    
	}
   
  void CandyCollected()
  {

  }

  // Update is called once per frame
  void Update () {
    Movement();
    CollectCandy();
  }




  public void CollectCandy()
  {
    CollisionList l = gameObject.GetComponent<CollisionList>();
    for(int i = 0;i < l.currentCollisions.Count;i++)
    {
      if (l.currentCollisions[i].tag == "Candy")
      {
        CandyCollected();
        Destroy(l.currentCollisions[i]);
      }
    }
  }

  public void Movement()
  {
    EinhornPhysic p = gameObject.GetComponent<EinhornPhysic>();
    Vector2 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

    if (Input.GetKey(KeyCode.A))
    {
      p.VX = -WalkSpeed;
      transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
    }
    else if (Input.GetKey(KeyCode.D))
    {
      p.VX = WalkSpeed;
      transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
    }
    else p.VX = 0;

    if (p.IsGrounded)
    {
      if (Input.GetKey(KeyCode.Space))
        p.VY = JumpForce;
    }

    //if (p.VY <= 0)
    //p.VY += -1f;

    Camera.main.transform.position = Camera.main.transform.position + new Vector3(transform.position.x - Camera.main.transform.position.x, 0, 0);
  }
}
