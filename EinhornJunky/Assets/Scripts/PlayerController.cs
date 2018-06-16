using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
  public float Depri_WalkSpeed = 0.3f;
  public float Depri_JumpForce = 0;

  public float Wach_WalkSpeed = 3;
  public float Wach_JumpForce = 5;

  public float Overdrive_WalkSpeed = 10;
  public float Overdrive_JumpForce = 30;

  // Use this for initialization
  void Start () {
    
	}
   
  void CandyCollected(GameObject g)
  {
    GameObject.Find("Canvas/SugarLevel").GetComponent<SugarLevel>().CurrentLevel += g.GetComponent<Candy>().SugarBoost;
    Destroy(g);
  }

  // Update is called once per frame
  void Update () {
    Movement();
    CollectCandy();
  }

  float getWalkSpeed()
  {
    SugarStatus sugar = GetComponent<SugarLevelDependent>().CurrentLevel;
    if (sugar == SugarStatus.Depri)
      return Depri_WalkSpeed;
    else if (sugar == SugarStatus.Wach)
      return Wach_WalkSpeed;
    else if (sugar == SugarStatus.Overdrive)
      return Overdrive_WalkSpeed;
    return 0;
  }

  float getJumpForce()
  {
    SugarStatus sugar = GetComponent<SugarLevelDependent>().CurrentLevel;
    if (sugar == SugarStatus.Depri)
      return Depri_JumpForce;
    else if (sugar == SugarStatus.Wach)
      return Wach_JumpForce;
    else if (sugar == SugarStatus.Overdrive)
      return Overdrive_JumpForce;
    return 0;
  }


  public void CollectCandy()
  {
    CollisionList l = gameObject.GetComponent<CollisionList>();
    for(int i = 0;i < l.currentCollisions.Count;i++)
    {
      if (l.currentCollisions[i].tag == "Candy")
      {
        CandyCollected(l.currentCollisions[i]);
      }
    }
  }

  public void Movement()
  {
    EinhornPhysic p = gameObject.GetComponent<EinhornPhysic>();
    Vector2 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

    SugarStatus sugar = GetComponent<SugarLevelDependent>().CurrentLevel;


    if (Input.GetKey(KeyCode.A))
    {
      p.VX = -getWalkSpeed();
      transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
    }
    else if (Input.GetKey(KeyCode.D))
    {
      p.VX = getWalkSpeed();
      transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
    }
    else p.VX = 0;

    if (p.IsGrounded)
    {
      if (Input.GetKey(KeyCode.Space) || sugar == SugarStatus.Overdrive)
        p.VY = getJumpForce();
    }

    //if (p.VY <= 0)
    //p.VY += -1f;

    Camera.main.transform.position = Camera.main.transform.position + new Vector3(transform.position.x - Camera.main.transform.position.x, 0, 0);
  }
}
