using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint2 : MonoBehaviour {

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
        Instantiate(spawnThing, new Vector3(transform.position.x + Random.Range(-2, 3), transform.position.y + Random.Range(-2, 3), transform.position.z + Random.Range(-2, 3)), transform.rotation);
    }
}
