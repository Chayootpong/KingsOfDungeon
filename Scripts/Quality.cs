using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quality : MonoBehaviour {

    public int quality;

	// Use this for initialization
	void Start () {
        QualitySettings.SetQualityLevel(quality);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
