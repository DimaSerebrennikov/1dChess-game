using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IS;

public class QueenS : MonoBehaviour, IS.IFigure
{
    FigureS figure;
    public float damage;
    public float health;
    private void Awake()
    {
        health = MagicBalanceS.updatedAllK[4][0];
        damage = MagicBalanceS.updatedAllK[4][1];
        figure = GetComponent<FigureS>();
        figure.Health = health;
    }
    public void Attack()
    {
        figure.CreateHitSound(damage);
        int nextTarget = 0;
        while (true) //!!!
        {
            if ((figure.currentCell + 1 + nextTarget) > figure.board.cells.Length - 1)
            {
                break;
            }
            CellS targetCell = figure.board.cells[figure.currentCell + 1 + nextTarget];
            if (targetCell.myFigure != null)
            {
                BlackFigureS blackFigure = targetCell.myFigure.GetComponent<BlackFigureS>();
                if (blackFigure != null)
                {
                    blackFigure.Health = blackFigure.Health - damage;
                }
            }
            nextTarget++;
        }
    }
    public bool Check() 
    {
        CellS targetCell = figure.board.cells[figure.currentCell + 1];
        if (targetCell.myFigure != null)
        {
            BlackFigureS blackFigure = targetCell.myFigure.GetComponent<BlackFigureS>();
            if (blackFigure != null)
            {
                return true;
            }
            return false;
        }
        return false;
    }
}
