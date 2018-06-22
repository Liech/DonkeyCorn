using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugaredTexture : MonoBehaviour {

  public Sprite Depri;
  public Sprite Wach;
  public Sprite Overdrive;

	// Update is called once per frame
	void Update () {
    SugarStatus s = GetComponent<SugarLevelDependent>().CurrentLevel;
    if (s == SugarStatus.Depri)
      GetComponent<SpriteRenderer>().sprite = Depri;
    if (s == SugarStatus.Wach)
      GetComponent<SpriteRenderer>().sprite = Wach;
    if (s == SugarStatus.Overdrive)
      GetComponent<SpriteRenderer>().sprite = Overdrive;
  }
}
