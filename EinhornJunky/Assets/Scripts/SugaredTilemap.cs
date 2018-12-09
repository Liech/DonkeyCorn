using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SugaredTilemap : MonoBehaviour {

  public List<TileBase> DepriTiles;
  public List<TileBase> SugarTiles;

  private void Start()
  {
    old = SugarStatus.Overdrive;
  }

  SugarStatus old;
	// Update is called once per frame
	void Update () {  
    SugarStatus s = GetComponent<SugarLevelDependent>().CurrentLevel;
    if (old == s)
      return;
    old = s;
    if (s == SugarStatus.Depri)
    {
      for (int i = 0; i < DepriTiles.Count; i++)
        GetComponent<Tilemap>().SwapTile(SugarTiles[i], DepriTiles[i]);      
    }
    if (s == SugarStatus.Wach)
    {
      for (int i = 0; i < DepriTiles.Count; i++)
        GetComponent<Tilemap>().SwapTile(DepriTiles[i], SugarTiles[i]);
    }
    if (s == SugarStatus.Overdrive)
    {
      for (int i = 0; i < DepriTiles.Count; i++)
        GetComponent<Tilemap>().SwapTile(DepriTiles[i], SugarTiles[i]);
    }
  }
}
