using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreGUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
    List<score> score = Highscore.Instance.getTop10();

    Text t = transform.GetChild(0).GetComponent<Text>();
    if (score.Count == 0)
    {
      t.text = "<b>Highscore</b>\nNo score yet.";
    }
    else
    {
      string msg = "<b>Highscore</b>\nName / Percentage / Time\n";
      for(int i = 0;i < score.Count;i++)
      {
        msg += score[i].name + ": " + (int)(100 * score[i].percentage) + "% " + score[i].time + "\n";
      }
      t.text = msg;
    }
	}

  public void submitScore()
  {
    //Highscore.Instance.addScore(new score(transform.GetChild(2).GetChild(1).GetComponent<Text>().text
    //  ,0,Time.time);
  }
	
	// Update is called once per frame
	void Update () {
		
	}
}
