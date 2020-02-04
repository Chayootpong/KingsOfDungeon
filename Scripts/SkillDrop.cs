using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDrop : MonoBehaviour {

    public bool starter;
    public int detonate, force, hide;
    public GameObject spawned;
    public Rigidbody body;

    int currenttw, tw;

	// Use this for initialization
	void Start () {

        currenttw = 0;
        if (starter) { GetComponent<Rigidbody>(); body.AddRelativeForce(Vector3.forward * force); }

    }
	
	// Update is called once per frame
	void Update () {

        tw = (int)Time.time;

        if (currenttw != tw) { currenttw = tw; detonate--; }

        if (detonate <= 0 && starter) { Instantiate(spawned, transform.position, Quaternion.identity); Destroy(gameObject); }

        if (detonate <= 0) Destroy(gameObject);

    }

    void OnTriggerStay(Collider col)
    {
        if(!starter && col.gameObject.tag == "Enemy")
        {
            if(detonate <= hide)
            {
                col.gameObject.GetComponent<EnemyAI>().unseen = false;
                //Debug.Log("Unblocked");
            }

            else if (gameObject.name == "Smoke Bomb Spread(Clone)")
            {
                col.gameObject.GetComponent<EnemyAI>().unseen = true;
                //Debug.Log("Blocked");
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (!starter && col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<EnemyAI>().unseen = false;
            //Debug.Log("Unblocked");
        }
    }
}
