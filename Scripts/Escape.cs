using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Escape : MonoBehaviour {

    GameObject nwmng;

    // Use this for initialization
    void Start () {

        nwmng = GameObject.Find("Network Manager");
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.Escape))
        {
            nwmng.GetComponent<NetworkServerUI>().ClearRegisterKey();
            SceneManager.LoadScene("Demo");
        }
    }

}
