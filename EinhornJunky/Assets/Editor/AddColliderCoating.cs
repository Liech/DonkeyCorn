using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

/// <summary>
/// http://answers.unity3d.com/questions/144453/reverting-several-gameobjects-to-prefab-settings-a.html
/// </summary>
public class AddColliderCoating : UnityEditor.Editor
{
  [MenuItem("Tools/Apply Coating")]
  static void Coat()
  {
    GameObject[] selection = Selection.gameObjects;

    if (selection.Length > 0)
    {
      Vector2 min = new Vector3(float.PositiveInfinity, float.PositiveInfinity);
      Vector2 max = new Vector3(float.NegativeInfinity, float.NegativeInfinity);
      int elementsfound = 0;
      for(int i = 0;i < selection.Length; i++)
      {
        Collider2D c = selection[i].GetComponent<Collider2D>();
        if (c == null) continue;
        elementsfound++;
        Vector2 minC = c.bounds.min;
        Vector2 maxC = c.bounds.max;
        if (minC.x < min.x) min.x = minC.x;
        if (minC.y < min.y) min.y = minC.y;
        if (maxC.x > max.x) max.x = maxC.x;
        if (maxC.y > max.y) max.y = maxC.y;
      } 
      if (elementsfound == 0) return;
      GameObject G = new GameObject();
      if (GameObject.Find("Coat") == null)
      {
        GameObject g = new GameObject();
        g.name = "Coat";
      }      
      G.transform.parent = GameObject.Find("Coat").transform;
      G.transform.position = (min + max) / 2;
      BoxCollider2D b = G.AddComponent<BoxCollider2D>();
      b.offset = new Vector2(0,0);
      Vector2 siz = (max- min) + new Vector2(0.05f, 0.05f);
      b.size = siz;
      G.name = "Coat " + G.transform.position;
      G.AddComponent<NoticeMe>();
    }

  }
}