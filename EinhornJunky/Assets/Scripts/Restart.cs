using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {

	public void RestartGame()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Return))
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }
}
