using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthpoint : MonoBehaviour {

    public int maxhp, hp, guard, block, mp, fp, id = -1, exp; //exp FOR ENEMY
    int currenthp, currentmp, currentfp, regenHP, regenMP, regenFP;

    public float stunned, pushed; //STATUS
    public bool damaged, spblock, isJoin;

    int tw, currenttw; //OTHER
    public bool isDeath, isGetQueue;

    Animator anim;
    GameObject nwmng, cam;

    //HEALTHPOPUP
    public GameObject dmgpopup;
    public Vector3 offset;

    //DEBUFF
    public bool dbf;
    public float healrate;

	// Use this for initialization
	void Start () {

        //MAIN
        hp = maxhp; currenthp = hp;
        mp = 10000; currentmp = mp;
        fp = 10000; currentfp = fp;

        //OTHER
        anim = GetComponent<Animator>();
        isGetQueue = isDeath = isJoin = damaged = spblock = false;
        block = 100;
        currenttw = 0;

        //NETWORK MANAGER
        nwmng = GameObject.Find("Network Manager");
        cam = GameObject.Find("Main Camera");

        dbf = false;
        healrate = 1f;
	}
	
	// Update is called once per frame
	void Update () {

        //SELECT
        if (gameObject.name == "Knight")
        {
            maxhp = (int)GetComponent<SkillInfo>().knightStat[1];
            regenHP = (int)GetComponent<SkillInfo>().knightStat[2];
            regenMP = (int)(GetComponent<SkillInfo>().knightStat[3] * 100);
            regenFP = (int)(GetComponent<SkillInfo>().knightStat[4] * 100);
            guard = (int)GetComponent<SkillInfo>().knightStat[5];
        }

        else if (gameObject.name == "Archer")
        {
            maxhp = (int)GetComponent<SkillInfo>().archerStat[1];
            regenHP = (int)GetComponent<SkillInfo>().archerStat[2];
            regenMP = (int)(GetComponent<SkillInfo>().archerStat[3] * 100);
            regenFP = (int)(GetComponent<SkillInfo>().archerStat[4] * 100);
            guard = (int)GetComponent<SkillInfo>().archerStat[5];
        }

        else if (gameObject.name == "Warchief")
        {
            maxhp = (int)GetComponent<SkillInfo>().warchiefStat[1];
            regenHP = (int)GetComponent<SkillInfo>().warchiefStat[2];
            regenMP = (int)(GetComponent<SkillInfo>().warchiefStat[3] * 100);
            regenFP = (int)(GetComponent<SkillInfo>().warchiefStat[4] * 100);
            guard = (int)GetComponent<SkillInfo>().warchiefStat[5];
        }

        else if (gameObject.name == "Berserker")
        {
            maxhp = (int)GetComponent<SkillInfo>().berserkerStat[1];
            regenHP = (int)GetComponent<SkillInfo>().berserkerStat[2];
            regenMP = (int)(GetComponent<SkillInfo>().berserkerStat[3] * 100);
            regenFP = (int)(GetComponent<SkillInfo>().berserkerStat[4] * 100);
            guard = (int)GetComponent<SkillInfo>().berserkerStat[5];
        }

        tw = (int)Time.time;

        //BASE CASE
        if (hp > maxhp) hp = maxhp;
        if (mp > 10000) mp = 10000;
        if (fp > 10000) fp = 10000;

        //GET QUEUE
        if (id >= 0 && !isGetQueue && gameObject.tag == "Player") isGetQueue = true;

        //DEATH
        if (hp <= 0) 
        {
            //Debug.Log(id);
            if (!isDeath && gameObject.tag == "Player")
            {
                anim.SetInteger("Speed", 6);
                nwmng.GetComponent<NetworkServerUI>().SendToClientDeath(id);
                isDeath = true;
            }          
        }

        //CHANGE
        else if (currenthp != hp)
        {
            if (currenthp > hp) ShowFloatingText(currenthp - hp);
            currenthp = hp;
            damaged = true;
        }
        if (currentmp != mp) currentmp = mp;
        if (currentfp != fp) currentfp = fp;

        if (mp <= 0) mp = 0;
        if (fp <= 0) fp = 0;

        //REGEN
        if (currenttw != tw && hp > 0)
        {
            currenttw = tw;
            hp += (int)(regenHP * healrate);
            mp += regenMP;

            if (dbf) fp -= regenFP * 2;
            else fp += regenFP;
        }

        //GUARD
        if (!spblock) guard = block;
        if (stunned <= 0) stunned = 0;

        //Debug.Log(fp);

    }

    public void ReDeath()
    {
        cam.GetComponent<CameraFollow>().Recamera();
    }

    public void ShowFloatingText(int damage)
    {
        var go = Instantiate(dmgpopup, transform.position + offset, Quaternion.identity, transform);
        Component[] element = go.GetComponentsInChildren<TextMesh>();
        foreach (TextMesh t in element) t.text = damage.ToString();

    }
}
