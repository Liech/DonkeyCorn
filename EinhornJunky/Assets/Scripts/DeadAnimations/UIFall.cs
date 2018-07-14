using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFall : MonoBehaviour
{

  Vector3 StartPos;
  public float falldepth;
  public float fallSpeed;
  public float waitTimeBeforeReset;

  float EndTime;

  // Use this for initialization
  void Start()
  {
    StartPos = transform.position;
  }

  bool endTriggered;
  // Update is called once per frame
  void FixedUpdate()
  {
    float resolutionFactor = (Screen.height / 1080.0f);
    if (!endTriggered) transform.position = transform.position - new Vector3(0, fallSpeed * resolutionFactor, 0);
    float perc = Mathf.Abs(StartPos.y - transform.position.y) / (falldepth * resolutionFactor);
    if (perc > 1)
    {
      if (!endTriggered) { EndTime = Time.time; endTriggered = true; }
      if (Time.time > EndTime + waitTimeBeforeReset)
      {
        endTriggered = false;
        transform.position = StartPos;
      }
    }
  }
}
