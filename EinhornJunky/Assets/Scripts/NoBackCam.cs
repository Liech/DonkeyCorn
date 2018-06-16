using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoBackCam : MonoBehaviour {

  public bool CanGoBack = false;


  public void Update()
  {
    GameObject p = GameObject.Find("Player");
    if (p == null) return;
    Vector3 pos = Camera.main.transform.position + new Vector3(p.transform.position.x - Camera.main.transform.position.x, 0, 0);
    if (pos.x < Camera.main.transform.position.x && !CanGoBack)
      pos.x = Camera.main.transform.position.x;

    Camera.main.transform.position = pos;
  }
}
