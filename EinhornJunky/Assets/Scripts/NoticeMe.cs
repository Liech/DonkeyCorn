using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoticeMe : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  private void OnDrawGizmos()
  {
    Gizmos.DrawCube(transform.position, new Vector3(2, 2, 2));
  }
}
