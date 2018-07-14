using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DeathReason
{
  Fall, CandyCorn, OverSugar, Pig, Undersugar
}

public class DeadHandle : MonoBehaviour {
  float TimeOfDeath;
  bool dead;
  public float timeUntilDeathScreen = 3;
  public float fadeInTime = 1;
  DeathReason reason;
  public GameObject MovingCandy;
  public GameObject ExplosionSound;

  public float CandyExplosionVelocity;
  public float CandyExplosionAngularVelocity;

  // Use this for initialization
  void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
  {
    GameObject plr = GameObject.Find("Player");
    if (dead)
    {


      if (TimeOfDeath + timeUntilDeathScreen < Time.time)
      {
        GameObject looseScreen = GameObject.Find("Canvas");
        foreach (Transform x in looseScreen.transform)
          if (x.name == "LooseScreen") looseScreen = x.gameObject;
        looseScreen.SetActive(true);

        Image img = looseScreen.GetComponent<Image>();
        img.enabled = true;
        float perc = (Time.time - (TimeOfDeath + timeUntilDeathScreen)) / fadeInTime;
        if (perc > 1)
        {
          perc = 1;

        }
        img.color = new Color(1, 1, 1, perc);
        if (reason == DeathReason.Fall)
          looseScreen.transform.GetChild(0).gameObject.SetActive(true);
        else if (reason == DeathReason.Undersugar)
          looseScreen.transform.GetChild(1).gameObject.SetActive(true);
        else if (reason == DeathReason.OverSugar)
          looseScreen.transform.GetChild(2).gameObject.SetActive(true);
        else if (reason == DeathReason.Pig)
          looseScreen.transform.GetChild(3).gameObject.SetActive(true);
        else if (reason == DeathReason.CandyCorn)
          looseScreen.transform.GetChild(4).gameObject.SetActive(true);
      }
    }
  }

  public void Dead(DeathReason reason)
  {
    if (dead) return;
    this.reason = reason;
    GameObject plr = GameObject.Find("Player");
    //GameObject.Find("Canvas/LooseScreen").GetComponent<Image>().enabled = true;
    plr.GetComponent<PlayerController>().Dead = true;
    TimeOfDeath = Time.time;
    dead = true;

    if (reason == DeathReason.OverSugar)
    {
      for (int i = 0; i < 30; i++)
      {
        GameObject candy = Instantiate(MovingCandy);
        Rigidbody2D r = candy.GetComponent<Rigidbody2D>();
        candy.transform.position = plr.transform.position;
        r.angularVelocity = (Random.value * CandyExplosionAngularVelocity) - CandyExplosionAngularVelocity / 2;
        r.velocity = new Vector2(Random.value * CandyExplosionVelocity - CandyExplosionVelocity / 2, Random.value * CandyExplosionVelocity - CandyExplosionVelocity / 2);
        plr.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        Instantiate(ExplosionSound).GetComponent<AudioSource>().Play();
      }
    }

  }
}
