using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Animator : MonoBehaviour {

  private int currentAnimation;
  public List<Sprite> Frames;

  public float AnimationSpeed;
  public bool  Flip;

  public int CurrentFrame
  {
    get
    {
      return currentAnimation;
    }
    set
    {
      currentAnimation = value;
      gameObject.GetComponent<SpriteRenderer>().sprite = Frames[currentAnimation];
    }
  }

  private float startTime;
  public void Start()
  {
    startTime = Time.time;
  }

  public void Update()
  {
    float t = Time.time - startTime;
    int index = (int)(t / AnimationSpeed) % Frames.Count;
    if (AnimationSpeed != 0) CurrentFrame = index;
    gameObject.GetComponent<SpriteRenderer>().flipX = Flip;
  }

}
