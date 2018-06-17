using UnityEngine;
using System.Collections;

public class AudioSuicider : MonoBehaviour {

    float initTime;
    AudioSource src;
	// Use this for initialization
	void Start () {
        initTime = Time.time;
        src = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if ((Time.time - initTime) > src.timeSamples / 441000f + 9)
        {
            Destroy(this.gameObject);        
        }
	}
}
