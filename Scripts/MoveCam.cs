using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveCam : MonoBehaviour {

    public static int camNum = 1;
    public GameObject info;
    public GameObject[] posCam, canvas; //Standby Menu Charac Intro
    int camNumClick = 1;

    //INTRO COMPONENTS
    public GameObject newPlayer;
    public InputField nameInput;

    //PLAYERPREFS
    public Text playerName;
    public Text exp;

    // Use this for initialization
    void Start () {

        if (PlayerPrefs.GetString("Name") != "") playerName.text = PlayerPrefs.GetString("Name");
        if (PlayerPrefs.GetInt("Level") != 0) exp.text = PlayerPrefs.GetInt("Level").ToString();
        if (!DontDestroy.isIntro) canvas[3].SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        transform.position = Vector3.Lerp(transform.position, posCam[camNumClick].transform.position, Time.deltaTime * 2f);
        transform.rotation = Quaternion.Lerp(transform.rotation, posCam[camNumClick].transform.rotation, Time.deltaTime * 2f);

        if (!DontDestroy.isIntro)
        {
            if (camNum > -1)
            {
                canvas[camNum].SetActive(true);
            }
            else
            {
                for (int i = 0; i < canvas.Length; i++)
                {
                    if (i != camNum) canvas[i].SetActive(false);
                }
            }
        }
    }

    //FOR INTRO
    public void CheckNewPlayer()
    {
        if (PlayerPrefs.GetString("Name") == "") newPlayer.SetActive(true);
        else
        {
            canvas[3].SetActive(false);
            DontDestroy.isIntro = false;
        }
    }

    public void RegisterNewPlayer() 
    {
        if (nameInput.text != "")
        {
            PlayerPrefs.SetString("Name", nameInput.text);
            PlayerPrefs.SetInt("EXP", 0);
            PlayerPrefs.SetInt("Level", 1);
            playerName.text = PlayerPrefs.GetString("Name");
            canvas[3].SetActive(false);
            DontDestroy.isIntro = false;
        }
    }

    public void Standby()
    {
        camNumClick = 0;
    }

    public void Menu()
    {
        camNumClick = 1;
    }

    public void Charac()
    {
        camNumClick = 2;
    }

    public void ShowInfo()
    {
        info.SetActive(true);
    }
}
