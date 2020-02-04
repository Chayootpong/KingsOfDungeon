using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffer : MonoBehaviour {

    public bool isitem;
    public int health, mana;
    bool destroyed;

	// Use this for initialization
	void Start () {

        destroyed = false;

	}
	
	// Update is called once per frame
	void Update () {

	}

    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Main Player" || col.gameObject.tag == "Sub Player")
        {
            if (col.gameObject.GetComponent<Healthpoint>().hp < col.gameObject.GetComponent<Healthpoint>().maxhp)
            {
                col.gameObject.GetComponent<Healthpoint>().hp += health;
                destroyed = true;
            }

            if (col.gameObject.GetComponent<Healthpoint>().mp < 10000)
            {
                col.gameObject.GetComponent<Healthpoint>().mp += mana;
                destroyed = true;
            }

            Stats.relicGain[col.GetComponent<Healthpoint>().id]++;
        }

        if (isitem && destroyed) Destroy(gameObject);
    }
}
