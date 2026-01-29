using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseS : MonoBehaviour, IS.IFigure
{
    FigureS figure;
    public float damage;
    public float health;
    private void Awake()
    {
        health = MagicBalanceS.updatedAllK[1][0];
        damage = MagicBalanceS.updatedAllK[1][1];
        figure = GetComponent<FigureS>();
        figure.Health = health;
    }
    public void Attack()
    {
        figure.CreateHitSound(damage);
        int nTargetCell = figure.currentCell + 2;
        if (nTargetCell > figure.board.cells.Length - 1)
        {
            nTargetCell = figure.board.cells.Length - 1;
        }
        CellS targetCell = figure.board.cells[nTargetCell];
        if (targetCell.myFigure != null)
        {
            BlackFigureS blackFigure = targetCell.myFigure.GetComponent<BlackFigureS>();
            if (blackFigure != null)
            {
                blackFigure.Health = blackFigure.Health - damage;
            }
        }
    }
    public bool Check()
    {
        int nTargetCell = figure.currentCell + 2;
        if (nTargetCell > figure.board.cells.Length - 1)
        {
            nTargetCell = figure.board.cells.Length - 1;
        }
        CellS targetCell = figure.board.cells[nTargetCell];
        if (targetCell.myFigure != null)
        {
            BlackFigureS blackFigure = targetCell.myFigure.GetComponent<BlackFigureS>();
            if (blackFigure != null)
            {
                return true;
            }
        }
        return false;
    }
}
