using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCounter : MonoBehaviour {

    public static int furyUse = 0, spellUse = 0, trailUse = 0;

	// Use this for initialization
	void Start () {

        furyUse = spellUse = trailUse = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
