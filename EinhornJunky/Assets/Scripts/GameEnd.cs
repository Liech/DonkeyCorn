using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEnd : MonoBehaviour {

  bool won = false;

  // Update is called once per frame
  void Update () {

    if (!won)
    {
      GameObject Player = GameObject.Find("Player");
      if (Player == null) return;
      if (Player.transform.position.x > transform.position.x)
      {
        GameObject looseScreen = GameObject.Find("Canvas");
        foreach (Transform x in looseScreen.transform)
          if (x.name == "WinScreen") looseScreen = x.gameObject;
        looseScreen.SetActive(true);
        won = true;
        Time.timeScale = 0;
      }
    }
  }
}
