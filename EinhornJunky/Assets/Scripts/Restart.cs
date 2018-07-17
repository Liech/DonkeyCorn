using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {

  public GameObject loadingScreen;
  public void RestartGame()
  {
    if (loadingScreen != null) Instantiate(loadingScreen, GameObject.Find("Canvas").transform);
    Time.timeScale = 1;
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Return))
    {
      if (loadingScreen != null) Instantiate(loadingScreen, GameObject.Find("Canvas").transform);
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
  }
}
