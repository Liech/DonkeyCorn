using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostProcess : MonoBehaviour {

  public Material mat;
  private float time;
  void OnRenderImage(RenderTexture src, RenderTexture dest)
  {
    float lvl = GameObject.Find("Canvas/SugarLevel").GetComponent<SugarLevel>().effectLevel;
    if (lvl < 2 || GameObject.Find("Canvas/WinScreen")
      || GameObject.Find("Canvas/LooseScreen"))
    {
      mat.SetFloat("_Red", 1);
      mat.SetFloat("_Random", Random.value);
      mat.SetFloat("_Green", 1);
      mat.SetFloat("_Blue", 1);
      Graphics.Blit(src, dest, mat);
      return;
    }
    time += Time.deltaTime * ((lvl - 2.0f)*5);
    Color c = Rainbow(time);
    mat.SetFloat("_Random", Random.value);
    mat.SetFloat("_Red", c.r/255.0f);
    mat.SetFloat("_Green", c.g / 255.0f);
    mat.SetFloat("_Blue", c.b/255.0f);
    Graphics.Blit(src, dest, mat);
  }

  public static Color Rainbow(float progress)
  {
    float div = (Mathf.Abs(progress % 1) * 6);
    int ascending = (int)((div % 1) * 255);
    int descending = 255 - ascending;

    switch ((int)div)
    {
      case 0:
        return new Color(255, 255, ascending, 0);
      case 1:
        return new Color(255, descending, 255, 0);
      case 2:
        return new Color(255, 0, 255, ascending);
      case 3:
        return new Color(255, 0, descending, 255);
      case 4:
        return new Color(255, ascending, 0, 255);
      default: // case 5
        return new Color(255, 255, 0, descending);
    }
  }
}
