using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour {
  public GameObject Sound;

  public void Play()
  {
    Instantiate(Sound).GetComponent<AudioSource>().Play();
  }
}
