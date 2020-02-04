using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirturePlayer : MonoBehaviour {

    public string playerName;

    //SKILL
    public GameObject warchiefSkill3;

    // Use this for initialization
    void Start () {

        gameObject.name = playerName = DontDestroy.selectedPlayer;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
