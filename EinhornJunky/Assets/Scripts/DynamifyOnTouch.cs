using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamifyOnTouch : MonoBehaviour {

  private void Start()
  {
    GetComponent<Rigidbody2D>().isKinematic = true;
  }

  void OnCollisionEnter2D(Collision2D col)
  {
    GetComponent<Rigidbody2D>().isKinematic = false;
  }
}
