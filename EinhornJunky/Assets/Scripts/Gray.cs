using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gray : MonoBehaviour {

  public Material mat;
  public float Range = 0.02f;
  void OnRenderImage(RenderTexture src, RenderTexture dest)
  {
    float lvl = GameObject.Find("Canvas/SugarLevel").GetComponent<SugarLevel>().effectLevel;
    if (lvl < 1.5f)
    {
      Range = (1.5f - (lvl)) / 1.5f;
      Range = Mathf.Pow(Range,0.4f);
    }
    else
      Range = 0;
    mat.SetFloat("_Range", Range);
    mat.SetFloat("_Random", Random.value);
    Graphics.Blit(src, dest, mat);
  }  
}
