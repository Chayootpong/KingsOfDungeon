using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clinking : MonoBehaviour {

    bool isClink = false;
    public float clinkDistance, clinkSpeed;
    public int clinkCount;

    GameObject currentEnemy;
    GameObject[] allEnemies; public GameObject[] enmInRange;

	// Use this for initialization
	void Start () {

        isClink = false;
        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        enmInRange = new GameObject[allEnemies.Length];
        currentEnemy = null;
    }
	
	// Update is called once per frame
	void Update () {

        if (isClink)
        {
            if (Vector3.Distance(currentEnemy.transform.position, transform.position) > clinkDistance || clinkCount == 0) Destroy(gameObject);
            if (Vector3.Distance(currentEnemy.transform.position, transform.position) < 0.1f) FindInRange();
            try
            {
                transform.position = Vector3.MoveTowards(transform.position, currentEnemy.transform.position, Time.deltaTime * clinkSpeed);
            }
            catch { }
        }
		
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy" && !isClink)
        {
            isClink = true;
            currentEnemy = col.gameObject;
            RemoveRig();
        }

        //if (col.gameObject.tag == "Enemy") Debug.Log("HIT!!!");
    }

    public void RemoveRig()
    {
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }

    public void FindInRange()
    {
        int i = 0, count = 0;
        for (int j = 0; j < enmInRange.Length; j++) enmInRange[j] = null;

        clinkCount--;

        foreach (GameObject enm in allEnemies)
        {
            float distance = Vector3.Distance(allEnemies[i].transform.position, transform.position);
            if (distance < clinkDistance && distance > 0.1f)
            {
                enmInRange[count] = enm;
                count++;
            }
            i++; 
        }

        currentEnemy = enmInRange[Random.Range(0, count)];

        if (enmInRange[0] == null) Destroy(gameObject);
    }
}
