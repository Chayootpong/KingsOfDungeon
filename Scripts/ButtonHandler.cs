using System;
using UnityEngine;

namespace UnityStandardAssets.CrossPlatformInput
{
    public class ButtonHandler : MonoBehaviour
    {

        public string Name;
        public GameObject vrpy, mui;

        void OnEnable()
        {

        }

        public void SetDownState()
        {
            CrossPlatformInputManager.SetButtonDown(Name);
            NetworkClientUI.SendButtonInfo(Name);

            if (Name == "Fury 1")
            {
                if (mui.GetComponent<UI>().player.name == "Knight" &&
                    vrpy.GetComponent<Healthpoint>().fp >= (int)(vrpy.GetComponent<SkillInfo>().knightMFC[0] * 100))
                {
                    vrpy.GetComponent<Healthpoint>().fp -= (int)(vrpy.GetComponent<SkillInfo>().knightMFC[0] * 100);
                    QuestCounter.furyUse++;
                }

                else if (mui.GetComponent<UI>().player.name == "Archer" &&
                    vrpy.GetComponent<Healthpoint>().fp >= (int)(vrpy.GetComponent<SkillInfo>().archerMFC[0] * 100))
                {
                    vrpy.GetComponent<Healthpoint>().fp -= (int)(vrpy.GetComponent<SkillInfo>().archerMFC[0] * 100);
                    QuestCounter.furyUse++;
                }

                else if (mui.GetComponent<UI>().player.name == "Warchief" &&
                    vrpy.GetComponent<Healthpoint>().fp >= (int)(vrpy.GetComponent<SkillInfo>().warchiefMFC[0] * 100))
                {
                    vrpy.GetComponent<Healthpoint>().fp -= (int)(vrpy.GetComponent<SkillInfo>().warchiefMFC[0] * 100);
                    QuestCounter.furyUse++;
                }

                else if (mui.GetComponent<UI>().player.name == "Berserker" &&
                    vrpy.GetComponent<Healthpoint>().fp >= (int)(vrpy.GetComponent<SkillInfo>().berserkerMFC[0] * 100))
                {
                    vrpy.GetComponent<Healthpoint>().fp -= (int)(vrpy.GetComponent<SkillInfo>().berserkerMFC[0] * 100);
                    QuestCounter.furyUse++;
                }
            }

            else if (Name == "Fury 2")
            {
                if (mui.GetComponent<UI>().player.name == "Knight" &&
                    vrpy.GetComponent<Healthpoint>().fp >= (int)(vrpy.GetComponent<SkillInfo>().knightMFC[1] * 100))
                {
                    vrpy.GetComponent<Healthpoint>().fp -= (int)(vrpy.GetComponent<SkillInfo>().knightMFC[1] * 100);
                    QuestCounter.furyUse++;
                }

                else if (mui.GetComponent<UI>().player.name == "Archer" &&
                    vrpy.GetComponent<Healthpoint>().fp >= (int)(vrpy.GetComponent<SkillInfo>().archerMFC[1] * 100))
                {
                    vrpy.GetComponent<Healthpoint>().fp -= (int)(vrpy.GetComponent<SkillInfo>().archerMFC[1] * 100);
                    QuestCounter.furyUse++;
                }

                else if (mui.GetComponent<UI>().player.name == "Warchief" &&
                    vrpy.GetComponent<Healthpoint>().fp >= (int)(vrpy.GetComponent<SkillInfo>().warchiefMFC[1] * 100))
                {
                    vrpy.GetComponent<Healthpoint>().fp -= (int)(vrpy.GetComponent<SkillInfo>().warchiefMFC[1] * 100);
                    QuestCounter.furyUse++;
                }

                else if (mui.GetComponent<UI>().player.name == "Berserker")
                {
                    
                }
            }

            else if (Name == "Fury 3")
            {
                if (mui.GetComponent<UI>().player.name == "Knight")
                {

                }

                else if (mui.GetComponent<UI>().player.name == "Archer" &&
                    vrpy.GetComponent<Healthpoint>().fp >= (int)(vrpy.GetComponent<SkillInfo>().archerMFC[2] * 100))
                {
                    vrpy.GetComponent<Healthpoint>().fp -= (int)(vrpy.GetComponent<SkillInfo>().archerMFC[2] * 100);
                    QuestCounter.furyUse++;
                }

                else if (mui.GetComponent<UI>().player.name == "Warchief" &&
                    vrpy.GetComponent<Healthpoint>().fp >= (int)(vrpy.GetComponent<SkillInfo>().warchiefMFC[2] * 100))
                {
                    vrpy.GetComponent<Healthpoint>().fp -= (int)(vrpy.GetComponent<SkillInfo>().warchiefMFC[2] * 100);
                    QuestCounter.furyUse++;
                }

                else if (mui.GetComponent<UI>().player.name == "Berserker" &&
                    vrpy.GetComponent<Healthpoint>().fp >= (int)(vrpy.GetComponent<SkillInfo>().berserkerMFC[2] * 100))
                {
                    vrpy.GetComponent<Healthpoint>().fp -= (int)(vrpy.GetComponent<SkillInfo>().berserkerMFC[2] * 100);
                    QuestCounter.furyUse++;
                }
            }

            else if (Name == "Skill 1")
            {
                if (mui.GetComponent<UI>().player.name == "Knight" &&
                    vrpy.GetComponent<SkillInfo>().knightCD[0, 0] == vrpy.GetComponent<SkillInfo>().knightCD[0, 1])
                {
                    vrpy.GetComponent<SkillInfo>().knightCD[0, 0] = 0;
                    QuestCounter.spellUse++;
                }

                else if (mui.GetComponent<UI>().player.name == "Archer" &&
                    vrpy.GetComponent<SkillInfo>().archerCD[0, 0] == vrpy.GetComponent<SkillInfo>().archerCD[0, 1])
                {
                    vrpy.GetComponent<SkillInfo>().archerCD[0, 0] = 0;
                    QuestCounter.spellUse++;
                }

                else if (mui.GetComponent<UI>().player.name == "Warchief" &&
                    vrpy.GetComponent<SkillInfo>().warchiefCD[0, 0] == vrpy.GetComponent<SkillInfo>().warchiefCD[0, 1])
                {
                    vrpy.GetComponent<SkillInfo>().warchiefCD[0, 0] = 0;
                    QuestCounter.spellUse++;
                }

                else if (mui.GetComponent<UI>().player.name == "Berserker" &&
                    vrpy.GetComponent<SkillInfo>().berserkerCD[0, 0] == vrpy.GetComponent<SkillInfo>().berserkerCD[0, 1])
                {
                    vrpy.GetComponent<SkillInfo>().berserkerCD[0, 0] = 0;
                    QuestCounter.spellUse++;
                }
            }

            else if (Name == "Skill 2")
            {
                if (mui.GetComponent<UI>().player.name == "Knight" &&
                    vrpy.GetComponent<SkillInfo>().knightCD[1, 0] == vrpy.GetComponent<SkillInfo>().knightCD[1, 1])
                {
                    vrpy.GetComponent<SkillInfo>().knightCD[1, 0] = 0;
                    QuestCounter.spellUse++;
                }

                else if (mui.GetComponent<UI>().player.name == "Archer" &&
                    vrpy.GetComponent<SkillInfo>().archerCD[1, 0] == vrpy.GetComponent<SkillInfo>().archerCD[1, 1])
                {
                    vrpy.GetComponent<SkillInfo>().archerCD[1, 0] = 0;
                    QuestCounter.spellUse++;
                }

                else if (mui.GetComponent<UI>().player.name == "Warchief" &&
                    vrpy.GetComponent<SkillInfo>().warchiefCD[1, 0] == vrpy.GetComponent<SkillInfo>().warchiefCD[1, 1])
                {
                    vrpy.GetComponent<SkillInfo>().warchiefCD[1, 0] = 0;
                    QuestCounter.spellUse++;
                }

                else if (mui.GetComponent<UI>().player.name == "Berserker" &&
                    vrpy.GetComponent<SkillInfo>().berserkerCD[1, 0] == vrpy.GetComponent<SkillInfo>().berserkerCD[1, 1])
                {
                    vrpy.GetComponent<SkillInfo>().berserkerCD[1, 0] = 0;
                    QuestCounter.spellUse++;
                }
            }

            else if (Name == "Skill 3")
            {
                if (mui.GetComponent<UI>().player.name == "Knight" &&
                    vrpy.GetComponent<SkillInfo>().knightCD[2, 0] == vrpy.GetComponent<SkillInfo>().knightCD[2, 1])
                {
                    vrpy.GetComponent<SkillInfo>().knightCD[2, 0] = 0;
                    QuestCounter.spellUse++;
                }

                else if (mui.GetComponent<UI>().player.name == "Archer" &&
                    vrpy.GetComponent<SkillInfo>().archerCD[2, 0] == vrpy.GetComponent<SkillInfo>().archerCD[2, 1])
                {
                    vrpy.GetComponent<SkillInfo>().archerCD[2, 0] = 0;
                    QuestCounter.spellUse++;
                }

                else if (mui.GetComponent<UI>().player.name == "Warchief" &&
                    vrpy.GetComponent<SkillInfo>().warchiefCD[2, 0] == vrpy.GetComponent<SkillInfo>().warchiefCD[2, 1])
                {
                    vrpy.GetComponent<VirturePlayer>().warchiefSkill3.SetActive(true);
                    vrpy.GetComponent<SkillInfo>().warchiefCD[2, 0] = 0;
                    QuestCounter.spellUse++;
                }

                else if (mui.GetComponent<UI>().player.name == "Berserker" &&
                    vrpy.GetComponent<SkillInfo>().berserkerCD[2, 0] == vrpy.GetComponent<SkillInfo>().berserkerCD[2, 1])
                {
                    vrpy.GetComponent<SkillInfo>().berserkerCD[2, 0] = 0;
                    QuestCounter.spellUse++;
                }
            }

            else if (Name == "Trail")
            {
                if (mui.GetComponent<UI>().player.name == "Knight" &&
                    vrpy.GetComponent<SkillInfo>().knightCD[3, 0] == vrpy.GetComponent<SkillInfo>().knightCD[3, 1])
                {
                    vrpy.GetComponent<SkillInfo>().knightCD[3, 0] = 0;
                    vrpy.GetComponent<SkillInfo>().knightBuff[6, 0] = 0;
                    QuestCounter.trailUse++;
                }

                else if (mui.GetComponent<UI>().player.name == "Archer" &&
                    vrpy.GetComponent<SkillInfo>().archerCD[3, 0] == vrpy.GetComponent<SkillInfo>().archerCD[3, 1])
                {
                    vrpy.GetComponent<SkillInfo>().archerCD[3, 0] = 0;
                    vrpy.GetComponent<SkillInfo>().archerBuff[6, 0] = 0;
                    if (mui.GetComponent<UI>().isReverse) mui.GetComponent<UI>().isReverse = false;
                    else mui.GetComponent<UI>().isReverse = true;
                    QuestCounter.trailUse++;
                }

                else if (mui.GetComponent<UI>().player.name == "Warchief" &&
                    vrpy.GetComponent<SkillInfo>().warchiefCD[3, 0] == vrpy.GetComponent<SkillInfo>().warchiefCD[3, 1])
                {
                    vrpy.GetComponent<SkillInfo>().warchiefCD[3, 0] = 0;
                    vrpy.GetComponent<SkillInfo>().warchiefBuff[6, 0] = 0;
                    QuestCounter.trailUse++;
                }

                else if (mui.GetComponent<UI>().player.name == "Berserker" &&
                    vrpy.GetComponent<SkillInfo>().berserkerCD[3, 0] == vrpy.GetComponent<SkillInfo>().berserkerCD[3, 1])
                {
                    vrpy.GetComponent<SkillInfo>().berserkerCD[3, 0] = 0;
                    vrpy.GetComponent<SkillInfo>().berserkerBuff[6, 0] = 0;
                    QuestCounter.trailUse++;
                }
            }
        }

        public void SetUpState()
        {
            CrossPlatformInputManager.SetButtonUp(Name);
        }


        public void SetAxisPositiveState()
        {
            CrossPlatformInputManager.SetAxisPositive(Name);
        }


        public void SetAxisNeutralState()
        {
            CrossPlatformInputManager.SetAxisZero(Name);
        }


        public void SetAxisNegativeState()
        {
            CrossPlatformInputManager.SetAxisNegative(Name);
        }

        public void Update()
        {

        }

    }
}
