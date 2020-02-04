using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    public int type, damage, adddmg = 0;
    float atkdmg;
    public GameObject self;

    public float power = 3f, radius = 1f, upforce = 1f, muldmg;
    public bool acti;

    public float stun, push;
    public string spawn;

    public bool isCombine;
    public GameObject combiner;
    public string effect;


    // Use this for initialization
    void Start() {

        if (spawn != "") { self = GameObject.Find(spawn); damage = adddmg; }
        else //SPAWN CASE: ONLY HEROES
        {
            if (self.gameObject.name == "Knight") damage = (int)self.GetComponent<SkillInfo>().knightStat[0] + adddmg;
            else if (self.gameObject.name == "Archer") damage = (int)self.GetComponent<SkillInfo>().archerStat[0] + adddmg;
            else if (self.gameObject.name == "Warchief") damage = (int)self.GetComponent<SkillInfo>().warchiefStat[0] + adddmg;
            else if (self.gameObject.name == "Berserker") damage = (int)self.GetComponent<SkillInfo>().berserkerStat[0] + adddmg;
        }

        if (isCombine) combiner = GameObject.Find(gameObject.name.Substring(4));

        atkdmg = 0;
        muldmg = 1f;
        acti = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (acti)
        {
            Vector3 areaPosition = transform.position;
            Collider[] col = Physics.OverlapSphere(areaPosition, radius);
            foreach (Collider hit in col)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    if (type == 1 && hit.gameObject.tag == "Enemy" && !hit.gameObject.GetComponent<EnemyAI>().isBig)
                    {
                        rb.AddExplosionForce(power, areaPosition, radius, upforce, ForceMode.Impulse);
                        atkdmg = (damage * muldmg) * hit.GetComponent<Healthpoint>().block / 100;
                        if (atkdmg <= 0) atkdmg = 0;
                        hit.gameObject.GetComponent<Healthpoint>().hp -= (int)atkdmg;

                        //FOR GAIN EXP
                        if (hit.gameObject.GetComponent<Healthpoint>().hp <= 0)
                        {
                            Stats.expGain[self.GetComponent<Healthpoint>().id] += hit.gameObject.GetComponent<Healthpoint>().exp;
                            Stats.killGain[self.GetComponent<Healthpoint>().id]++;
                        }

                        self.GetComponent<Controller>().isatk = true;

                        if (stun > 0f) hit.gameObject.GetComponent<Healthpoint>().stunned = stun;
                        if (push > 0f) hit.gameObject.GetComponent<Healthpoint>().pushed = push;
                    }

                    if (type == 2 && hit.gameObject.tag == "Player")
                    {
                        rb.AddExplosionForce(power, areaPosition, radius, upforce, ForceMode.Impulse);
                        atkdmg = (damage * muldmg) * hit.GetComponent<Healthpoint>().block / 100;
                        if (atkdmg <= 0) atkdmg = 0;
                        hit.gameObject.GetComponent<Healthpoint>().hp -= (int)atkdmg;
                    }
                }
            }

            acti = false;
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if (self.GetComponent<Healthpoint>().hp > 0)
        {
            if (type == 1 && col.gameObject.tag == "Enemy" && self.GetComponent<Controller>().isatk)
            {
                atkdmg = (damage * muldmg) * col.GetComponent<Healthpoint>().block / 100;
                if (atkdmg <= 0) atkdmg = 0;
                col.gameObject.GetComponent<Healthpoint>().hp -= (int)atkdmg;

                //FOR GAIN EXP
                if (col.gameObject.GetComponent<Healthpoint>().hp <= 0)
                {
                    Stats.expGain[self.GetComponent<Healthpoint>().id] += col.gameObject.GetComponent<Healthpoint>().exp;
                    Stats.killGain[self.GetComponent<Healthpoint>().id]++;
                }

                if (stun > 0f) col.gameObject.GetComponent<Healthpoint>().stunned = stun;
                if (push > 0f) col.gameObject.GetComponent<Healthpoint>().pushed = push;
            }

            if (type == 2 && col.gameObject.tag == "Player" && (self.GetComponent<EnemyAI>().isatk || spawn != null))
            {
                atkdmg = (damage * muldmg) * col.GetComponent<Healthpoint>().block / 100;
                if (atkdmg <= 0) atkdmg = 0;
                col.gameObject.GetComponent<Healthpoint>().hp -= (int)atkdmg;

                if (effect != "" && col.GetComponent<Healthpoint>().hp > 0) col.GetComponent<Controller>().status = effect;

                //FOR BUTCHER
                if (effect.Contains("Butch")) self.GetComponent<EnemyAI>().GetAnim().Play("Skill04");
            }
        }

        if (isCombine) combiner.GetComponent<SkillShot>().body.velocity *= 1.5f;
    }
}
