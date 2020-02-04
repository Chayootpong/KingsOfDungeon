using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInfo : MonoBehaviour {

    string[,] skillName = new string[4, 7];
    string[,] skillAct = new string[4, 7];
    string[,] skillAbi = new string[4, 7];

    public Text nameSkill, typeSkill, actSkill, abiSkill;
    int selected;

    public GameObject root;
    public GameObject[] aura;
    public Image[] skillHolder = new Image[7];

    public Sprite[] skillKnightIcon, skillArcherIcon, skillWarchiefIcon, skillBerserkerIcon;


    // Use this for initialization
    void Start () {

        skillName[0, 0] = "Shield of Aegis";
        skillName[0, 1] = "Spinblade";
        skillName[0, 2] = "Leap Stomp";
        skillName[0, 3] = "King's Spirit";
        skillName[0, 4] = "Quick Kick";
        skillName[0, 5] = "Consecration";
        skillName[0, 6] = "Twin Sword Attack";

        skillAct[0, 0] = "None";
        skillAct[0, 1] = "None";
        skillAct[0, 2] = "None";
        skillAct[0, 3] = "Shake";
        skillAct[0, 4] = "None";
        skillAct[0, 5] = "None";
        skillAct[0, 6] = "None";

        skillAbi[0, 0] = "You become invincible for a short duration";
        skillAbi[0, 1] = "Turn around and move forward, deal damage to nearby enemy";
        skillAbi[0, 2] = "Jump toward that deal damage to nearby enemy";
        skillAbi[0, 3] = "Passive, Consume all spirits to the sword. Deal damage to all enemies in front of you";
        skillAbi[0, 4] = "Kick an enemy in front of you, it become stunned";
        skillAbi[0, 5] = "Hold the sword and then thrash it to the ground and bounce the enemies off";
        skillAbi[0, 6] = "Fence the sword that deal massive damage to enemy";

        skillName[1, 0] = "Horus's Eye";
        skillName[1, 1] = "Rolling Dodge";
        skillName[1, 2] = "Split Shot";
        skillName[1, 3] = "Clink Spark";
        skillName[1, 4] = "Smoke Bomb";
        skillName[1, 5] = "Stun Crossbow";
        skillName[1, 6] = "Rifle Arrowshot";

        skillAct[1, 0] = "None";
        skillAct[1, 1] = "None";
        skillAct[1, 2] = "None";
        skillAct[1, 3] = "None";
        skillAct[1, 4] = "None";
        skillAct[1, 5] = "None";
        skillAct[1, 6] = "Tilt";

        skillAbi[1, 0] = "You can attack while moving backward but you are unable to use any skill instead. Click again to become normal";
        skillAbi[1, 1] = "Roll toward for a short distance. You become invulnerable while rolling";
        skillAbi[1, 2] = "Shoot the extra arrow with different angle";
        skillAbi[1, 3] = "Attack enemy with spark arrow that bounces between nearby enemy";
        skillAbi[1, 4] = "Throw smoke bomb that make enemy cannot see other players while in area of this effect";
        skillAbi[1, 5] = "Shoot the arrow that stun enemy for a short duration";
        skillAbi[1, 6] = "Charge the arrow to shoot forward that deal massive damage to all enermies in line";

        skillName[2, 0] = "Rearm";
        skillName[2, 1] = "Spear Barrage";
        skillName[2, 2] = "Hook Knife";
        skillName[2, 3] = "Commander's Call";
        skillName[2, 4] = "Army Reinforce";
        skillName[2, 5] = "Healing Field";
        skillName[2, 6] = "Construction Cantonment";

        skillAct[2, 0] = "None";
        skillAct[2, 1] = "None";
        skillAct[2, 2] = "None";
        skillAct[2, 3] = "None";
        skillAct[2, 4] = "None";
        skillAct[2, 5] = "None";
        skillAct[2, 6] = "Tap";

        skillAbi[2, 0] = "Change attack between melee and range attack";
        skillAbi[2, 1] = "Pierce the spear repeatly";
        skillAbi[2, 2] = "Deal special attack that also bleeding the enemy";
        skillAbi[2, 3] = "All army in your control gain move speed and attack speed for a short duration";
        skillAbi[2, 4] = "Call soidier to fight with you (No need to control them)";
        skillAbi[2, 5] = "Summon a healing totem that heal all army in area";
        skillAbi[2, 6] = "Build the bootcamp that can summon army repeatly until bootcamp is destroyed";

        skillName[3, 0] = "Bloodlust";
        skillName[3, 1] = "Rapid Punch";
        skillName[3, 2] = "Power Among Us";
        skillName[3, 3] = "Flaming Thorn";
        skillName[3, 4] = "Vitality Boost";
        skillName[3, 5] = "Charge";
        skillName[3, 6] = "Splitting Fall";

        skillAct[3, 0] = "None";
        skillAct[3, 1] = "None";
        skillAct[3, 2] = "None";
        skillAct[3, 3] = "None";
        skillAct[3, 4] = "None";
        skillAct[3, 5] = "None";
        skillAct[3, 6] = "Swipe";

        skillAbi[3, 0] = "Regenerate your health instantly";
        skillAbi[3, 1] = "Quick attack the enemy in front of you";
        skillAbi[3, 2] = "Passive, Give ability boost to the nearby allies";
        skillAbi[3, 3] = "Create the flame's ring that deal damage overtime";
        skillAbi[3, 4] = "Cast the small healing area in front of you in a short duration";
        skillAbi[3, 5] = "Move forward, deal damge while moving";
        skillAbi[3, 6] = "Summon the shade from above that deal massive damage around you";
    }
	
	// Update is called once per frame
	void Update () {

        nameSkill.text = skillName[UICharac.selected, selected];

        if (selected == 0) typeSkill.text = "<color=#ffa81e>Trail</color> / Cooldown <color=#736357>" + GetData(UICharac.selected, 0, 3) + "</color> seconds";
        else if (selected < 4) typeSkill.text = "<color=#ff0f0f>Fury</color> / Stamina <color=#736357>" + GetData(UICharac.selected, 1, selected - 1) + "</color> percent";
        else typeSkill.text = "<color=#0054a6>Spell</color> / Cooldown <color=#736357>" + GetData(UICharac.selected, 0, selected - 4) + "</color> seconds";

        actSkill.text = "Interact : <color=#736357>" + skillAct[UICharac.selected, selected] + "</color>";
        abiSkill.text = "Ability : " + skillAbi[UICharac.selected, selected];

        SetSprite();

        for (int i = 0; i < 7; i++)
        {
            if (i == selected) aura[i].SetActive(true);
            else aura[i].SetActive(false);
        }       
    }

    public void SelectSkill(int select)
    {
        selected = select;
    }

    public void SetSprite()
    {
        if (UICharac.selected == 0) for (int i = 0; i < 7; i++) skillHolder[i].sprite = skillKnightIcon[i];
        else if (UICharac.selected == 1) for (int i = 0; i < 7; i++) skillHolder[i].sprite = skillArcherIcon[i];
        else if (UICharac.selected == 2) for (int i = 0; i < 7; i++) skillHolder[i].sprite = skillWarchiefIcon[i];
        else if (UICharac.selected == 3) for (int i = 0; i < 7; i++) skillHolder[i].sprite = skillBerserkerIcon[i];
    }

    public string GetData(int select, int type, int subtype) //type 0 == Cooldown type 1 == Fury Point 
    {
        if (select == 0)
        {
            if (type == 0) return GetComponent<SkillInfo>().knightCD[subtype, 1].ToString();
            else if (type == 1) return GetComponent<SkillInfo>().knightMFC[subtype].ToString();
        }

        else if (select == 1)
        {
            if (type == 0) return GetComponent<SkillInfo>().archerCD[subtype, 1].ToString();
            else if (type == 1) return GetComponent<SkillInfo>().archerMFC[subtype].ToString();
        }

        else if (select == 2)
        {
            if (type == 0) return GetComponent<SkillInfo>().warchiefCD[subtype, 1].ToString();
            else if (type == 1) return GetComponent<SkillInfo>().warchiefMFC[subtype].ToString();
        }

        else if (select == 3)
        {
            if (type == 0) return GetComponent<SkillInfo>().berserkerCD[subtype, 1].ToString();
            else if (type == 1) return GetComponent<SkillInfo>().berserkerMFC[subtype].ToString();
        }

        return null;
    }

    public void Exit()
    {
        selected = 0;

        for (int i = 0; i < 7; i++)
        {
            if (i == selected) aura[i].SetActive(true);
            else aura[i].SetActive(false);
        }

        root.SetActive(false);
    }
}
