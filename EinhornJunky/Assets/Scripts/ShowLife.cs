using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ShowLife : MonoBehaviour {

    public float MaxLife = 100;
    public float CurrentDamage = 0;
    public List<Sprite> Lifes;
    public float YOffset = 3;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    //CurrentDamage++;
    //CurrentDamage = CurrentDamage % MaxLife;
    int Index = (int)(10.0f * ((float)CurrentDamage / (float)MaxLife));
    gameObject.GetComponent<Image>().sprite = Lifes[Index];
	}
}
