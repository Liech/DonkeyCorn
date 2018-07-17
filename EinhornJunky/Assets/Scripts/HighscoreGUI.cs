using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreGUI : MonoBehaviour {
  score ownScore;

  // Use this for initialization
  void Start ()
  { 
    ownScore = getScore();
    List<score> score = Highscore.Instance.getTop10();

    Text t = transform.GetChild(1).GetComponent<Text>();
    if (score.Count == 0)
    {
      t.text = "----";
    }
    else
    {
      string msg = "Name / Fortschritt / Zeit\n";
      for(int i = 0;i < score.Count;i++)
      {
        msg += score2String(score[i]) + "\n";
      }
      t.text = msg;
    }
    transform.GetChild(2).GetComponent<Text>().text = score2String(ownScore);
	}

  private string score2String(score s)
  {
    return ((s.name.Length > 0)?s.name:"unkown") + "  --  " + (int)(100 * s.percentage) + "%  --   " + s.time + "s";
  }

  private score getScore()
  {
    string name = transform.GetChild(3).GetComponent<InputField>().text;
    float start = GameObject.Find("Start").transform.position.x;
    float end = GameObject.Find("End").transform.position.x;
    float plr = GameObject.Find("Player").transform.position.x;
    float perc = (plr - start) / (end - start);
    float t = Time.time - GameObject.Find("Canvas/Time").GetComponent<time>().startTime;
    return new score(name, perc, t);
  }

  public void nameChanged()
  {
    ownScore.name = transform.GetChild(3).GetComponent<InputField>().text;
    transform.GetChild(2).GetComponent<Text>().text = score2String(ownScore);
  }

  public void submitScore()
  {
    Highscore.Instance.addScore(ownScore);
    transform.GetChild(4).GetComponent<Button>().interactable = false;
    Start();
    transform.GetChild(3).GetComponent<InputField>().text = "Gespeichert";
    transform.GetChild(3).GetComponent<InputField>().interactable = false;
    transform.GetChild(2).GetComponent<Text>().text = "";
  }

  // Update is called once per frame
  void Update () {
		
	}
}
