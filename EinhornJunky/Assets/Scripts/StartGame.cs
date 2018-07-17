using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

  public GameObject loadingScreen;
  public string sceneName;

	public void Clicked()
  {
    if (loadingScreen != null) Instantiate(loadingScreen,GameObject.Find("Canvas").transform);
    SceneManager.LoadScene(sceneName);
  }
}
