using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMixer : MonoBehaviour {
  public float FadeInStart;
  public float FullVolume;
  public float FadeOutEnd;
  public bool FullL;
  public bool FullR;

	// Update is called once per frame
	void Update () {
    float lvl = GameObject.Find("Canvas/SugarLevel").GetComponent<SugarLevel>().CurrentLevel;
    AudioSource s = GetComponent<AudioSource>();
    s.pitch = 0.5f + (lvl / 3.0f);
    if (lvl < FadeInStart)  s.volume = 0;
    else if (lvl > FadeOutEnd)  s.volume = 0;
    else if (lvl < FullVolume)
    {
      float perc = (lvl - FadeInStart) / (FullVolume - FadeInStart);
      s.volume = perc;
      
    }
    else
    {
      float perc = (lvl - FadeInStart) / (FullVolume - FadeInStart);
      s.volume = 1-perc;
    }

    if (FullL && lvl < FullVolume) s.volume = 1;
    if (FullR && lvl > FullVolume) s.volume = 1;

  }
}
