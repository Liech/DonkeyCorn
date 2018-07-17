using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class score
{
  public string name;
  public float percentage;
  public float time;

  public score() { }
  public score(string name, float percentage, float time)
  {
    this.name = name;
    this.percentage = percentage;
    this.time = time;
  }
}

public class Highscore {
  static string path;
  private List<score> Scores;

  private static Highscore _instance;
  public static Highscore Instance
  {
    get
    {
      if (_instance == null) _instance = new Highscore();
      return _instance;
    }
  }

	// Use this for initialization
	private Highscore() {
    path = Application.persistentDataPath + "Highscore";
    if (!File.Exists(path))
    {
      FileStream f = new FileStream(path,FileMode.OpenOrCreate);
      BinaryWriter bw = new BinaryWriter(f);
      int numberOfScores = 0;
      bw.Write(numberOfScores);
      f.Close();
      Scores = new List<score>();
    }
    else
    {
      FileStream f = new FileStream(path, FileMode.Open);
      BinaryReader br = new BinaryReader(f);
      int numberOfScores = 0;
      numberOfScores = br.ReadInt32();
      Scores = new List<score>();
      for(int i = 0;i < numberOfScores;i++)
      {
        score s = new score()
        {
          name = br.ReadString(),
          percentage = br.ReadSingle(),
          time = br.ReadSingle()          
        };
        Scores.Add(s);
      }
      f.Close();
    }
	}

  class comp : IComparer<score>
  {
    public int Compare(score x, score y)
    {
      if (x.percentage == y.percentage || (x.percentage > 0.99 && y.percentage > 0.99))
        return y.time.CompareTo(x.time);
      else
        return y.percentage.CompareTo(x.percentage);
    }
  }

  void sortScore()
  {    
    Scores.Sort(new comp());
  }

  void writeOut()
  {
    FileStream f = new FileStream(path, FileMode.OpenOrCreate);
    BinaryWriter bw = new BinaryWriter(f);
    bw.Write(Scores.Count);
    for(int i = 0;i < Scores.Count; i++)
    {
      bw.Write(Scores[i].name);
      bw.Write(Scores[i].percentage);
      bw.Write(Scores[i].time);
    }
    f.Close();
  }
	
  public List<score> getTop10()
  {
    return new List<score>(Scores);
  }

  public void addScore(score s)
  {
    Scores.Add(s);
    sortScore();
    if (Scores.Count > 10)
      Scores = Scores.GetRange(0, 9);
    writeOut();
  }  
}
