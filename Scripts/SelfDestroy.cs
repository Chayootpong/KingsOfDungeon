using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour {

    public float destime;
    public string selfName;
    GameObject self;

	// Use this for initialization
	void Start () {

        Invoke("DelayDestroy", destime);
        self = GameObject.Find(selfName);
        self.GetComponent<Controller>().isatk = true;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DelayDestroy()
    {
        self.GetComponent<Controller>().isatk = false;
        Destroy(gameObject);
    }
}
