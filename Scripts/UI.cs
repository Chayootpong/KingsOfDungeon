using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {

    public GameObject player;
    public Image[] UIskill; //F F F M M M T
    public GameObject[] UIBskill; //F F F M M M T
    public GameObject[] UICDskill;
    public GameObject[] UISLskill;
    public Text[] CDTime;
    public Sprite locked;

    //CHARACTERS
    public Sprite[] knightNM; //F F F M M M T
    public Sprite[] knightGS; //F F F M M M T

    public Sprite[] archerNM; //F F F M M M T
    public Sprite[] archerGS; //F F F M M M T

    public Sprite[] warchiefNM; //F F F M M M T
    public Sprite[] warchiefGS; //F F F M M M T

    public Sprite[] berserkerNM; //F F F M M M T
    public Sprite[] berserkerGS; //F F F M M M T

    //TOGGLE
    public Sprite[] toggleIM;
    public Image toggleBTN;
    bool isFury = true;
    public GameObject[] element;

    //JOYSTICK
    public GameObject joystick;

    int level;
    public bool isReverse;

    //STATUS (except death)
    public string status, etc;
    public GameObject[] sts_b; //Hack

    //DEATH
    public bool death;
    public GameObject death_b;

    //PAUSE
    public GameObject pause, loading, spellBG;

    //DOUBLE CLICK
    float distime, timer;
    bool disable, isCounting;

    // Use this for initialization
    void Start () {

        //level = player.GetComponent<LevelEXP>().level; //LEVEL

        if (player == null) player = GameObject.FindGameObjectWithTag("Player");

        //if (player.name == "Knight") for (int x = 0; x < UIskill.Length; x++) UIskill[x].sprite = knightNM[x];
        //else if (player.name == "Archer") for (int x = 0; x < UIskill.Length; x++) UIskill[x].sprite = archerNM[x];

        //STARTER
        death = false;
        death_b.SetActive(false);
        distime = 0.7f;
        timer = 0.5f;
        disable = isCounting = false;
    }
	
	// Update is called once per frame
	void Update () {

        //COUNTER DOUBLE CLICK
        if (!disable)
        {
            if (isCounting) timer -= Time.deltaTime;
            if (timer <= 0)
            {
                isCounting = false;
                timer = 0.5f;
            }
        }
        else distime -= Time.deltaTime;

        if (distime <= 0)
        {
            distime = 0.75f;
            disable = false;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //CONTROL UI
        if (isReverse) for (int i = 0; i < UISLskill.Length - 1; i++) UISLskill[i].SetActive(true);
        else for (int i = 0; i < UISLskill.Length - 1; i++) UISLskill[i].SetActive(false);

        if (player.name == "Knight")
        {
            //CHECK GUAGE
            for (int i = 0; i < 3; i++)
            {
                if (player.GetComponent<Healthpoint>().fp >= player.GetComponent<SkillInfo>().knightMFC[i] * 100)
                    UIskill[i].sprite = knightNM[i];
                else UIskill[i].sprite = knightGS[i];
            }

            for (int j = 0; j < 3; j++)
            {
                if (player.GetComponent<Healthpoint>().mp >= player.GetComponent<SkillInfo>().knightMFC[j + 3] * 100 &&
                player.GetComponent<SkillInfo>().knightCD[j, 0] >= player.GetComponent<SkillInfo>().knightCD[j, 1])
                    UIskill[j + 3].sprite = knightNM[j + 3];
                else UIskill[j + 3].sprite = knightGS[j + 3];
            }

            if (player.GetComponent<SkillInfo>().knightCD[3, 0] >= player.GetComponent<SkillInfo>().knightCD[3, 1])
                UIskill[6].sprite = knightNM[6];
            else UIskill[6].sprite = knightGS[6];

            //CHECK COOLDOWN
            for (int k = 0; k < 4; k++)
            {
                if (player.GetComponent<SkillInfo>().knightCD[k, 0] < player.GetComponent<SkillInfo>().knightCD[k, 1])
                    UICDskill[k].SetActive(true);
                else UICDskill[k].SetActive(false);

                CDTime[k].text = (player.GetComponent<SkillInfo>().knightCD[k, 1] - player.GetComponent<SkillInfo>().knightCD[k, 0]).ToString();
            }
        }

        else if (player.name == "Archer")
        {
            //CHECK GUAGE
            for (int i = 0; i < 3; i++)
            {
                if (player.GetComponent<Healthpoint>().fp >= player.GetComponent<SkillInfo>().archerMFC[i] * 100)
                    UIskill[i].sprite = archerNM[i];
                else UIskill[i].sprite = archerGS[i];
            }

            for (int j = 0; j < 3; j++)
            {
                if (player.GetComponent<Healthpoint>().mp >= player.GetComponent<SkillInfo>().archerMFC[j + 3] * 100 &&
                player.GetComponent<SkillInfo>().archerCD[j, 0] >= player.GetComponent<SkillInfo>().archerCD[j, 1])
                    UIskill[j + 3].sprite = archerNM[j + 3];
                else UIskill[j + 3].sprite = archerGS[j + 3];
            }

            if (player.GetComponent<SkillInfo>().archerCD[3, 0] >= player.GetComponent<SkillInfo>().archerCD[3, 1])
                UIskill[6].sprite = archerNM[6];
            else UIskill[6].sprite = archerGS[6];

            //CHECK COOLDOWN
            for (int k = 0; k < 4; k++)
            {
                if (player.GetComponent<SkillInfo>().archerCD[k, 0] < player.GetComponent<SkillInfo>().archerCD[k, 1])
                    UICDskill[k].SetActive(true);
                else UICDskill[k].SetActive(false);

                CDTime[k].text = (player.GetComponent<SkillInfo>().archerCD[k, 1] - player.GetComponent<SkillInfo>().archerCD[k, 0]).ToString();
            }
        }

        else if (player.name == "Warchief")
        {
            //CHECK GUAGE
            for (int i = 0; i < 3; i++)
            {
                if (player.GetComponent<Healthpoint>().fp >= player.GetComponent<SkillInfo>().warchiefMFC[i] * 100)
                    UIskill[i].sprite = warchiefNM[i];
                else UIskill[i].sprite = warchiefGS[i];
            }

            for (int j = 0; j < 3; j++)
            {
                if (player.GetComponent<Healthpoint>().mp >= player.GetComponent<SkillInfo>().warchiefMFC[j + 3] * 100 &&
                player.GetComponent<SkillInfo>().warchiefCD[j, 0] >= player.GetComponent<SkillInfo>().warchiefCD[j, 1])
                    UIskill[j + 3].sprite = warchiefNM[j + 3];
                else UIskill[j + 3].sprite = warchiefGS[j + 3];
            }

            if (player.GetComponent<SkillInfo>().warchiefCD[3, 0] >= player.GetComponent<SkillInfo>().warchiefCD[3, 1])
                UIskill[6].sprite = warchiefNM[6];
            else UIskill[6].sprite = warchiefGS[6];

            //CHECK COOLDOWN
            for (int k = 0; k < 4; k++)
            {
                if (player.GetComponent<SkillInfo>().warchiefCD[k, 0] < player.GetComponent<SkillInfo>().warchiefCD[k, 1])
                    UICDskill[k].SetActive(true);
                else UICDskill[k].SetActive(false);

                CDTime[k].text = (player.GetComponent<SkillInfo>().warchiefCD[k, 1] - player.GetComponent<SkillInfo>().warchiefCD[k, 0]).ToString();
            }
        }

        else if (player.name == "Berserker")
        {
            //CHECK GUAGE
            for (int i = 0; i < 3; i++)
            {
                if (player.GetComponent<Healthpoint>().fp >= player.GetComponent<SkillInfo>().berserkerMFC[i] * 100)
                    UIskill[i].sprite = berserkerNM[i];
                else UIskill[i].sprite = berserkerGS[i];
            }

            for (int j = 0; j < 3; j++)
            {
                if (player.GetComponent<Healthpoint>().mp >= player.GetComponent<SkillInfo>().berserkerMFC[j + 3] * 100 &&
                player.GetComponent<SkillInfo>().berserkerCD[j, 0] >= player.GetComponent<SkillInfo>().berserkerCD[j, 1])
                    UIskill[j + 3].sprite = berserkerNM[j + 3];
                else UIskill[j + 3].sprite = berserkerGS[j + 3];
            }

            if (player.GetComponent<SkillInfo>().berserkerCD[3, 0] >= player.GetComponent<SkillInfo>().berserkerCD[3, 1])
                UIskill[6].sprite = berserkerNM[6];
            else UIskill[6].sprite = berserkerGS[6];

            //CHECK COOLDOWN
            for (int k = 0; k < 4; k++)
            {
                if (player.GetComponent<SkillInfo>().berserkerCD[k, 0] < player.GetComponent<SkillInfo>().berserkerCD[k, 1])
                    UICDskill[k].SetActive(true);
                else UICDskill[k].SetActive(false);

                CDTime[k].text = (player.GetComponent<SkillInfo>().berserkerCD[k, 1] - player.GetComponent<SkillInfo>().berserkerCD[k, 0]).ToString();
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //STATUS UI
        if (death) death_b.SetActive(true);
        else death_b.SetActive(false);

        if (status != null)
        {
            if (status == "Hack")
            {
                sts_b[int.Parse(etc) - 1].SetActive(true);
                joystick.SetActive(false);
                NetworkClientUI.SendJoystickInfo(0, 0);
            }
            else if (status == "Butch")
            {
                if (etc == "0") NetworkClientUI.SceneAdditive("Butch");
                else if (etc == "1") NetworkClientUI.SceneSubitive("Butch");
            }

            //Handheld.Vibrate(); //FOR MOBILE
            status = etc = null;
        }
    }

    public void OnToggle()
    {
        if (isFury)
        {
            isFury = false;
            //toggleBTN.sprite = toggleIM[1];
            spellBG.SetActive(true);
            for (int i = 0; i < element.Length; i++) element[i].SetActive(false);
        }

        else
        {
            isFury = true;
            //toggleBTN.sprite = toggleIM[0];
            spellBG.SetActive(false);
            for (int i = 0; i < element.Length; i++) element[i].SetActive(true);
        }
    }

    public void DoubleClick() //FOR TOGGLE
    {
        if (!disable)
        {
            if (timer > 0 && isCounting)
            {
                disable = true;
                OnToggle();
                isCounting = false;
                timer = 0.5f;
            }
            else if (!isCounting) isCounting = true;
        }
    }

    public void ShowPause()
    {
        pause.SetActive(true);
    }

    public void HidePause()
    {
        pause.SetActive(false);
    }

    public void Menu()
    {
        loading.SetActive(true);
        DontDestroy.isIntro = false;
        SceneManager.LoadScene("Menu");
    }
}
