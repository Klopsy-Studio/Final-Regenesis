using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum E_Effectiveness
{
   None,
   Neutral,
   Fire,
   Water,
   Grass
};

[Serializable]
public class ElementsEffectiveness 
{
    static float[][] chart =
    {
        //                 NEU FIR WAT GRA
       /*NEU*/ new float[]{1f, 1f, 1f, 1f},
       /*FIR*/ new float[]{1f, 1f, 0.5f, 1.5f},
       /*WAT*/ new float[]{1f, 1.5f, 1f, 0.5f},
       /*GRA*/ new float[]{1f, 0.5f, 1.5f, 1f},
    };

    public static float GetEffectiveness(E_Effectiveness attacker, E_Effectiveness defender)
    {
        if (attacker == E_Effectiveness.None || defender == E_Effectiveness.None) return 1;

        int row = (int)attacker - 1;
        int col = (int)defender - 1;

        return chart[row][col];
    }

}
