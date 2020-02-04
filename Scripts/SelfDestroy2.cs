using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy2 : MonoBehaviour {

    public float destime;

	// Use this for initialization
	void Start () {

        Invoke("DelayDestroy", destime);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DelayDestroy()
    {
        Destroy(gameObject);
    }
}
