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

  public float StunDuration = 0.5f;
  public float WalkTimeForHighJump = 0.5f;
  public float HighJumpFactor = 1.2f;
  public float CandyCollectRadius = 1;

  public GameObject OnStunSound;
  public GameObject OnJumpSound;
  public GameObject OnCandySound;
  public GameObject OnFallSound;

  private float StunnedUntil = 0;
  private float WalkSince = 0;

  public bool Dead { get; set; }

  // Use this for initialization
  void Start () {
    Dead = false;
	}

  public bool Glitch
  {
    get
    {
      return IsStunned;
    }
  }
   
  void CandyCollected(GameObject g)
  {

    Instantiate(OnCandySound).GetComponent<AudioSource>().Play();
    SugarLevel s = GameObject.Find("Canvas/SugarLevel").GetComponent<SugarLevel>();
    s.addSugar(g.GetComponent<Candy>().SugarBoost);


    Destroy(g);
  }

  // Update is called once per frame
  void Update () {
    if (Dead)
    {
      transform.GetChild(0).GetComponent<Animator>().SetBool("Dying", true);
      return;
    }

    SugarStatus sugar = GetComponent<SugarLevelDependent>().CurrentLevel;

    if (sugar != SugarStatus.Depri)
      transform.GetChild(0).GetComponent<Animator>().SetBool("Unicorn", true);
    else
      transform.GetChild(0).GetComponent<Animator>().SetBool("Unicorn", false);

    Movement();
    CollectCandy();

    transform.GetChild(1).gameObject.SetActive(sugar != SugarStatus.Depri && WalkSince != 0 && WalkTimeForHighJump < Time.time - WalkSince);
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

    float factor = 1;
    if (WalkSince != 0 && WalkTimeForHighJump < Time.time- WalkSince)
      factor = HighJumpFactor;
    SugarStatus sugar = GetComponent<SugarLevelDependent>().CurrentLevel;
    if (sugar == SugarStatus.Depri)
      return Depri_JumpForce*factor;
    else if (sugar == SugarStatus.Wach)
      return Wach_JumpForce * factor;
    else if (sugar == SugarStatus.Overdrive)
      return Overdrive_JumpForce * factor;
    return 0;
  }


  public void CollectCandy()
  {
    AnimatorStateInfo state = transform.GetChild(0).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
    SugarStatus sugar = GetComponent<SugarLevelDependent>().CurrentLevel;
    if (sugar == SugarStatus.Depri)
      if (!state.IsName("eating"))
        return;

    //Collider2D  Physics2D.OverlapSphere2D(Vector3 position, float radius, int layerMask = AllLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal);
    Collider2D[] c = Physics2D.OverlapCircleAll(transform.position,CandyCollectRadius );
    foreach(Collider2D cc in c)
    {
      if (cc.gameObject.tag == "Candy")
      {
        transform.GetChild(0).GetComponent<Animator>().SetTrigger("StopEating");
        CandyCollected(cc.gameObject);
      }
    }

    //CollisionList l = gameObject.GetComponent<CollisionList>();
    //for(int i = 0;i < l.currentCollisions.Count;i++)
    //{
    //  if (l.currentCollisions[i].tag == "Candy")
    //  {
    //    CandyCollected(l.currentCollisions[i]);
    //  }
    //}
  }

  float lastFump = 0;

  enum walkdir { l, r, n };
  public void Movement()
  {
    walkdir currentWalkDir = walkdir.n;
    bool wantjump = false;
    foreach (Touch touch in Input.touches)
    {
      if (touch.position.y > Screen.height - Screen.height / 6) continue;
      if (touch.position.y > (Screen.height) / 3.0f)
      { 
        if (touch.position.x < Screen.width / 2)
        {
          currentWalkDir = walkdir.l;
        }
        else
        {
          currentWalkDir = walkdir.r;
        }
      }
      else
      {
        wantjump = true;
      }
    }


      EinhornPhysic p = gameObject.GetComponent<EinhornPhysic>();
    Vector2 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

    SugarStatus sugar = GetComponent<SugarLevelDependent>().CurrentLevel;

    if (!IsStunned)
    {
      if ((currentWalkDir == walkdir.l||Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && !p.LeftCol)
      {
        transform.GetChild(0).GetComponent<Animator>().SetFloat("Speed", 1);
        p.VX = -getWalkSpeed();
        transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
        if (WalkSince == 0) WalkSince = Time.time;
      }
      else if ((currentWalkDir == walkdir.r|| Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && !p.RightCol)
      {
        transform.GetChild(0).GetComponent<Animator>().SetFloat("Speed", 1);
        p.VX = getWalkSpeed();
        transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
        if (WalkSince == 0) WalkSince = Time.time;
      }
      else
      {
        p.VX = 0;
        transform.GetChild(0).GetComponent<Animator>().SetFloat("Speed", 0);
        WalkSince = 0;
      }
      if (Input.GetKey(KeyCode.Space) || sugar == SugarStatus.Overdrive || wantjump)
        Jump();
    }
    if (!p.IsGrounded && sugar != SugarStatus.Depri)
      transform.GetChild(0).GetComponent<Animator>().SetBool("Jump", true);
    else
    transform.GetChild(0).GetComponent<Animator>().SetBool("Jump", false);
    //if (p.VY <= 0)
    //p.VY += -1f;

  }



  public void Jump()
  {
    EinhornPhysic p = gameObject.GetComponent<EinhornPhysic>();
    Vector2 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
    SugarStatus sugar = GetComponent<SugarLevelDependent>().CurrentLevel;
    if (p.IsGrounded)
    {
      p.VY = getJumpForce();
      if (sugar != SugarStatus.Depri) Instantiate(OnJumpSound).GetComponent<AudioSource>().Play();
      if (sugar == SugarStatus.Depri)
      {
        if (lastFump + 0.5f < Time.time)
          Instantiate(OnFallSound).GetComponent<AudioSource>().Play();
        lastFump = Time.time;
        transform.GetChild(0).GetComponent<Animator>().SetTrigger("eating");
      }
    }
  }

  public bool IsStunned
  {
    get{ return Time.time < StunnedUntil; }
  }

  //float oldTime = 0;
  public void Stun()
  {
    if (StunnedUntil > Time.time) return;
    StunnedUntil = Time.time + StunDuration;
    Instantiate(OnStunSound).GetComponent<AudioSource>().Play();
    //gameObject.AddComponent<ActionEffect>();
    //Debug.Log("Hit");
    //Time.timeScale = 0.00001f;
    //Color old = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color;
    //transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
    //yield WaitForSeconds(.5);
    //transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = old;
    //Time.timeScale = oldTime;
  }

}
