using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum FadeOutStatus
{
  Visible, FadeOut, Invisible, FadeIn
}

public class Plattform : MonoBehaviour {

  public bool MovesRight = false;
  public bool MovesLeft = false;
  public bool FadesOnCollision = false;
  public float Speed = 1;
  public float MoveRange = 1;
  public float fadeOutTime = 1;
  public float fadedOutTime = 4;
  public float fadeInTime = 4;
  public bool  snapBack=false;
  public float snapBackModulo = 1.57f;

  GameObject plt;
  CollisionList col;
  SpriteRenderer rnd;

  HashSet<GameObject> OnCloud;

  private float NextFadeStatusChange;
  private float StatusChangeStart;
  FadeOutStatus CurrentFadeStatus;

  Plattform()
  {
    OnCloud = new HashSet<GameObject>();
  }

  // Use this for initialization
  void Start () {
    plt = transform.GetChild(0).gameObject;
    col = plt.GetComponent<CollisionList>();
    rnd = plt.transform.GetChild(0).GetComponent<SpriteRenderer>();
    CurrentFadeStatus = FadeOutStatus.Visible;
	}
	
	// Update is called once per frame
	void Update () {

    if (CurrentFadeStatus == FadeOutStatus.Visible || CurrentFadeStatus == FadeOutStatus.FadeOut)
    foreach (GameObject g in col.currentCollisions)
    {
        if (g.GetComponent<Rigidbody2D>() == null) return;
        g.transform.parent = plt.transform;
        OnCloud.Add(g);
    }


    HashSet<GameObject> copy_OnCLoud = new HashSet<GameObject>(OnCloud);
    foreach(GameObject g in copy_OnCLoud)
      if (CurrentFadeStatus == FadeOutStatus.Invisible || CurrentFadeStatus == FadeOutStatus.FadeOut||!col.currentCollisions.Contains(g))
      {
        OnCloud.Remove(g);
        g.transform.parent = null;
      }

    float t = Time.time * Speed;

    if (snapBack) while (t > snapBackModulo) t -= snapBackModulo;

    if (MovesRight)
    {
      plt.transform.localPosition = new Vector3(Mathf.Sin(t) * MoveRange, snapBack ? t : plt.transform.localPosition.y);
    }
    if (MovesLeft)
    {
      plt.transform.localPosition = new Vector3(plt.transform.localPosition.x, snapBack?t: Mathf.Cos(t) * MoveRange);
    }
    if (FadesOnCollision && col.currentCollisions.Count > 0 && CurrentFadeStatus == FadeOutStatus.Visible)
    {
      NextFadeStatusChange = Time.time + fadeOutTime;
      StatusChangeStart = Time.time;
      CurrentFadeStatus = FadeOutStatus.FadeOut;
      return;
    }else
    
    if (CurrentFadeStatus == FadeOutStatus.FadeOut)
    {
      float range = NextFadeStatusChange - StatusChangeStart;
      float current = Time.time - StatusChangeStart;
      float perc = current / range;
      if (perc > 1) perc = 1;
      rnd.color = new Color(1,1,1,1- perc);      
      if (current > range)
      {
        CurrentFadeStatus = FadeOutStatus.Invisible;
        StatusChangeStart = Time.time;
        NextFadeStatusChange = Time.time + fadedOutTime;
      }
    }else
    if (CurrentFadeStatus == FadeOutStatus.Invisible)
    {
      rnd.color = new Color(1, 1, 1, 0);
      plt.GetComponent<BoxCollider2D>().enabled = false;
      if (Time.time > NextFadeStatusChange)
      {
        CurrentFadeStatus = FadeOutStatus.FadeIn;
        StatusChangeStart = Time.time;
        NextFadeStatusChange = Time.time + fadeInTime;
      }
    }else
    if (CurrentFadeStatus == FadeOutStatus.FadeIn)
    {
      plt.GetComponent<BoxCollider2D>().enabled = true;
      float range = NextFadeStatusChange - StatusChangeStart;
      float current = Time.time - StatusChangeStart;
      rnd.color = new Color(1, 1, 1, current / range);
      if (current > range)
      {
        CurrentFadeStatus = FadeOutStatus.Visible;
        StatusChangeStart = 0;
        NextFadeStatusChange = 0;
      }
    }else
    {
      rnd.color = new Color(1, 1, 1, 1);
    }
  }
}
