using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHorseS : MonoBehaviour, IS.IFigure
{
    BlackFigureS figure;
    public float damage;
    public float health;
    private void Awake()
    {
        health = MagicBalanceS.updatedAllK[1][0];
        damage = MagicBalanceS.updatedAllK[1][1];
        figure = GetComponent<BlackFigureS>();
        figure.Health = health;
    }
    public void Attack()
    {
        figure.CreateHitSound(damage);
        int nTargetCell = figure.currentCell - 2;
        if (nTargetCell < 0)
        {
            nTargetCell = 0;
        }
        CellS targetCell = figure.board.cells[nTargetCell];
        if (targetCell.myFigure != null)
        {
            FigureS figure = targetCell.myFigure.GetComponent<FigureS>();
            if (figure != null)
            {
                figure.Health = figure.Health - damage;
            }
        }
    }
    public bool Check()
    {
        int nTargetCell = figure.currentCell - 2;
        if (nTargetCell < 0)
        {
            nTargetCell = 0;
        }
        CellS targetCell = figure.board.cells[nTargetCell];
        if (targetCell.myFigure != null)
        {
            FigureS figure = targetCell.myFigure.GetComponent<FigureS>();
            if (figure != null)
            {
                return true;
            }
        }
        return false;
    }
}
