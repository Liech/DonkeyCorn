using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SugarExplodeAnimation : MonoBehaviour
{

  Vector3 StartPos;
  public float falldepth;
  public float fallSpeed;
  int rotationTime;
  public int maxRotationFrames = 20;
  public int ExplosionFrames = 40;
  public GameObject uiCandy;

  // Use this for initialization
  void Start()
  {
    StartPos = transform.position;
  }
  bool exploded = false;
  bool eaten = false;
  List<GameObject> candys;
  // Update is called once per frame
  void FixedUpdate()
  {
    float resolutionFactor = (Screen.height / 1080.0f);
    float perc = Mathf.Abs(StartPos.y - transform.position.y) / (falldepth * resolutionFactor);
    if (perc < 1)
      transform.position = transform.position - new Vector3(0, fallSpeed * resolutionFactor, 0);    
    else if (perc > 1 && rotationTime <= maxRotationFrames)
    {
      if (!eaten)
      {
        eaten = true;
        transform.parent.GetChild(4).GetComponent<Image>().color = new Color(1, 1, 1, 0);
      }
      transform.Rotate(new Vector3(0, 0, 1), -40);
      rotationTime++;
    }

    Image Dead = transform.GetChild(0).GetComponent<Image>();
    Image Alive = GetComponent<Image>();

    if (rotationTime > maxRotationFrames)
    {
      if (!exploded)
      {
        exploded = true;
        candys = new List<GameObject>();
        for(int i = 0;i < 40;i++)
          candys.Add(Instantiate(uiCandy,transform));
      }
      transform.rotation = Quaternion.identity;
      rotationTime++;
      Alive.color = new Color(1, 1, 1, 0);
      Dead.color = new Color(1, 1, 1, 1);
      if (rotationTime > maxRotationFrames + ExplosionFrames)
      {
        for (int i = candys.Count-1; i >= 0; i--)
          Destroy(candys[i]);
        candys.Clear();
        Alive.color = new Color(1, 1, 1, 1);
        Dead.color = new Color(1, 1, 1, 0);
        transform.position = StartPos;
        rotationTime = 0;
        exploded = false;
        eaten = false;
        transform.parent.GetChild(4).GetComponent<Image>().color = new Color(1, 1, 1,1);
      }
    }

  }
}
