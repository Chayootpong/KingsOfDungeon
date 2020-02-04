using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCount : MonoBehaviour {

    public int limit;
    int x;
    public bool isUseRespond;

    private void OnEnable()
    {
        x = 0;
    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Click()
    {
        x++;
        if (x >= limit)
        {
            if (isUseRespond) NetworkClientUI.SendRespond(0);
            GetComponent<Destroy>().DestroyButton2();
        }
    }
}
