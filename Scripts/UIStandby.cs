using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIStandby : MonoBehaviour {

    public static string ip = "";
    public Text iptext, stbName;
    public string[] charName;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        ip = iptext.text;
        stbName.text = charName[UICharac.selected];
    }

    public void OnStart()
    {
        SceneManager.LoadScene("Controller");
    }
}
