using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {

    float angle, distance, rotate, realang, x, z, timehide, timedie, timewait;

    public float disatk, walkspeed, rushspeed;
    public bool isatk, iswalk, isBoss, isSubBoss;
    float[] disall = new float[4];

    NavMeshHit hit;
    public bool blocked, seen, unseen;
    public Transform[] waypoint;
    Transform nowwaypoint;

    public GameObject healthring;
    GameObject selectedpy, checkpoint;
    GameObject[] player = new GameObject[4];
    NavMeshAgent agent;
    Animator anim;
    Rigidbody body;
    public AudioClip[] sAction; //0 = Attack 1 = Move
    AudioSource source;

    public float limitin, limitout, limitdis;

    bool isFinishAnim = true, isTrigger = false;
    int tw, currenttw, loop;

    //ABILITY
    public bool isBig = false;

    //ATTACK ASSETS
    public GameObject[] assets;

	// Use this for initialization
	void Start () {

        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        healthring.SetActive(false);
        isTrigger = seen = isatk = false;
        timehide = 3;
        timedie = 12;
        timewait = 5;
        currenttw = 0;
        if (iswalk) nowwaypoint = waypoint[0];

        for (int i = 0; i < 4; i++) disall[i] = 1000;
    }
	
	// Update is called once per frame
	void Update () {

        tw = (int)Time.time;
        if (currenttw != tw) currenttw = tw;

        //Debug.Log(currenttw);

        checkpoint = GameObject.Find("Checkpoint");

        player[0] = checkpoint.GetComponent<Checkpoint>().player[0];
        player[1] = checkpoint.GetComponent<Checkpoint>().player[1];
        player[2] = checkpoint.GetComponent<Checkpoint>().player[2];
        player[3] = checkpoint.GetComponent<Checkpoint>().player[3];

        //ASSETS
        AssetsCharacters();

        //EnemySkill(); //Temp

        if (player[0] != null)
        {
            //SKILL
            if (GetComponent<Healthpoint>().hp > 0 && seen && isBoss) EnemySkill();

            if (player[0] != null && player[0].GetComponent<Healthpoint>().hp > 0)
                disall[0] = Vector3.Distance(transform.position, player[0].transform.position);
            else disall[0] = 1000;

            if (player[1] != null && player[1].GetComponent<Healthpoint>().hp > 0)
                disall[1] = Vector3.Distance(transform.position, player[1].transform.position);
            else disall[1] = 1000;

            if (player[2] != null && player[2].GetComponent<Healthpoint>().hp > 0)
                disall[2] = Vector3.Distance(transform.position, player[2].transform.position);
            else disall[2] = 1000;

            if (player[3] != null && player[3].GetComponent<Healthpoint>().hp > 0)
                disall[3] = Vector3.Distance(transform.position, player[3].transform.position);
            else disall[3] = 1000;

            if (disall[0] < disall[1] && disall[0] < disall[2] && disall[0] < disall[3]) selectedpy = player[0];
            else if (disall[1] < disall[2] && disall[1] < disall[3] && disall[1] < disall[0]) selectedpy = player[1];
            else if (disall[2] < disall[3] && disall[2] < disall[0] && disall[2] < disall[1]) selectedpy = player[2];
            else if (disall[3] < disall[0] && disall[3] < disall[1] && disall[3] < disall[2]) selectedpy = player[3];

            if (unseen) { blocked = true; seen = false; }
            else blocked = NavMesh.Raycast(transform.position, selectedpy.transform.position, out hit, NavMesh.AllAreas);
            Debug.DrawLine(transform.position, selectedpy.transform.position, blocked ? Color.red : Color.green);

            x = transform.position.x - selectedpy.transform.position.x;
            z = transform.position.z - selectedpy.transform.position.z;
            angle = ((Mathf.Atan2(z, x) * Mathf.Rad2Deg) + 180);

            rotate = 90 - transform.rotation.eulerAngles.y;
            if (rotate < 0) rotate += 360;

            realang = Mathf.Abs(angle - rotate);

            distance = Vector3.Distance(transform.position, selectedpy.transform.position);

            if (GetComponent<Healthpoint>().hp <= 0)
            {
                body.velocity = new Vector3(0, 0, 0);
                anim.Play("Death");
                GetComponent<Collider>().isTrigger = true;
                GetComponent<Rigidbody>().useGravity = false;
                gameObject.tag = "Untagged";
                agent.speed = 0;
                timedie -= Time.deltaTime;

                if (timedie <= 10)
                {
                    transform.Translate(Vector3.down * 0.5f * Time.deltaTime);
                    GetComponent<NavMeshAgent>().enabled = false;
                }

                if (timedie <= 0) Destroy(gameObject);
            }

            else if (GetComponent<Healthpoint>().damaged)
            {
                healthring.SetActive(true);
                GetComponent<Healthpoint>().damaged = false;
                transform.LookAt(selectedpy.transform);
            }

            else if (GetComponent<Healthpoint>().pushed > 0 && !isBig)
            {
                float force = GetComponent<Healthpoint>().pushed;
                body.AddRelativeForce(new Vector3(0f, 0f, -force), ForceMode.Impulse);
                GetComponent<Healthpoint>().pushed = 0;
            }

            else if (GetComponent<Healthpoint>().stunned > 0)
            {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Stun"))
                {
                    if (!isBig) anim.Play("Stun");
                    else anim.Play("Idle");
                }
                GetComponent<Healthpoint>().stunned -= Time.deltaTime;
                agent.speed = 0;
            }

            else if ((realang <= limitin || realang >= limitout || distance <= limitdis) && !blocked)
            {
                transform.LookAt(selectedpy.transform);
                timehide = 3;
                seen = true;

                //EXCEPTION
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Skill01") && gameObject.name == "Butcher")
                {
                    agent.speed = rushspeed + 2;
                    agent.destination = selectedpy.transform.position;
                }

                //STOP MOVING              
                else if (distance <= disatk)
                {
                    anim.SetInteger("Speed", Random.Range(3, 5));
                    agent.speed = 0;
                    PlayOnceSound(sAction[0]);
                }

                else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Skill01") || anim.GetCurrentAnimatorStateInfo(0).IsName("Skill02")
                    || anim.GetCurrentAnimatorStateInfo(0).IsName("Skill03") || anim.GetCurrentAnimatorStateInfo(0).IsName("Skill04")
                    || anim.GetCurrentAnimatorStateInfo(0).IsName("Skill05"))
                {                 
                    agent.speed = 0;
                }

                else
                {
                    agent.speed = rushspeed;
                    agent.destination = selectedpy.transform.position;
                    anim.SetInteger("Speed", 2);
                    //PlaySound(sAction[1]);
                }
            }

            else if (seen)
            {
                transform.LookAt(selectedpy.transform);
                agent.speed = walkspeed;
                agent.destination = selectedpy.transform.position;
                anim.SetInteger("Speed", 2);

                if (timehide <= 0)
                {
                    anim.SetInteger("Speed", -1);
                    agent.speed = 0;

                    if (!iswalk) seen = false;
                    else Invoke("InvokeTime", 5);
                }

                if (blocked) timehide -= Time.deltaTime;
                else timehide = 3;

            }

            else if (iswalk && !seen)
            {
                if (Vector3.Distance(transform.position, nowwaypoint.position) < 1f)
                {
                    timewait -= Time.deltaTime;
                    anim.SetInteger("Speed", -1);
                }

                else
                {
                    agent.SetDestination(nowwaypoint.position);
                    anim.SetInteger("Speed", 1);
                    agent.speed = walkspeed;
                }

                if (timewait <= 0)
                {
                    nowwaypoint = waypoint[Random.Range(0, waypoint.Length)];
                    timewait = 5;
                }
            }

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack01") || anim.GetCurrentAnimatorStateInfo(0).IsName("Attack02")) isatk = true;
            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Skill03") && gameObject.name == "Butcher") isatk = true;
            else isatk = false;
        }
    }

    public void AssetsCharacters()
    {
        if (gameObject.name == "Slave")
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack02")) assets[0].SetActive(true);
            else assets[0].SetActive(false);
        }

        else if (gameObject.name == "Mech")
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack01") || anim.GetCurrentAnimatorStateInfo(0).IsName("Skill04"))
            {
                assets[0].SetActive(true);
                assets[1].GetComponent<Attack>().enabled = true;
            }
            else
            {
                assets[0].SetActive(false);
                assets[1].GetComponent<Attack>().enabled = false;
            }

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack02")) assets[2].SetActive(true);
            else assets[2].SetActive(false);
        }

        else if (gameObject.name == "Butcher")
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Skill01") || anim.GetCurrentAnimatorStateInfo(0).IsName("Skill04"))
            {
                assets[4].GetComponent<Attack>().enabled = false;
                assets[5].SetActive(false);
            }
            else
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack01")) assets[4].GetComponent<Attack>().enabled = true;
                else assets[4].GetComponent<Attack>().enabled = false;

                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack02")) assets[5].SetActive(true);
                else assets[5].SetActive(false);
            }
        }
    }

    public void EnemySkill()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Skill01") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Skill02")
            && !anim.GetCurrentAnimatorStateInfo(0).IsName("Skill03") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Skill04")
            && !anim.GetCurrentAnimatorStateInfo(0).IsName("Skill05"))
        {
            if (gameObject.name == "Mech" && !isSubBoss)
            {
                if (currenttw % 90 == 0)
                {
                    StartCoroutine(InvokeSpell("Mech", 4, 0f));
                    anim.Play("Skill05");
                }

                else if (currenttw % 60 == 0)
                {
                    StartCoroutine(InvokeSpell("Mech", 2, 0.5f));
                    int rand = Random.Range(0, 2);
                    if (rand == 0) anim.Play("Skill02");
                    else anim.Play("Skill03");
                }

                else if (currenttw % 30 == 0)
                {
                    StartCoroutine(InvokeSpell("Mech", 3, 1.7f));
                    anim.Play("Skill04");
                    agent.isStopped = true;
                    StartCoroutine(SubEnemySkill("Jump", 0.5f));
                    assets[1].GetComponent<Attack>().enabled = true;
                    assets[1].GetComponent<Attack>().power = 8.5f;
                    assets[1].GetComponent<Attack>().radius = 10f;
                    assets[1].GetComponent<Attack>().upforce = 1f;
                    Invoke("InvokeActi", 1.7f);
                }

                else if (currenttw % 15 == 0)
                {
                    StartCoroutine(InvokeSpell("Mech", 1, 1.5f));
                    anim.Play("Skill01");
                }
            }

            else if (gameObject.name == "Butcher")
            {
                if (currenttw % 90 == 0)
                {
                    StartCoroutine(InvokeSpell("Butcher", 4, 2.5f));
                    agent.speed = 4f;
                    loop = 2; //MEANS 3 - 1
                    timehide = 3;
                    seen = true;
                    anim.Play("Skill01");
                    assets[7].SetActive(true);
                    PlayForceSound(sAction[3]);
                }

                else if (currenttw % 60 == 0)
                {
                    StartCoroutine(InvokeSpell("Butcher", 3, 1.5f));
                    assets[3].SetActive(true);
                    assets[6].SetActive(true);
                    anim.Play("Skill03");
                    PlayForceSound(sAction[2]);
                }

                else if (currenttw % 30 == 0)
                {
                    StartCoroutine(InvokeSpell("Butcher", 2, 0.5f));
                    anim.Play("Skill02");
                }

                else if (currenttw % 15 == 0)
                {
                    StartCoroutine(InvokeSpell("Butcher", 1, 1f));
                    anim.Play("Skill05");
                }
            }
        }
    }

    IEnumerator SubEnemySkill(string action, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (action == "Jump") body.AddRelativeForce(new Vector3(0f, 90f, 50f), ForceMode.Impulse);
    }

    public void InvokeTime()
    {
        seen = false;
    }

    public void InvokeActi()
    {
        if (gameObject.name == "Mech") assets[1].GetComponent<Attack>().acti = true;
    }

    public void IsFinishAnim()
    {
        //BLANK IT, DO NOT DELETE
    }

    public void Looping(string action)
    {
        if (loop > 0)
        {
            loop--;
            anim.Play(action);
        }
    }

    public void ClearStatus(string cmd)
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (cmd == "Butch") player.GetComponent<Controller>().status = "Butch1";
        }
    }

    IEnumerator InvokeSpell(string name, int sknum, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (name == "Mech")
        {
            if (sknum == 1) Instantiate(assets[3], assets[4].transform.position, assets[4].transform.rotation);
            else if (sknum == 2)
            {
                GameObject beam = Instantiate(assets[5], new Vector3(assets[6].transform.position.x, assets[6].transform.position.y, assets[6].transform.position.z), assets[6].transform.rotation);
                beam.transform.parent = assets[6].transform;
            }
            else if (sknum == 3) agent.isStopped = false;
            else if (sknum == 4) Instantiate(assets[Random.Range(8, 12)], assets[7].transform.position, assets[7].transform.rotation);
        }

        else if (name == "Butcher")
        {
            if (sknum == 1) Instantiate(assets[1], assets[0].transform.position, assets[0].transform.rotation);
            else if (sknum == 2)
            {
                var spanwOBJ = Instantiate(assets[2], new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.Euler(transform.rotation.x - 90, transform.rotation.y, transform.rotation.z));
                spanwOBJ.transform.parent = gameObject.transform;
            }
            else if (sknum == 3)
            {
                assets[3].SetActive(false);
                assets[6].SetActive(false);
            }
            else if (sknum == 4)
            {
                agent.speed = 3f;
                assets[7].SetActive(false);
            }
        }
    }

    public Animator GetAnim()
    {
        return anim;
    }

    public void PlaySound(AudioClip track)
    {
        if (!source.isPlaying)
        {
            source.clip = track;
            source.Play();
        }
    }

    public void PlayOnceSound(AudioClip track)
    {
        if (!source.isPlaying && source.clip != track)
        {
            source.clip = track;
            source.Play();
        }
    }

    public void PlayForceSound(AudioClip track)
    {
        source.clip = track;
        source.Play();
    }
}
