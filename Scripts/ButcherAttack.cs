using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButcherAttack : MonoBehaviour {

    public GameObject hitScreen;
    int random;
    Animator anim;
    public int hptake;

	// Use this for initialization
	void Start () {

        anim = GetComponent<Animator>();
        random = Random.Range(1, 4); ;
	}
	
	// Update is called once per frame
	void Update () {

        anim.SetInteger("Hit", random);		
	}

    public void Hitter(int pos)
    {       
        if (AccelDodge.position == 0 || pos != AccelDodge.position)
        {
            hitScreen.SetActive(true);
            Invoke("ClearHitScreen", 0.5f);
            NetworkClientUI.SendUpdateHealth(hptake);
        }

        random = Random.Range(1, 4);
    }

    public void ClearHitScreen()
    {
        hitScreen.SetActive(false);
    }
}
