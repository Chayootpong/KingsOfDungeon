using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt2 : MonoBehaviour {

    GameObject cam;

	// Use this for initialization
	void Start () {

        cam = GameObject.Find("Main Camera");
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.LookAt(2 * transform.position - cam.transform.position);
		
	}
}
