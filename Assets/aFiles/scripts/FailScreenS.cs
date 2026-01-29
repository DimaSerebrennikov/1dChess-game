using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailScreenS : MonoBehaviour
{
    public bool isFailed;
    public LayerMask layerMask;
    public BoardS board;
    public TextMeshPro tmp;
    private void Awake()
    {
        isFailed = false;
    }
    public void Fail()
    {
        isFailed = true;
    }
    private void Update()
    {
        if (isFailed)
        {
            tmp.text = "pick up pieces yourself, to start a new one";
            tmp.fontSize = 5f;
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D raycast = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 10f, layerMask);
                if (raycast.collider != null)
                {
                    Destroy(raycast.collider.gameObject.GetComponent<CellS>().myFigure);
                    raycast.collider.gameObject.GetComponent<CellS>().OutFigure();

                }
            }
            bool allDeleted = true;
            for (int i = 0; i < board.cells.Length; i++)
            {
                if (i > 0 && i < board.cells.Length - 1)
                {
                    if (board.cells[i].isLocked)
                    {
                        allDeleted = false;
                    }
                }
            }
            if (allDeleted)
            {
                SceneManager.LoadScene("Main");
            }
        }
    }

}
