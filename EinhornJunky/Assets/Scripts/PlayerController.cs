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

    if (p.IsGrounded){
      if (Input.GetKey(KeyCode.A))
      {
        p.VX = -WalkSpeed;
      }
      else if (Input.GetKey(KeyCode.D))
      {
        p.VX = WalkSpeed;
      }
      else p.VX = 0;

      if (Input.GetKey(KeyCode.Space))
        p.VY = JumpForce;
    }

    Camera.main.transform.position = Camera.main.transform.position + new Vector3(transform.position.x- Camera.main.transform.position.x, 0,0);
  }
}
