using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeImg : MonoBehaviour {

    GameObject player;
    Image img;
    public Sprite[] rb;
    Sprite srb;
    float fadetime;
    int injured; //0 = stay 1 = trigger

    // Use this for initialization
    void Start () {

        img = GetComponent<Image>();
        //player = GameObject.FindGameObjectWithTag("Player");
        injured = 0;
        fadetime = 2f;

    }
	
	// Update is called once per frame
	void Update () {

        //Debug.Log(fadetime);

        if (gameObject.name == "Main Red Border") { player = GameObject.FindGameObjectWithTag("Main Player"); srb = rb[1]; }
        else if (gameObject.name == "Sub Red Border") { player = GameObject.FindGameObjectWithTag("Sub Player"); srb = rb[2]; }

        if (injured != 2 && (float)player.GetComponent<Healthpoint>().hp / (float)player.GetComponent<Healthpoint>().maxhp <= 0.15f) injured = 1;
        else if ((float) player.GetComponent<Healthpoint>().hp / (float)player.GetComponent<Healthpoint>().maxhp >= 0.25f) injured = 0;

        if (injured == 2)
        {
            img.sprite = srb;
            if (fadetime >= 1) img.CrossFadeAlpha(0f, 1f, false);
            else if (fadetime < 1) img.CrossFadeAlpha(1f, 1f, false);

            if (fadetime <= 0) fadetime = 2;
            fadetime -= Time.deltaTime;
        }

        else if (injured == 1) { fadetime = 2; injured = 2; }
        else if (injured == 0) img.sprite = rb[0];
    }
}
