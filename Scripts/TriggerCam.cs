using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCam : MonoBehaviour {

    public int camNumPos = -1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "MainCamera")
        {
            MoveCam.camNum = camNumPos;
        }
    }

    public void OnTriggerStay(Collider col)
    {
        if(col.gameObject.tag == "MainCamera")
        {
            MoveCam.camNum = camNumPos;
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "MainCamera")
        {
            MoveCam.camNum = -1;
        }
    }
}
