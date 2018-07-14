using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Outline : MonoBehaviour
{

  public Material mat;
  public float Range = 0.02f;
  void OnRenderImage(RenderTexture src, RenderTexture dest)
  {
    mat.SetFloat("_Range", Range);
    Graphics.Blit(src, dest, mat);
  }
}
