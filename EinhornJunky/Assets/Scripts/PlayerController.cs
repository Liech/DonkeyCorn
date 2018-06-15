using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
  public float WalkSpeed = 1;
  public float JumpForce = 5;

	// Use this for initialization
	void Start () {
    
	}
   
  // Update is called once per frame
  void Update () {
    EinhornPhysic p = gameObject.GetComponent<EinhornPhysic>();
    Vector2 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

      if (Input.GetKey(KeyCode.A))
      {
        p.VX = -WalkSpeed;
        transform.GetChild(0).GetComponent<Animator>().Flip = false;
    }
    else if (Input.GetKey(KeyCode.D))
      {
        p.VX = WalkSpeed;
        transform.GetChild(0).GetComponent<Animator>().Flip = true;
      }
      else p.VX = 0;

    if (p.IsGrounded)
    {
      if (Input.GetKey(KeyCode.Space))
        p.VY = JumpForce;
    }

    //if (p.VY <= 0)
      //p.VY += -1f;

    Camera.main.transform.position = Camera.main.transform.position + new Vector3(transform.position.x- Camera.main.transform.position.x, 0,0);
  }
}
