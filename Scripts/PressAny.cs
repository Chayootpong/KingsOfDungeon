using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAny : MonoBehaviour {

    public GameObject uiManage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if (Input.anyKey)
        {
            uiManage.GetComponent<UIManager>().TapToStart();
        }

	}
}
