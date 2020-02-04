using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Controller : MonoBehaviour {

    public float turnspeed = 10f, speed, angle;
    float normalspeed, rushspeed, interval = 0f;
    public bool isatk, spattack, reversemove;
    public GameObject weapon, weapon2;
    public string charac, status, range;
    bool death;
    GameObject nwmng;

    public Animator anim;
    Rigidbody body;
    Vector3 jump = new Vector3(0f, 5f, 5f); //JUMP FIX FORWARD

    int tw, currenttw;
    float countdowntw;

    bool respond;
    public bool recres;
    public string restext;
    public GameObject spawnRes;

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //SKILL SETTING
    public GameObject[] assets;

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //INPUT KEY
    float x, y;
    bool atk, fr1, fr2, fr3, sk1, sk2, sk3, tl;

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //SOUND
    public AudioClip[] sAction; //0 = Attack 1 = Move
    AudioSource source;
    int checkSound;

    // Use this for initialization
    void Start() {

        if(gameObject.name == "Knight")
        {
            normalspeed = GetComponent<SkillInfo>().knightStat[7];
            rushspeed = GetComponent<SkillInfo>().knightStat[8];
            range = "MELEE";
        }

        else if (gameObject.name == "Archer")
        {
            normalspeed = GetComponent<SkillInfo>().archerStat[7];
            rushspeed = GetComponent<SkillInfo>().archerStat[8];
            range = "RANGE";
        }

        else if (gameObject.name == "Warchief")
        {
            normalspeed = GetComponent<SkillInfo>().warchiefStat[7];
            rushspeed = GetComponent<SkillInfo>().warchiefStat[8];
            range = "MELEE";
        }

        else if (gameObject.name == "Berserker")
        {
            normalspeed = GetComponent<SkillInfo>().berserkerStat[7];
            rushspeed = GetComponent<SkillInfo>().berserkerStat[8];
            range = "MELEE";
        }

        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
        speed = normalspeed;
        isatk = false;
        death = false;
        respond = false;
        recres = false;
        restext = "1";
        currenttw = 0;
        countdowntw = 2f;
        nwmng = GameObject.Find("Network Manager");
    }

    // Update is called once per frame
    void Update()
    {
        tw = (int)Time.time;

        //RESPOND SKILL
        if (respond) Respond();
        //Debug.Log("Respond : " + respond);
        //Debug.Log("Recres : " + recres);

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //INPUT CONTROLLER
        if (gameObject.tag == "Player")
        {
            int id = GetComponent<Healthpoint>().id;
            x = CrossPlatformInputManager.GetAxis("Horizontal_" + id);
            y = CrossPlatformInputManager.GetAxis("Vertical_" + id);
            atk = CrossPlatformInputManager.GetButtonDown("Attack_" + id);
            fr1 = CrossPlatformInputManager.GetButtonDown("Fury_1_" + id);
            fr2 = CrossPlatformInputManager.GetButtonDown("Fury_2_" + id);
            fr3 = CrossPlatformInputManager.GetButtonDown("Fury_3_" + id);
            sk1 = CrossPlatformInputManager.GetButtonDown("Skill_1_" + id);
            sk2 = CrossPlatformInputManager.GetButtonDown("Skill_2_" + id);
            sk3 = CrossPlatformInputManager.GetButtonDown("Skill_3_" + id);
            tl = CrossPlatformInputManager.GetButtonDown("Trail_" + id);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //CONTROL

        if (GetComponent<Healthpoint>().hp <= 0) death = true;

        else if (x != 0.0f || y != 0.0f)
        {
            if (reversemove &&
                !anim.GetCurrentAnimatorStateInfo(0).IsName("Block") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Trail") &&
                !anim.GetCurrentAnimatorStateInfo(0).IsName("Combo01") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Combo02") &&
                !anim.GetCurrentAnimatorStateInfo(0).IsName("Combo03") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Combo04") &&
                !anim.GetCurrentAnimatorStateInfo(0).IsName("Combo05"))
            {
                angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
                if (angle < 0) angle += 360;

                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 495 - angle - 180, 0), Time.deltaTime * turnspeed);
                transform.Translate(Vector3.back * speed * Time.deltaTime);

                anim.SetInteger("Speed", 7); speed = rushspeed * 0.75f;
            }

            else if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack01") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack02") &&
                !anim.GetCurrentAnimatorStateInfo(0).IsName("Block") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Trail") &&
                !anim.GetCurrentAnimatorStateInfo(0).IsName("Combo01") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Combo02") &&
                !anim.GetCurrentAnimatorStateInfo(0).IsName("Combo03") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Combo04") &&
                !anim.GetCurrentAnimatorStateInfo(0).IsName("Combo05") && !respond)
            {
                angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
                if (angle < 0) angle += 360;

                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 495 - angle, 0), Time.deltaTime * turnspeed);
                transform.Translate(Vector3.forward * speed * Time.deltaTime);

                anim.SetInteger("Speed", 2); speed = rushspeed;

                /*if (ljb && GetComponent<Healthpoint>().fp >= 1000)
                {
                    anim.SetInteger("Speed", 2); speed = rushspeed;
                    if (currenttw != tw)
                    {
                        currenttw = tw;
                        if (gameObject.name == "Knight") GetComponent<Healthpoint>().fp -= (int)GetComponent<SkillInfo>().knightStat[6] * 100;
                        else if (gameObject.name == "Archer") GetComponent<Healthpoint>().fp -= (int)GetComponent<SkillInfo>().archerStat[6] * 100;
                    }
                }
                else { anim.SetInteger("Speed", 1); speed = normalspeed; }*/
            }
        }

        else if ((atk && !reversemove) || spattack)
        {
            if (range != "RANGE") if (!source.isPlaying) PlayForceSound(sAction[0]);

            if (gameObject.name == "Knight")
            {
                anim.SetInteger("Speed", Random.Range(3, 5));
                anim.SetFloat("Accel", 1f);
            }

            else if (gameObject.name == "Archer" && Time.time * 100 >= interval && atk)
            {
                interval = Time.time * 100 + 50;
                Instantiate(assets[0], assets[1].transform.position, assets[1].transform.rotation);
            }

            else if (gameObject.name == "Warchief")
            {
                if (range == "MELEE") anim.SetInteger("Speed", 3);
                else if (range == "RANGE" && Time.time * 100 >= interval)
                {
                    anim.SetInteger("Speed", 4);
                    interval = Time.time * 100 + 50;
                    //Instantiate(assets[0], assets[1].transform.position, assets[1].transform.rotation);
                }
                anim.SetFloat("Accel", 1f);
            }

            else if (gameObject.name == "Berserker")
            {
                anim.SetInteger("Speed", Random.Range(3, 5));
                anim.SetFloat("Accel", 1f);
            }

            else
            {
                anim.SetInteger("Speed", -1);
                anim.SetFloat("Accel", 1f);
            }
        }

        //else if (rb) anim.SetInteger("Speed", 5);

        else anim.SetInteger("Speed", -1);

        if (atk && reversemove && !anim.GetCurrentAnimatorStateInfo(0).IsName("Block"))
        {
            anim.SetInteger("Speed", Random.Range(3, 5));
            anim.SetFloat("Accel", 1f);

            if (gameObject.name == "Archer" && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack01") &&
                !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack02") && Time.time * 100 >= interval)
            {
                interval = Time.time * 100 + 50;
                Instantiate(assets[0], assets[1].transform.position, assets[1].transform.rotation);
            }
        }

        if (spattack) isatk = true;
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack01") || anim.GetCurrentAnimatorStateInfo(0).IsName("Attack02") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("Combo01") || anim.GetCurrentAnimatorStateInfo(0).IsName("Combo02") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("Combo03") || anim.GetCurrentAnimatorStateInfo(0).IsName("Combo04") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("Combo05")) isatk = true;
        else isatk = false;

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //STATUS
        if (status != null)
        {
            Stats.effectGain[GetComponent<Healthpoint>().id]++;
            if (status.Contains("Hack")) nwmng.GetComponent<NetworkServerUI>().SendToClientStatus(GetComponent<Healthpoint>().id, "Hack", status.Remove(0, 4));
            else if (status.Contains("Butch")) nwmng.GetComponent<NetworkServerUI>().SendToClientStatus(GetComponent<Healthpoint>().id, "Butch", status.Remove(0, 5));
            status = null;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //SKILL

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack01") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack02") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Block") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Trail") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Combo01") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Combo02") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Combo03") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Combo04") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Combo05") && !death)
        {
            if (charac == "Knight")
            {
                if (fr1 && GetComponent<Healthpoint>().fp >= (int)(GetComponent<SkillInfo>().knightMFC[0] * 100)) //FURY01
                {
                    anim.Play("Combo01");
                    GetComponent<Healthpoint>().fp -= (int)(GetComponent<SkillInfo>().knightMFC[0] * 100);
                    PlayForceSound(sAction[2]);
                    Stats.furyGain[GetComponent<Healthpoint>().id]++;
                }

                else if (sk1 /*&& GetComponent<Healthpoint>().mp >= (int)(GetComponent<SkillInfo>().knightMFC[3] * 100)*/ &&
                    GetComponent<SkillInfo>().knightCD[0, 0] == GetComponent<SkillInfo>().knightCD[0, 1]) //SKILL01
                {
                    anim.Play("Combo04");
                    GetComponent<Healthpoint>().mp -= (int)(GetComponent<SkillInfo>().knightMFC[3] * 100);
                    GetComponent<SkillInfo>().knightCD[0, 0] = 0;
                    PlayForceSound(sAction[3]);
                    Stats.spellGain[GetComponent<Healthpoint>().id]++;
                }

                else if (fr2 && GetComponent<Healthpoint>().fp >= (int)(GetComponent<SkillInfo>().knightMFC[1] * 100)) //FURY02
                {
                    Invoke("DelayActi", 1.25f);
                    weapon.GetComponent<Attack>().power = 10f;
                    weapon.GetComponent<Attack>().radius = 2f;
                    weapon.GetComponent<Attack>().upforce = 1f;
                    body.AddRelativeForce(jump, ForceMode.Impulse);
                    anim.Play("Combo02");
                    GetComponent<Healthpoint>().fp -= (int)(GetComponent<SkillInfo>().knightMFC[1] * 100);
                    checkSound = 1;
                    Stats.furyGain[GetComponent<Healthpoint>().id]++;
                }

                else if (sk2 /*&& GetComponent<Healthpoint>().mp >= (int)(GetComponent<SkillInfo>().knightMFC[4] * 100)*/ &&
                    GetComponent<SkillInfo>().knightCD[1, 0] == GetComponent<SkillInfo>().knightCD[1, 1]) //SKILL02
                {
                    Invoke("DelayActi", 1.75f);
                    weapon.GetComponent<Attack>().power = 13f;
                    weapon.GetComponent<Attack>().radius = 5f;
                    weapon.GetComponent<Attack>().upforce = 1f;
                    anim.Play("Combo03");
                    GetComponent<Healthpoint>().mp -= (int)(GetComponent<SkillInfo>().knightMFC[4] * 100);
                    GetComponent<SkillInfo>().knightCD[1, 0] = 0;
                    checkSound = 2;
                    Stats.spellGain[GetComponent<Healthpoint>().id]++;
                }

                else if (recres) //FURY03
                {
                    respond = true;
                    spattack = true;
                    GetComponent<Healthpoint>().dbf = true;
                    if (spawnRes == null)
                    {
                        spawnRes = Instantiate(assets[1], assets[2].transform.position, assets[2].transform.rotation);
                    }
                }

                else if (sk3 /*&& GetComponent<Healthpoint>().mp >= (int)(GetComponent<SkillInfo>().knightMFC[4] * 100)*/ &&
                    GetComponent<SkillInfo>().knightCD[2, 0] == GetComponent<SkillInfo>().knightCD[2, 1]) //SKILL03
                {
                    anim.Play("Combo05");
                    GetComponent<Healthpoint>().mp -= (int)(GetComponent<SkillInfo>().knightMFC[5] * 100);
                    Instantiate(assets[3], assets[4].transform.position, assets[4].transform.rotation);
                    GetComponent<SkillInfo>().knightCD[2, 0] = 0;
                    Stats.spellGain[GetComponent<Healthpoint>().id]++;
                }
            }

            else if (charac == "Archer" && !reversemove)
            {
                if (fr1 && GetComponent<Healthpoint>().fp >= (int)(GetComponent<SkillInfo>().archerMFC[0] * 100)) //FURY01
                {
                    anim.Play("Combo02");
                    GetComponent<Healthpoint>().fp -= (int)(GetComponent<SkillInfo>().archerMFC[0] * 100);
                    Stats.furyGain[GetComponent<Healthpoint>().id]++;
                }

                else if (sk2 /*&& GetComponent<Healthpoint>().mp >= (int)(GetComponent<SkillInfo>().archerMFC[4] * 100)*/ &&
                    GetComponent<SkillInfo>().archerCD[1, 0] == GetComponent<SkillInfo>().archerCD[1, 1]) //SKILL02
                {
                    anim.Play("Combo01");
                    Instantiate(assets[3], assets[1].transform.position, assets[1].transform.rotation);
                    transform.Rotate(0, -90, 0, Space.Self);
                    GetComponent<Healthpoint>().mp -= (int)(GetComponent<SkillInfo>().archerMFC[4] * 100);
                    Stats.spellGain[GetComponent<Healthpoint>().id]++;
                }

                else if (fr2 &&
                    GetComponent<Healthpoint>().fp >= (int)(GetComponent<SkillInfo>().archerMFC[1] * 100)) //FURY02
                {
                    anim.Play("Attack01");
                    Instantiate(assets[4], assets[1].transform.position, assets[1].transform.rotation);
                    GetComponent<Healthpoint>().fp -= (int)(GetComponent<SkillInfo>().archerMFC[1] * 100);
                    Stats.furyGain[GetComponent<Healthpoint>().id]++;
                }

                else if (sk1 /*&& GetComponent<Healthpoint>().mp >= (int)(GetComponent<SkillInfo>().archerMFC[3] * 100)*/ &&
                    GetComponent<SkillInfo>().archerCD[0, 0] == GetComponent<SkillInfo>().archerCD[0, 1]) //SKILL01
                {
                    anim.Play("Combo03");
                    GetComponent<Healthpoint>().mp -= (int)(GetComponent<SkillInfo>().archerMFC[3] * 100);
                    GetComponent<SkillInfo>().archerCD[0, 0] = 0;
                    Instantiate(assets[2], assets[1].transform.position, assets[1].transform.rotation);
                    Stats.spellGain[GetComponent<Healthpoint>().id]++;
                }

                else if (fr3 &&
                    GetComponent<Healthpoint>().fp >= (int)(GetComponent<SkillInfo>().archerMFC[2] * 100)) //FURY03
                {
                    anim.Play("Attack01");
                    Instantiate(assets[5], assets[1].transform.position, assets[1].transform.rotation);
                    GetComponent<Healthpoint>().fp -= (int)(GetComponent<SkillInfo>().archerMFC[2] * 100);
                    Stats.furyGain[GetComponent<Healthpoint>().id]++;
                }

                else if (sk3 /*&& GetComponent<Healthpoint>().mp >= (int)(GetComponent<SkillInfo>().archerMFC[3] * 100)*/ &&
                    GetComponent<SkillInfo>().archerCD[2, 0] == GetComponent<SkillInfo>().archerCD[2, 1]) //SKILL03
                {
                    anim.Play("Attack01");
                    GetComponent<Healthpoint>().mp -= (int)(GetComponent<SkillInfo>().archerMFC[5] * 100);
                    GetComponent<SkillInfo>().archerCD[2, 0] = 0;
                    spawnRes = null;
                    spawnRes = Instantiate(assets[6], assets[1].transform.position, assets[1].transform.rotation);
                    respond = true;
                    Stats.spellGain[GetComponent<Healthpoint>().id]++;
                }
            }

            else if (charac == "Warchief")
            {
                if (fr1 && GetComponent<Healthpoint>().fp >= (int)(GetComponent<SkillInfo>().warchiefMFC[0] * 100)) //FURY01
                {
                    anim.Play("Combo01");
                    GetComponent<Healthpoint>().fp -= (int)(GetComponent<SkillInfo>().warchiefMFC[0] * 100);
                    PlayForceSound(sAction[2]);
                    Stats.furyGain[GetComponent<Healthpoint>().id]++;
                }

                else if (sk1 && GetComponent<SkillInfo>().warchiefCD[0, 0] == GetComponent<SkillInfo>().warchiefCD[0, 1]) //SKILL01
                {
                    anim.Play("Combo02");
                    Instantiate(assets[1], assets[0].transform.position, assets[0].transform.rotation);
                    GetComponent<Healthpoint>().mp -= (int)(GetComponent<SkillInfo>().warchiefMFC[3] * 100);
                    GetComponent<SkillInfo>().warchiefCD[0, 0] = 0;
                    PlayForceSound(sAction[3]);
                    Stats.spellGain[GetComponent<Healthpoint>().id]++;
                }

                else if (fr2 && GetComponent<Healthpoint>().fp >= (int)(GetComponent<SkillInfo>().warchiefMFC[1] * 100)) //FURY02
                {
                    anim.Play("Combo03");
                    GetComponent<Healthpoint>().fp -= (int)(GetComponent<SkillInfo>().warchiefMFC[1] * 100);
                    PlayForceSound(sAction[4]);
                    Stats.furyGain[GetComponent<Healthpoint>().id]++;
                }

                else if (sk2 && GetComponent<SkillInfo>().warchiefCD[1, 0] == GetComponent<SkillInfo>().warchiefCD[1, 1]) //SKILL02
                {
                    Instantiate(assets[2], transform.position, transform.rotation);
                    GetComponent<Healthpoint>().mp -= (int)(GetComponent<SkillInfo>().warchiefMFC[4] * 100);
                    GetComponent<SkillInfo>().warchiefCD[1, 0] = 0;
                    Stats.spellGain[GetComponent<Healthpoint>().id]++;
                }

                else if (fr3 && GetComponent<Healthpoint>().fp >= (int)(GetComponent<SkillInfo>().warchiefMFC[2] * 100)) //FURY03
                {
                    GetComponent<SkillInfo>().warchiefBuff[2, 0] = 0;
                    GetComponent<Healthpoint>().fp -= (int)(GetComponent<SkillInfo>().warchiefMFC[2] * 100);
                    PlayForceSound(sAction[5]);
                    Stats.furyGain[GetComponent<Healthpoint>().id]++;
                }

                else if (sk3 && GetComponent<SkillInfo>().warchiefCD[2, 0] == GetComponent<SkillInfo>().warchiefCD[2, 1]) //SKILL03
                {
                    GetComponent<Healthpoint>().mp -= (int)(GetComponent<SkillInfo>().warchiefMFC[5] * 100);
                    GetComponent<SkillInfo>().warchiefCD[2, 0] = 0;
                    Stats.spellGain[GetComponent<Healthpoint>().id]++;
                }
            }

            else if (charac == "Berserker")
            {
                if (fr1 && GetComponent<Healthpoint>().fp >= (int)(GetComponent<SkillInfo>().berserkerMFC[0] * 100)) //FURY01
                {
                    anim.Play("Combo01");
                    GetComponent<Healthpoint>().fp -= (int)(GetComponent<SkillInfo>().berserkerMFC[0] * 100);
                    PlayForceSound(sAction[2]);
                    Stats.furyGain[GetComponent<Healthpoint>().id]++;
                }

                else if (sk1 && GetComponent<SkillInfo>().berserkerCD[0, 0] == GetComponent<SkillInfo>().berserkerCD[0, 1]) //SKILL01
                {
                    anim.Play("Combo03");
                    Instantiate(assets[4], assets[3].transform.position, assets[3].transform.rotation);
                    GetComponent<SkillInfo>().berserkerCD[0, 0] = 0;
                    PlayForceSound(sAction[3]);
                    Stats.spellGain[GetComponent<Healthpoint>().id]++;
                }

                else if (fr2 && GetComponent<Healthpoint>().fp >= (int)(GetComponent<SkillInfo>().berserkerMFC[1] * 100)) //FURY02
                {
                    GetComponent<Healthpoint>().fp -= (int)(GetComponent<SkillInfo>().berserkerMFC[1] * 100);
                    Stats.furyGain[GetComponent<Healthpoint>().id]++;
                }

                else if (sk2 && GetComponent<SkillInfo>().berserkerCD[1, 0] == GetComponent<SkillInfo>().berserkerCD[1, 1]) //SKILL02
                {
                    anim.Play("Combo02");
                    GetComponent<SkillInfo>().berserkerCD[1, 0] = 0;
                    PlayForceSound(sAction[4]);
                    Stats.spellGain[GetComponent<Healthpoint>().id]++;
                }

                else if (fr3 && GetComponent<Healthpoint>().fp >= (int)(GetComponent<SkillInfo>().berserkerMFC[2] * 100)) //FURY03
                {
                    anim.Play("Combo05");
                    Instantiate(assets[2], transform.position, transform.rotation);
                    GetComponent<Healthpoint>().fp -= (int)(GetComponent<SkillInfo>().berserkerMFC[2] * 100);
                    Stats.furyGain[GetComponent<Healthpoint>().id]++;
                }

                else if (sk3 && GetComponent<SkillInfo>().berserkerCD[2, 0] == GetComponent<SkillInfo>().berserkerCD[2, 1]) //SKILL03
                {
                    spawnRes = Instantiate(assets[1], assets[0].transform.position, assets[0].transform.rotation);
                    GetComponent<SkillInfo>().berserkerCD[2, 0] = 0;
                    Stats.spellGain[GetComponent<Healthpoint>().id]++;
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //MOVE COMBO

        if (charac == "Knight")
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Combo01")) //Fury01
            {
                transform.Translate(Vector3.forward * 2 * Time.deltaTime);
                weapon.GetComponent<Attack>().muldmg = 1.5f;
            }

            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Combo02")) //Fury02
            {
                weapon.GetComponent<Attack>().muldmg = 2f;
            }

            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Combo03")) //Skill02
            {
                weapon.GetComponent<Attack>().muldmg = 1.5f;
            }

            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Combo04")) //Skill01
            {
                weapon.GetComponent<Attack>().muldmg = 1.5f;
                weapon.GetComponent<Attack>().stun = 2.5f;
                weapon.GetComponent<Attack>().push = 3f;
                assets[0].SetActive(true);
            }

            else
            {
                weapon.GetComponent<Attack>().muldmg = 1f;
                weapon.GetComponent<Attack>().stun = 0f;
                weapon.GetComponent<Attack>().push = 0f;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //BUFF
            if (GetComponent<SkillInfo>().knightBuff[6, 0] < GetComponent<SkillInfo>().knightBuff[6, 1])
            {
                GetComponent<Healthpoint>().block = 0;
                GetComponent<Healthpoint>().spblock = true;
            }
            else
            {
                GetComponent<Healthpoint>().block = (int)GetComponent<SkillInfo>().knightStat[5];
                GetComponent<Healthpoint>().spblock = false;
            }
        }

        else if (charac == "Archer" && !reversemove)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Combo01")) //Skill02
            {
                assets[3].GetComponent<Attack>().muldmg = 0.5f;
                assets[3].GetComponent<Attack>().stun = 3f;
            }

            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Combo02")) //Fury01
            {
                transform.Translate(Vector3.forward * 5 * Time.deltaTime);
            }

            /*else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Combo03")) //Skill01
            {

            }*/

            else
            {
                weapon.GetComponent<Attack>().muldmg = 1f;
                weapon.GetComponent<Attack>().stun = 0f;
                weapon.GetComponent<Attack>().push = 0f;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //BUFF
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Combo02"))
            {
                GetComponent<Healthpoint>().block = 0;
                GetComponent<Healthpoint>().spblock = true;
            }
            else
            {
                GetComponent<Healthpoint>().block = (int)GetComponent<SkillInfo>().archerStat[5];
                GetComponent<Healthpoint>().spblock = false;
            }
        }

        else if (charac == "Warchief")
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Combo01") || anim.GetCurrentAnimatorStateInfo(0).IsName("Combo04") ||
                anim.GetCurrentAnimatorStateInfo(0).IsName("Combo05")) //Fury01
            {
                weapon.GetComponent<Attack>().muldmg = 1.5f;
            }

            /*else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Combo02")) //Skill01
            {
                
            }*/

            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Combo03")) //Fury02
            {
                weapon2.GetComponent<Attack>().muldmg = 3f;
            }

            else if (recres) respond = true;

            else
            {
                weapon.GetComponent<Attack>().muldmg = 1f;
                weapon.GetComponent<Attack>().stun = 0f;
                weapon.GetComponent<Attack>().push = 0f;
                weapon2.GetComponent<Attack>().muldmg = 1f;
                weapon2.GetComponent<Attack>().stun = 0f;
                weapon2.GetComponent<Attack>().push = 0f;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //BUFF
            if (GetComponent<SkillInfo>().warchiefBuff[2, 0] < GetComponent<SkillInfo>().warchiefBuff[2, 1])
            {
                anim.SetFloat("Accel", 1.5f);
                rushspeed = GetComponent<SkillInfo>().warchiefStat[8] + 1.5f;
            }
            else
            {
                anim.SetFloat("Accel", 1f);
                rushspeed = GetComponent<SkillInfo>().warchiefStat[8];
            }
        }

        else if (charac == "Berserker")
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Combo02")) //Skill02
            {
                transform.Translate(Vector3.forward * 5.5f * Time.deltaTime);
                weapon.GetComponent<Attack>().muldmg = 2f;
                weapon2.GetComponent<Attack>().muldmg = 2f;
            }

            else if (recres) respond = true;

            else if (spawnRes != null) spattack = true;

            else if (spawnRes == null) spattack = false;

            else
            {
                weapon.GetComponent<Attack>().muldmg = 1f;
                weapon.GetComponent<Attack>().stun = 0f;
                weapon.GetComponent<Attack>().push = 0f;
                weapon2.GetComponent<Attack>().muldmg = 1f;
                weapon2.GetComponent<Attack>().stun = 0f;
                weapon2.GetComponent<Attack>().push = 0f;
            }

            if (GetComponent<Healthpoint>().hp > 0) assets[5].SetActive(true);
            else assets[5].SetActive(false);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //BUFF
            if (GetComponent<SkillInfo>().berserkerBuff[2, 0] < GetComponent<SkillInfo>().berserkerBuff[2, 1])
            {

            }
            else
            {

            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //TRAIL
        if (tl && !death)
        {
            if (charac == "Knight" && GetComponent<SkillInfo>().knightCD[3, 0] == GetComponent<SkillInfo>().knightCD[3, 1])
            {
                anim.Play("Trail");
                GetComponent<SkillInfo>().knightCD[3, 0] = 0;
                GetComponent<SkillInfo>().knightBuff[6, 0] = 0;
                PlayForceSound(sAction[6]);
            }

            else if (charac == "Archer" && GetComponent<SkillInfo>().archerCD[3, 0] == GetComponent<SkillInfo>().archerCD[3, 1])
            {
                anim.Play("Trail");
                GetComponent<SkillInfo>().archerCD[3, 0] = 0;
                GetComponent<SkillInfo>().archerBuff[6, 0] = 0;
                if (!reversemove) reversemove = true;
                else reversemove = false;
                Stats.trailGain[GetComponent<Healthpoint>().id]++;
            }

            else if (charac == "Warchief" && GetComponent<SkillInfo>().warchiefCD[3, 0] == GetComponent<SkillInfo>().warchiefCD[3, 1])
            {
                GetComponent<SkillInfo>().warchiefCD[3, 0] = 0;
                GetComponent<SkillInfo>().warchiefBuff[6, 0] = 0;
                if (range == "MELEE") range = "RANGE";
                else range = "MELEE";
            }

            if (charac == "Berserker" && GetComponent<SkillInfo>().berserkerCD[3, 0] == GetComponent<SkillInfo>().berserkerCD[3, 1])
            {
                GetComponent<Healthpoint>().hp += 250;
                GetComponent<SkillInfo>().berserkerCD[3, 0] = 0;
                GetComponent<SkillInfo>().berserkerBuff[6, 0] = 0;
                PlayForceSound(sAction[5]);
            }

            Stats.trailGain[GetComponent<Healthpoint>().id]++;
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //OTHERS

    public void DelayActi()
    {
        weapon.GetComponent<Attack>().acti = true;
        if (gameObject.name == "Knight")
        {
            if (checkSound == 1) PlayForceSound(sAction[4]);
            else if (checkSound == 2) PlayForceSound(sAction[5]);
        }
    }

    public void Respond()
    {
        if (gameObject.name == "Archer" && recres)
        {
            spawnRes.GetComponent<Attack>().muldmg = float.Parse(restext);
            spawnRes.GetComponent<SkillShot>().enabled = true;
            PlayForceSound(sAction[2]);
            respond = recres = false;
        }

        if (gameObject.name == "Knight")
        {
            if (!recres)
            {
                countdowntw -= Time.deltaTime;
                if (countdowntw <= 0 || GetComponent<Healthpoint>().fp < 2500)
                {
                    respond = recres = false;
                    Destroy(spawnRes);
                    spattack = false;
                    GetComponent<Healthpoint>().dbf = false;
                }
            }
            else countdowntw = 1;
        }

        if (gameObject.name == "Warchief")
        {
            Instantiate(assets[4], transform.position, transform.rotation);
            respond = recres = false;
        }

        if (gameObject.name == "Berserker")
        {
            Destroy(spawnRes);
            respond = recres = false;
        }
    }

    public void PlayForceSound(AudioClip track)
    {
        source.clip = track;
        source.Play();
    }
}

