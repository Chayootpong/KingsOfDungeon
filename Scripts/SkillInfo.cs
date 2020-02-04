using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInfo : MonoBehaviour {

    public float[] knightStat = { 200, 5350, 56.8f, 10000, 8, 50, 25, 3, 3.5f }; //ATK HP RGH RGM% RGF% GB Fury_Drain NMS FMS
    public int[,] knightCD = { {8, 8}, {25, 25}, {17, 17}, {60, 60} }; //current, full
    public float[] knightMFC = { 45, 60, 25, 55, 75, 65 }; //F F F M M M
    public float[,] knightBuff = { { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 7, 7 } }; //F F F M M M T

    public float[] archerStat = { 100, 3125, 17.25f, 10000, 5, 35, 15, 3.75f, 4 }; //ATK HP RGH RGM% RGF% GB Fury_Drain NMS FMS
    public int[,] archerCD = { { 12, 12 }, { 22, 22 }, { 40, 40 }, { 5, 5 } }; //current, full
    public float[] archerMFC = { 45, 35, 70, 55, 65, 100 }; //F F F M M M
    public float[,] archerBuff = { { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 } }; //F F F M M M T

    public float[] warchiefStat = { 250, 4750, 48.7f, 10000, 6, 50, 20, 3.25f, 3.75f }; //ATK HP RGH RGM% RGF% GB Fury_Drain NMS FMS
    public int[,] warchiefCD = { { 60, 60 }, { 20, 20 }, { 90, 90 }, { 3, 3 } }; //current, full
    public float[] warchiefMFC = { 65, 45, 100, 55, 65, 100 }; //F F F M M M
    public float[,] warchiefBuff = { { 0, 0 }, { 0, 0 }, { 4, 4 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 } }; //F F F M M M T

    public float[] berserkerStat = { 220, 4385, 40.5f, 10000, 5.5f, 60, 23, 3f, 3.25f }; //ATK HP RGH RGM% RGF% GB Fury_Drain NMS FMS
    public int[,] berserkerCD = { { 9, 9 }, { 15, 15 }, { 65, 65 }, { 9, 9 } }; //current, full
    public float[] berserkerMFC = { 45, 0, 70, 55, 65, 100 }; //F F F M M M
    public float[,] berserkerBuff = { { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 } }; //F F F M M M T

    int[] tw = new int[11], currenttw = new int[11];

    // Use this for initialization
    void Start () {

        for (int i = 0; i < 4; i++)
        {
            knightCD[i, 0] = knightCD[i, 1];
            archerCD[i, 0] = archerCD[i, 1];
            warchiefCD[i, 0] = warchiefCD[i, 1];
            berserkerCD[i, 0] = berserkerCD[i, 1];
        }
        for (int i = 0; i < 7; i++)
        {
            knightBuff[i, 0] = knightBuff[i, 1];
            archerBuff[i, 0] = archerBuff[i, 1];
            warchiefBuff[i, 0] = warchiefBuff[i, 1];
            berserkerBuff[i, 0] = berserkerBuff[i, 1];
        }
        for (int i = 0; i < 11; i++) currenttw[i] = 0;
    }
	
	// Update is called once per frame
	void Update () {

        for (int i = 0; i < 11; i++) tw[i] = (int)Time.time;

        if (gameObject.name == "Knight")
        {
            for(int i = 0; i < 7; i++)
            { 
                if (i < 4)
                {
                    if (knightCD[i, 0] < knightCD[i, 1]) //SPCD
                    {
                        if (currenttw[i + 7] != tw[i + 7])
                        {
                            currenttw[i + 7] = tw[i + 7];
                            knightCD[i, 0]++;
                        }
                    }
                    else knightCD[i, 0] = knightCD[i, 1];
                }

                if (knightBuff[i, 0] < knightBuff[i, 1]) //BUFF
                {
                    if (currenttw[i] != tw[i])
                    {
                        currenttw[i] = tw[i];
                        knightBuff[i, 0]++;
                    }
                }
                else knightBuff[i, 0] = knightBuff[i, 1];
            }
        }

        if (gameObject.name == "Archer")
        {
            for (int i = 0; i < 7; i++)
            {
                if (i < 4)
                {
                    if (archerCD[i, 0] < archerCD[i, 1]) //SPCD
                    {
                        if (currenttw[i + 7] != tw[i + 7])
                        {
                            currenttw[i + 7] = tw[i + 7];
                            archerCD[i, 0]++;
                        }
                    }
                    else archerCD[i, 0] = archerCD[i, 1];
                }

                if (archerBuff[i, 0] < archerBuff[i, 1]) //BUFF
                {
                    if (currenttw[i] != tw[i])
                    {
                        currenttw[i] = tw[i];
                        archerBuff[i, 0]++;
                    }
                }
                else archerBuff[i, 0] = archerBuff[i, 1];
            }
        }

        if (gameObject.name == "Warchief")
        {
            for (int i = 0; i < 7; i++)
            {
                if (i < 4)
                {
                    if (warchiefCD[i, 0] < warchiefCD[i, 1]) //SPCD
                    {
                        if (currenttw[i + 7] != tw[i + 7])
                        {
                            currenttw[i + 7] = tw[i + 7];
                            warchiefCD[i, 0]++;
                        }
                    }
                    else warchiefCD[i, 0] = warchiefCD[i, 1];
                }

                if (warchiefBuff[i, 0] < warchiefBuff[i, 1]) //BUFF
                {
                    if (currenttw[i] != tw[i])
                    {
                        currenttw[i] = tw[i];
                        warchiefBuff[i, 0]++;
                    }
                }
                else warchiefBuff[i, 0] = warchiefBuff[i, 1];
            }
        }

        if (gameObject.name == "Berserker")
        {
            for (int i = 0; i < 7; i++)
            {
                if (i < 4)
                {
                    if (berserkerCD[i, 0] < berserkerCD[i, 1]) //SPCD
                    {
                        if (currenttw[i + 7] != tw[i + 7])
                        {
                            currenttw[i + 7] = tw[i + 7];
                            berserkerCD[i, 0]++;
                        }
                    }
                    else berserkerCD[i, 0] = berserkerCD[i, 1];
                }

                if (berserkerBuff[i, 0] < berserkerBuff[i, 1]) //BUFF
                {
                    if (currenttw[i] != tw[i])
                    {
                        currenttw[i] = tw[i];
                        berserkerBuff[i, 0]++;
                    }
                }
                else berserkerBuff[i, 0] = berserkerBuff[i, 1];
            }
        }

    }
}
