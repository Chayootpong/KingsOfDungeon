using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingConsole : MonoBehaviour {

    public static int quality = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        QualitySettings.SetQualityLevel(quality);

    }
}
