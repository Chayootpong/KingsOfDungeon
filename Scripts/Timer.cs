using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    int tw, currenttw, elapsedtw;
    public int stbtw;
    public Text timeNum, ip;
    public Image timeOBJ;

    // Use this for initialization
    void Start () {

        currenttw = 0;
        elapsedtw = (int)Time.time;
	}
	
	// Update is called once per frame
	void Update () {

        tw = (int)Time.time;
        if (currenttw != tw) currenttw = tw - elapsedtw;

        if (stbtw - currenttw >= 0)
        {
            timeNum.text = (stbtw - currenttw).ToString();
            timeOBJ.GetComponent<Image>().fillAmount = 1 - (float)currenttw / stbtw;
        }
        else gameObject.SetActive(false);

        ip.text = "<color=#FFF167>locate :</color> " + NetworkServerUI.ipaddress;

    }
}
