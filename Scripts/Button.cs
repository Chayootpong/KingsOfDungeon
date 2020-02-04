using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public string Name;
    public GameObject vrpy, mui;

    public void OnPointerDown()
    {
        NetworkClientUI.SendButtonInfo(Name);

        if (Name == "Fury 1")
        {
            if (mui.GetComponent<UI>().player.name == "Knight" &&
                vrpy.GetComponent<Healthpoint>().fp >= (int)(vrpy.GetComponent<SkillInfo>().knightMFC[0] * 100))
            {
                vrpy.GetComponent<Healthpoint>().fp -= (int)(vrpy.GetComponent<SkillInfo>().knightMFC[0] * 100);
            }

            else if (mui.GetComponent<UI>().player.name == "Archer" &&
                vrpy.GetComponent<Healthpoint>().fp >= (int)(vrpy.GetComponent<SkillInfo>().archerMFC[0] * 100))
            {
                vrpy.GetComponent<Healthpoint>().fp -= (int)(vrpy.GetComponent<SkillInfo>().archerMFC[0] * 100);
            }
        }

        if (Name == "Fury 2")
        {
            if (mui.GetComponent<UI>().player.name == "Knight" &&
                vrpy.GetComponent<Healthpoint>().fp >= (int)(vrpy.GetComponent<SkillInfo>().knightMFC[1] * 100))
            {
                vrpy.GetComponent<Healthpoint>().fp -= (int)(vrpy.GetComponent<SkillInfo>().knightMFC[1] * 100);
            }

            else if (mui.GetComponent<UI>().player.name == "Archer" &&
                vrpy.GetComponent<Healthpoint>().fp >= (int)(vrpy.GetComponent<SkillInfo>().archerMFC[1] * 100))
            {
                vrpy.GetComponent<Healthpoint>().fp -= (int)(vrpy.GetComponent<SkillInfo>().archerMFC[1] * 100);
            }
        }
    }
}
