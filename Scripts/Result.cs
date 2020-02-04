using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour {

    public GameObject[] result;
    GameObject[] players = new GameObject[4];
    GameObject nwmng;
    public string bossname;
    bool isFlag, isAns;

	// Use this for initialization
	void Start () {

        nwmng = GameObject.Find("Network Manager");
        isFlag = true;
        isAns = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (!GetFlag() && !isAns)
        {
            isAns = true;
            nwmng.GetComponent<NetworkServerUI>().SendToClientEndGame("Lose", Stats.expGain);
            StartCoroutine(Resulting("Lose"));
        }

        if (GameObject.Find(bossname).GetComponent<Healthpoint>().hp <= 0 && !isAns)
        {
            isAns = true;
            nwmng.GetComponent<NetworkServerUI>().SendToClientEndGame("Win", Stats.expGain);
            StartCoroutine(Resulting("Win"));
        }
    }

    IEnumerator Resulting(string res)
    {
        yield return new WaitForSeconds(5f);
        if (res == "Win") result[0].SetActive(true);
        else result[1].SetActive(true);
        Invoke("Returning", 3f);
    }

    public void Returning()
    {
        SceneManager.LoadScene("Demo");
    }

    public bool GetFlag()
    {
        for (int i = 0; i < 4; i++)
        {
            if (GetComponent<Checkpoint>().player[i] != null && GetComponent<Checkpoint>().player[i].GetComponent<Healthpoint>().hp > 0)
            {
                return true;
            }
        }

        return false;
    }
}
