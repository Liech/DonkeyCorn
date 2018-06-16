using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour {

  bool won = false;

  // Update is called once per frame
  void Update () {

    if (!won)
    {
      GameObject Player = GameObject.Find("Player");
      if (Player == null) return;
      if (Player.transform.position.x > transform.position.x) won = true;
    }

    if (won) Debug.Log("Sieg");
  }
}
