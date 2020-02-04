using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharac : MonoBehaviour {

    public GameObject[] charac, stbCharac;
    public GameObject popupChar;
    public Text selectCharName;
    public string[] charName;
    int currentSelect = 0;

    public static int selected;

	// Use this for initialization
	void Start () {

        selected = currentSelect = 0;
    }
	
	// Update is called once per frame
	void Update () {

        for (int i = 0; i < charac.Length; i++)
        {
            if (i == currentSelect) charac[i].SetActive(true);
            else charac[i].SetActive(false);
        }

        for (int i = 0; i < stbCharac.Length; i++)
        {
            if (i == selected) stbCharac[i].SetActive(true);
            else stbCharac[i].SetActive(false);
        }

        selectCharName.text = DontDestroy.selectedPlayer = charName[currentSelect];
        selected = currentSelect;
	}

    public void ShowPopupChar()
    {
        popupChar.SetActive(true);
    }

    public void Select(int num)
    {
        currentSelect = num;
        popupChar.SetActive(false);
    }

    public void Exit()
    {
        popupChar.SetActive(false);
    }
}
