using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAI : MonoBehaviour {

    float angle, distance, rotate, realang, x, z, timehide, timedie, timewait;

    public float disatk, walkspeed, rushspeed;
    public bool isatk, iswalk;
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

    public float limitin, limitout, limitdis, lifetime;

    bool isFinishAnim = true, isTrigger = false;
    int tw, currenttw;

    //ABILITY
    public bool isBig = false;

    //ATTACK ASSETS
    public GameObject[] assets;

	// Use this for initialization
	void Start () {

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
        if (currenttw != tw)
        {
            currenttw = tw;
            lifetime--;
        }

        if (lifetime <= 0) GetComponent<Healthpoint>().hp = 0;

        checkpoint = GameObject.Find("Checkpoint");

        player[0] = checkpoint.GetComponent<Checkpoint>().player[0];
        player[1] = checkpoint.GetComponent<Checkpoint>().player[1];
        player[2] = checkpoint.GetComponent<Checkpoint>().player[2];
        player[3] = checkpoint.GetComponent<Checkpoint>().player[3];

        //ASSETS
        //AssetsCharacters();

        //EnemySkill(); //Temp

        if (GameObject.FindGameObjectWithTag("Enemy"))
        {
            //SKILL
            //if (GetComponent<Healthpoint>().hp > 0 && seen) EnemySkill();

            /*if (player[0] != null && player[0].GetComponent<Healthpoint>().hp > 0)
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
            else if (disall[3] < disall[0] && disall[3] < disall[1] && disall[3] < disall[2]) selectedpy = player[3];*/

            selectedpy = GameObject.FindGameObjectWithTag("Enemy");

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

            /*else if (GetComponent<Healthpoint>().stunned > 0)
            {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Stun"))
                {
                    if (!isBig) anim.Play("Stun");
                    else anim.Play("Idle");
                }
                GetComponent<Healthpoint>().stunned -= Time.deltaTime;
                agent.speed = 0;
            }*/

            else if ((realang <= limitin || realang >= limitout || distance <= limitdis) && !blocked)
            {
                transform.LookAt(selectedpy.transform);
                timehide = 3;
                seen = true;

                //STOP MOVING              
                if (distance <= disatk)
                {
                    anim.Play("Attack02");
                    agent.speed = 0;
                    //GetComponent<Controller>().spattack = true;
                    //Debug.Log("SUS");
                }

                else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack02") || anim.GetCurrentAnimatorStateInfo(0).IsName("Skill02")
                    || anim.GetCurrentAnimatorStateInfo(0).IsName("Skill03") || anim.GetCurrentAnimatorStateInfo(0).IsName("Skill04")
                    || anim.GetCurrentAnimatorStateInfo(0).IsName("Skill05"))
                {
                    agent.speed = 0;
                }

                /*else if (isFinishAnim)
                {
                    agent.speed = rushspeed;
                    agent.destination = selectedpy.transform.position;
                    anim.SetInteger("Speed", 2);
                }*/

                else
                {
                    agent.speed = rushspeed;
                    agent.destination = selectedpy.transform.position;
                    anim.Play("Run");
                    //GetComponent<Controller>().spattack = false;
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

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack01") || anim.GetCurrentAnimatorStateInfo(0).IsName("Attack02")) isatk = true;
            else isatk = false;
        }
    }

    /*public void AssetsCharacters()
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

    }*/ 

    /*public void InvokeTime()
    {
        seen = false;
    }

    public void InvokeActi()
    {
        if (gameObject.name == "Mech") assets[1].GetComponent<Attack>().acti = true;
    }

    /*public void IsFinishAnim()
    {
        if (!seen) anim.Play("Idle");
        isFinishAnim = true;
        isTrigger = false;
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
            else if (sknum == 4) Instantiate(assets[7], assets[8].transform.position, assets[8].transform.rotation);
        }
    }*/
    
}
