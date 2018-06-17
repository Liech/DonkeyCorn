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
    pitch(lvl);
    if (lvl < FadeInStart)  s.volume = 0;
    else if (lvl > FadeOutEnd)  s.volume = 0;
    else if (lvl < FullVolume)
    {
      float perc = (lvl - FadeInStart) / (FullVolume - FadeInStart);
      s.volume = perc;
      
    }
    else
    {
      //float perc = (lvl - FadeInStart) / (FullVolume - FadeInStart);
      float perc = (lvl - FullVolume) / (FadeOutEnd - FullVolume);
      s.volume = 1-perc;
    }

    if (FullL && lvl < FullVolume) s.volume = 1;
    if (FullR && lvl > FullVolume) s.volume = 1;

  }

  public void pitch(float lvl)
  {
    AudioSource s = GetComponent<AudioSource>();
    //s.pitch = 0.7f + (lvl / 3.0f);
    float pitch = 0;
    if (lvl < 0.9f) pitch = 0.8f;
    else if (lvl > 2.5f) pitch = 2.0f + (lvl - 2.5f) * 2;
    else if (lvl < 1.1f) pitch = 0.8f + (lvl - 0.9f) * 0.5f;
    else if (lvl < 1.9f) pitch = 1.2f;
    else if (lvl < 2.1f) pitch = 1.2f + (lvl - 1.9f) * 0.5f;
    else pitch = 2.0f;
    s.pitch = pitch;


  }
}
