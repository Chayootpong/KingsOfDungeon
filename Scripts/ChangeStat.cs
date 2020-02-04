using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStat : MonoBehaviour {

    ParticleSystem pal;
    public GameObject self;
    public Gradient palCol;
    float codeCol;
    public bool isEffect;

	// Use this for initialization
	void Start () {

        pal = GetComponent<ParticleSystem>();
		
	}
	
	// Update is called once per frame
	void Update () {

        ParticleSystem.MainModule psMain = pal.main;

        if (!isEffect)
        {
            codeCol = (float)self.GetComponent<Healthpoint>().hp / (float)self.GetComponent<Healthpoint>().maxhp;          
            psMain.startColor = palCol.Evaluate(codeCol);
        }
        else
        {
            psMain.startColor = palCol.Evaluate(1f);
        }
        //Debug.Log(codeCol);     

	}
}
