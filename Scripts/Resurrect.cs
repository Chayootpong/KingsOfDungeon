using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resurrect : MonoBehaviour {

    GameObject nwmng;

    public void Start()
    {
        nwmng = GameObject.Find("Network Manager");
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && col.GetComponent<Healthpoint>().hp <= 0)
        {
            nwmng.GetComponent<NetworkServerUI>().SendToClientResurrection("Yes", GetComponent<ChangeStat>().self.GetComponent<Healthpoint>().id, col.GetComponent<Healthpoint>().id);
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player" && col.GetComponent<Healthpoint>().hp <= 0)
        {
            nwmng.GetComponent<NetworkServerUI>().SendToClientResurrection("No", GetComponent<ChangeStat>().self.GetComponent<Healthpoint>().id, col.GetComponent<Healthpoint>().id);
        }
    }
}
