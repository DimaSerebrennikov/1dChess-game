using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingS : MonoBehaviour
{
    BoardS board;
    private void Awake()
    {
        board = FindAnyObjectByType<BoardS>();
        transform.position = board.cells[0].transform.position;
        board.cells[0].GetFigure(gameObject);
    }
}
