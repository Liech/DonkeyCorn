using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoBackCam : MonoBehaviour {

  public bool CanGoBack = false;
  public float yCamStart;
  public float OverdriveSpeed = 4;

  public void Start()
  {
    yCamStart = Camera.main.transform.position.y;
    gameObject.AddComponent<SugarLevelDependent>();
  }

  public void FixedUpdate()
  {

    SugarStatus sugar = GetComponent<SugarLevelDependent>().CurrentLevel;

    GameObject p = GameObject.Find("Player");
    if (p == null) return;
    Vector3 pos = Camera.main.transform.position + new Vector3(p.transform.position.x - Camera.main.transform.position.x, p.transform.position.y - Camera.main.transform.position.y, 0);
    if (sugar == SugarStatus.Overdrive)
      pos.x += OverdriveSpeed;
    if (pos.x < Camera.main.transform.position.x && !CanGoBack)
      pos.x = Camera.main.transform.position.x;
    if (pos.y < yCamStart)
      pos.y = yCamStart;
    Camera.main.transform.position = pos;
  }
}
