using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerometer : MonoBehaviour {

    float accelX, accelY;

	// Use this for initialization
	void Start () {

        accelX = Input.acceleration.x;
        accelY = Input.acceleration.y;

	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(0, (Input.acceleration.x - accelX) * Time.deltaTime * 100, 0, Space.Self);     //Horizontal
        //transform.Rotate((Input.acceleration.y - accelY) * Time.deltaTime * 100, 0, 0, Space.Self);   //Vertical
    }
}
