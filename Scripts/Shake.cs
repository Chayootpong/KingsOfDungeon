using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour {

    Vector3 accelDir;
    public GameObject vtpy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        accelDir = Input.acceleration;

        if (NetworkClientUI.inGame)
        {
            if (vtpy.name == "Knight")
            {
                if (accelDir.sqrMagnitude >= 4.5f)
                {
                    NetworkClientUI.SendRespond(0);
                    vtpy.GetComponent<Healthpoint>().dbf = true;
                }
                else
                {
                    NetworkClientUI.SendRespond(-1);
                    vtpy.GetComponent<Healthpoint>().dbf = false;
                }
            }
        }
    }
}
