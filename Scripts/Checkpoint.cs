using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public GameObject[] stand;
    public GameObject[] player = new GameObject[4];
    public GameObject nwmng;

    public string[] playName = new string[4];
    bool[] playNameCheck = new bool[4];
    public int maxCount, sendID, waitToSpawn;
    public static bool sendStart;

    //public bool isOutTime = false;

	// Use this for initialization
	void Start () {

        Invoke("DelaySpawn", waitToSpawn);

        for (int i = 0; i < 4; i++)
        {
            playName[i] = null;
            playNameCheck[i] = false;
        }

        sendStart = false;
    }

    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            if (playName[i] != null && playNameCheck[i] == false)
            {
                playNameCheck[i] = true;
                nwmng.GetComponent<NetworkServerUI>().SendToClientIDClient(sendID);
                sendID++;
            }
        }
    }

    public void DelaySpawn()
    { 
        for (int i = 0; i < 4; i++)
        {
            if (player[i] == null && playName[i] != null)
            {
                if (playName[i] == "Knight")
                {
                    player[i] = Instantiate(stand[0], new Vector3(transform.position.x + Random.Range(-2, 2), transform.position.y,
                                transform.position.z + Random.Range(-2, 2)), transform.rotation);
                }

                else if (playName[i] == "Archer")
                {
                    player[i] = Instantiate(stand[1], new Vector3(transform.position.x + Random.Range(-2, 2), transform.position.y,
                                transform.position.z + Random.Range(-2, 2)), transform.rotation);
                }

                else if (playName[i] == "Warchief")
                {
                    player[i] = Instantiate(stand[2], new Vector3(transform.position.x + Random.Range(-2, 2), transform.position.y,
                                transform.position.z + Random.Range(-2, 2)), transform.rotation);
                }

                else if (playName[i] == "Berserker")
                {
                    player[i] = Instantiate(stand[3], new Vector3(transform.position.x + Random.Range(-2, 2), transform.position.y,
                                transform.position.z + Random.Range(-2, 2)), transform.rotation);
                }

                player[i].name = player[i].name.Replace("(Clone)", "");
                player[i].tag = "Player";
                player[i].GetComponent<Healthpoint>().hp = player[i].GetComponent<Healthpoint>().maxhp;
                player[i].GetComponent<Healthpoint>().id = maxCount;
                maxCount++;
            }
        }

        GetComponent<Result>().enabled = true;

        if (!sendStart)
        {
            nwmng.GetComponent<NetworkServerUI>().SendToClientStartGame();
            sendStart = true;
            for (int i = 0; i < 4; i++)
            {
                Stats.expGain[i] = Stats.killGain[i] = Stats.reviveGain[i] = Stats.beReviveGain[i] = Stats.reviveGain[i] = Stats.furyGain[i] = Stats.spellGain[i] = Stats.trailGain[i] = Stats.trailGain[i] = 0;
            }
        }

    }
}
