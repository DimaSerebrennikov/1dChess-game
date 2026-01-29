using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackRookS : MonoBehaviour, IS.IFigure
{
    BlackFigureS figure;
    public float damage;
    public float health;
    private void Awake()
    {
        health = MagicBalanceS.updatedAllK[3][0];
        damage = MagicBalanceS.updatedAllK[3][1];
        figure = GetComponent<BlackFigureS>();
        figure.Health = health;
    }
    public void Attack()
    {
        figure.CreateHitSound(damage);
        CellS targetCell = figure.board.cells[figure.currentCell - 1];
        if (targetCell.myFigure != null)
        {
            FigureS blackFigure = targetCell.myFigure.GetComponent<FigureS>();
            if (blackFigure != null)
            {
                blackFigure.Health = blackFigure.Health - damage;
            }
        }
    }
    public bool Check()
    {
        CellS targetCell = figure.board.cells[figure.currentCell - 1];
        if (targetCell.myFigure != null)
        {
            FigureS blackFigure = targetCell.myFigure.GetComponent<FigureS>();
            if (blackFigure != null)
            {
                return true;
            }
        }
        return false;
    }
}
