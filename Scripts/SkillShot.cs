using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillShot : MonoBehaviour {

    public Rigidbody body;
    public float force;
    GameObject self;
    public string owner;
    public int destime, type;
    public bool isBreak = true;
    int tw, currenttw;

    // Use this for initialization
    void Start () {

        if (owner != null) gameObject.name = gameObject.name.Replace("(Clone)", "");

        if (owner == "Archer") self = GameObject.Find("Archer");
        else if (owner == "Mech") self = GameObject.Find("Mech");

        GetComponent<Rigidbody>();
        body.AddRelativeForce(Vector3.forward * force);
        currenttw = 0;
    }
	
	// Update is called once per frame
	void Update () {

        tw = (int)Time.time;

        if (type == 1) self.GetComponent<Controller>().spattack = true;

        if (currenttw != tw) { currenttw = tw; destime--; }
        if (destime <= 0)
        {
            if (type == 1) self.GetComponent<Controller>().spattack = false;
            Destroy(gameObject);
        }

    }

    public void OnTriggerEnter(Collider col)
    {
        if (((col.gameObject.tag == "Enemy" || col.gameObject.tag == "Obstacle") && type == 1) || 
            ((col.gameObject.tag == "Player" || col.gameObject.tag == "Obstacle") && type == 2))
        {
            if (isBreak)
            {
                self.GetComponent<Controller>().spattack = false;
                Destroy(gameObject);
            }
        }
    }
}
