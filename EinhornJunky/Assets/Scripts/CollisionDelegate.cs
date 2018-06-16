using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDelegate : MonoBehaviour {

  public event Action<CollisionDelegate, Collision2D> OnCollisionEnter = delegate { };
  public event Action<CollisionDelegate, Collision2D> OnCollisionExit = delegate { };

  void OnCollisionEnter2D(Collision2D col)
  {
    OnCollisionEnter(this, col);
  }
  void OnCollisionExit2D(Collision2D col)
  {
    OnCollisionExit(this, col);
  }

}
