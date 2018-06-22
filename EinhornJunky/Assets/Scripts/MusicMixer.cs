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
        if (lvl < 1.0f) pitch = 0.8f;
        else if (lvl < 1.1f) pitch = 0.8f + ((lvl - 1) * 4);
        else if (lvl < 2.0f) pitch = 1.2f;
        else if (lvl < 2.1f) pitch = 1.2f + ((lvl - 2)*4);
        else if (lvl < 2.8f) pitch = 1.6f;
        else pitch = 1.6f + (lvl - 1.6f);
    s.pitch = pitch;


  }
}
      