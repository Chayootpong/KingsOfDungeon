using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEDamage : MonoBehaviour {

    public int type;
    public float dmgDeal;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter(Collider other)
    {
        /*if (other.gameObject.tag == "Player" && type == 1)
        {
            other.gameObject.GetComponent<Healthpoint>().hp += (int)dmgDeal;
        }

        else if (other.gameObject.tag == "Enemy" && type == 2)
        {
            other.gameObject.GetComponent<Healthpoint>().hp += (int)dmgDeal;
        }*/

        if (other.gameObject.tag == "Player" && type == 3) //FLASH HEAL/DEAL
        {
            other.gameObject.GetComponent<Healthpoint>().hp += (int)dmgDeal;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && type == 1) //FOR HEROES
        {
            other.gameObject.GetComponent<Healthpoint>().hp += (int)dmgDeal;
        }

        else if (other.gameObject.tag == "Enemy" && type == 2) //FOR ENEMIES
        {
            other.gameObject.GetComponent<Healthpoint>().hp += (int)dmgDeal;
        }
    }
}
