using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIAnimation : MonoBehaviour {
    public List<Sprite> Animation;
    public float AnimationDelay = 0.5f;

    protected float fMod(float A, float B)
    {
      while (A > 0) A -= B;
      return A + B;
    }

  // Update is called once per frame
  void Update() {
    float mod = fMod(Time.time, (AnimationDelay * Animation.Count));
    int animindex = (int)(mod / AnimationDelay);
    GetComponent<Image>().sprite = Animation[animindex];
	}
}
