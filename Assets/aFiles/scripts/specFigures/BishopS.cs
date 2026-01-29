using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BishopS : MonoBehaviour, IS.IFigure
{
    FigureS figure;
    public float damage;
    public float health;
    private void Awake()
    {
        health = MagicBalanceS.updatedAllK[2][0] ;
        damage = MagicBalanceS.updatedAllK[2][1];
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
                    break;
                }
            }
            nextTarget++;
        }
    }
    public bool Check()
    {
        int nextTarget = 0;
        while (true) //!!!
        {
            if ((figure.currentCell + 1 + nextTarget) > figure.board.cells.Length - 1)
            {
                return false;
            }
            CellS targetCell = figure.board.cells[figure.currentCell + 1 + nextTarget];
            if (targetCell.myFigure != null)
            {
                BlackFigureS blackFigure = targetCell.myFigure.GetComponent<BlackFigureS>();
                if (blackFigure != null)
                {
                    return true;
                }
            }
            nextTarget++;
        }
    }
}
