using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBishopS : MonoBehaviour, IS.IFigure
{
    BlackFigureS figure;
    public float damage;
    public float health;
    private void Awake()
    {
        health = MagicBalanceS.updatedAllK[2][0];
        damage = MagicBalanceS.updatedAllK[2][1];
        figure = GetComponent<BlackFigureS>();
        figure.Health = health;
    }
    public void Attack()
    {
        figure.CreateHitSound(damage);
        int nextTarget = 0;
        while (true) //!!!
        {
            if ((figure.currentCell - 1 + nextTarget) < 0)
            {
                break;
            }
            CellS targetCell = figure.board.cells[figure.currentCell - 1 + nextTarget];
            if (targetCell.myFigure != null)
            {
                FigureS blackFigure = targetCell.myFigure.GetComponent<FigureS>();
                if (blackFigure != null)
                {
                    blackFigure.Health = blackFigure.Health - damage;
                    break;
                }
            }
            nextTarget--;
        }
    }
    public bool Check()
    {
        int nextTarget = 0;
        while (true) //!!!
        {
            if ((figure.currentCell - 1 + nextTarget) < 0)
            {
                return false;
            }
            CellS targetCell = figure.board.cells[figure.currentCell - 1 + nextTarget];
            if (targetCell.myFigure != null)
            {
                FigureS blackFigure = targetCell.myFigure.GetComponent<FigureS>();
                if (blackFigure != null)
                {
                    return true;
                }
            }
            nextTarget--;
        }
    }
}
