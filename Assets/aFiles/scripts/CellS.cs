using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FigureS;

public class CellS : MonoBehaviour
{
    public bool isLocked;
    public GameObject myFigure;
    private void Awake()
    {
        isLocked = false;
    }
    public void GetFigure(GameObject objectInCell)
    {
        myFigure = objectInCell;
        isLocked = true;
    }
    public void OutFigure()
    {
        isLocked = false;
        myFigure = null;
    }
}
