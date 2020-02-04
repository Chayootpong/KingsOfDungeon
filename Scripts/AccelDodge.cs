using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelDodge : MonoBehaviour {

    Vector3 accelDir, localPosition;
    float accelX, accelY;
    bool delayDodge;
    public static int position;

    // Use this for initialization
    void Start () {

        position = 0;
        localPosition = transform.position;
        accelX = Input.acceleration.x;
        accelY = Input.acceleration.y;
        delayDodge = true;

    }
	
	// Update is called once per frame
	void Update () {

        accelDir = Input.acceleration;

        if (delayDodge)
        {
            if (Input.acceleration.y - accelY >= 0.5f)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.5f), Time.deltaTime * 5);
                position = 3;
                Invoke("Lerping", 0.5f);
                Invoke("Delay", 1.5f);
            }

            else if (Input.acceleration.x - accelX <= -0.5f)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z), Time.deltaTime * 5);
                position = 1;
                Invoke("Lerping", 0.5f);
                Invoke("Delay", 1.5f);
            }

            else if (Input.acceleration.x - accelX >= 0.5f)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z), Time.deltaTime * 5);
                position = 2;
                Invoke("Lerping", 0.5f);
                Invoke("Delay", 1.5f);
            }

            else
            {
                transform.position = Vector3.Lerp(transform.position, localPosition, Time.deltaTime * 5);
            }
        }

        else transform.position = Vector3.Lerp(transform.position, localPosition, Time.deltaTime * 5);
    }

    public void Delay()
    {
        delayDodge = true;
    }

    public void Lerping()
    {
        position = 0;
        delayDodge = false;
    }
}
