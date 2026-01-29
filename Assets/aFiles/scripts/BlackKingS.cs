using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackKingS : MonoBehaviour
{
    BoardS board;
    private void Awake()
    {
        board = FindAnyObjectByType<BoardS>();
        transform.position = board.cells[board.cells.Length - 1].transform.position;
        board.cells[board.cells.Length - 1].GetFigure(gameObject);
    }
}
