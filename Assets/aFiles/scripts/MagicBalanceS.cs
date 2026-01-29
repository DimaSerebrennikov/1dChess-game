using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MagicBalanceS : MonoBehaviour
{
    float[] powers; //pawn,horse,bishop,rook,queen
    float[] pawnK; //health;damage;LinkHp;LinkDmg
    float[] horseK;
    float[] bishopK;
    float[] rookK;
    float[] queenK;
    static public float[][] updatedAllK;
    float[][] startAllK;
    float decreasor;
    private void Awake()
    {
        powers = new float[] { 1, 1, 1, 1, 1 };
        pawnK = new float[] { 55f, 20 };
        horseK = new float[] { 55f, 20 };
        bishopK = new float[] { 55f, 8f };
        rookK = new float[] { 95f, 10 };
        queenK = new float[] { 40f, 10 };
        updatedAllK = new float[][] { pawnK.ToArray(), horseK.ToArray(), bishopK.ToArray(), rookK.ToArray(), queenK.ToArray() };
        startAllK = new float[][] { pawnK.ToArray(), horseK.ToArray(), bishopK.ToArray(), rookK.ToArray(), queenK.ToArray() };
        decreasor = 0.072f;
    }
    public void BlameIt(int numberOfFigure)
    {
        for (int i = 0; i < powers.Length; i++)
        {
            if (i == numberOfFigure)
            {
                powers[i] = powers[i] - (powers[i] * decreasor);
            }
        }
        UpdatePowers();
    }
    void UpdatePowers()
    {
        for (int i = 0; i < updatedAllK.Length; i++)
        {
            updatedAllK[i][0] = startAllK[i][0] * powers[i];
            updatedAllK[i][1] = startAllK[i][1] * powers[i];
        }
    }
}
