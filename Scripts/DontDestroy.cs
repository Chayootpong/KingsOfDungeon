using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour {

    public static string selectedPlayer = "Knight";
    public static bool isIntro = true;

	// Use this for initialization
	void Start () {

        if (isIntro) isIntro = true;
	}
	
	// Update is called once per frame
	void Update () {

        Debug.Log(isIntro);
        DontDestroyOnLoad(gameObject);		
	}
}
