using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour {

    public float spawnStart, spawnTime;
    public GameObject spawnThing;

	// Use this for initialization
	void Start () {

        InvokeRepeating("Spawn", spawnStart, spawnTime);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Spawn()
    {
        Instantiate(spawnThing, transform.position, transform.rotation);
    }
}
