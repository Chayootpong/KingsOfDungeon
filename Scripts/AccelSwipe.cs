using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AccelSwipe : MonoBehaviour {

    public Swipe swipe;
    public GameObject[] side;
    public GameObject nwmng;
    int current = -1, rand = 0, lastnum = -1, count = 0;

	// Use this for initialization
	void Start () {

        InvokeRepeating("RandomRepeat", 0.75f, 0.75f);

        GetRandomNumber();

        for (int i = 0; i < 4; i++)
        {
            if (i == rand) side[i].SetActive(true);
            else side[i].SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (swipe.SWLeft) current = 0;
        else if (swipe.SWUp) current = 1;
        else if (swipe.SWRight) current = 2;
        else if (swipe.SWDown) current = 3;
    }

    public void RandomRepeat()
    {
        if (rand != current || count >= 8)
        {
            Respond();
            Debug.Log("BAD");
        }
        else Debug.Log("GOOD");

        current = -1;
        GetRandomNumber();

        for (int i = 0; i < 4; i++)
        {
            if (i == rand) side[i].SetActive(true);
            else side[i].SetActive(false);
        }

        count++;
    }

    public void GetRandomNumber()
    {
        rand = Random.Range(0, 4);
        while (rand == lastnum)
        {
            rand = Random.Range(0, 4);
        }
        lastnum = rand;
    }

    public void Respond()
    {
        NetworkClientUI.SendRespond(0);
        NetworkClientUI.ctl.GetComponent<CanvasGroup>().alpha = 1;
        NetworkClientUI.ctl.GetComponent<CanvasGroup>().blocksRaycasts = true;
        SceneManager.UnloadSceneAsync("Splitting Fall");
    }
}
