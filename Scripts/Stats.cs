using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {

    public static int[] expGain = new int[4];
    public static int[] killGain = new int[4];
    public static int[] reviveGain = new int[4];
    public static int[] beReviveGain = new int[4];
    public static int[] relicGain = new int[4];
    public static int[] furyGain = new int[4];
    public static int[] spellGain = new int[4];
    public static int[] trailGain = new int[4];

    public static int[] effectGain = new int[4];

    public void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            expGain[i] = killGain[i] = reviveGain[i] = beReviveGain[i] = reviveGain[i] = furyGain[i] = spellGain[i] = trailGain[i] = effectGain[i] = 0;
        }
    }

}
