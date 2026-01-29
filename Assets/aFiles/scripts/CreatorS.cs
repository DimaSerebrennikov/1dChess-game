using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CreatorS : MonoBehaviour
{
    //----------------------------------------------------------- Редактор
    public BoardS board;
    public MagicBalanceS magicBalance;
    //=========================================================== Объекты для создания
    public GameObject[] figures; //pawn, horse, bishop, rook, queen
    public GameObject[] figuresBlack; //pawn, horse, bishop, rook, queen
    //=========================================================== Объекты для создания
    //=========================================================== Изображения фигур
    public GameObject[] figuresImage; //pawn, horse, bishop, rook, queen
    public GameObject[] figuresBlackImage; //pawn, horse, bishop, rook, queen
    //=========================================================== Изображения фигур
    public LayerMask myLayerMask;
    public GameObject[] allWhiteCells;
    public GameObject[] allBlackCells;
    //----------------------------------------------------------- Редактор
    //=========================================================== Клеточки для выбора
    Color[] savedColorsWhite;
    Color[] savedColorsBlack;
    Color specialOrange;
    int[] whiteCellInt;
    int[] blackCellInt;
    GameObject[] inCellObjWhite;
    GameObject[] inCellObjBlack;
    SpriteRenderer[] srsWhite;
    SpriteRenderer[] srsBlack;
    //=========================================================== Клеточки для выбора
    //=========================================================== Выбранное
    int whiteSelectedCell;
    int blackSelectedCell;
    List<int> selectedArrayWhite;
    List<int> selectedArrayBlack;
    //=========================================================== Выбранное
    private void Awake()
    {
        whiteSelectedCell = 0;
        blackSelectedCell = 0;
        selectedArrayWhite = new List<int> { 1, 1, 1, 1, 1 };
        selectedArrayBlack = new List<int> { 1, 1, 1, 1, 1 };
        specialOrange = new Color(1f, 0.592f, 0f);
        whiteCellInt = new int[allWhiteCells.Length]; 
        blackCellInt = new int[allBlackCells.Length];
        inCellObjWhite = new GameObject[allWhiteCells.Length];
        inCellObjBlack = new GameObject[allBlackCells.Length];
        //----------------------------------------------------------- Сохранить цвета
        //=========================================================== Найти компонент спрайт рендер
        srsWhite = new SpriteRenderer[allWhiteCells.Length];
        for (int i = 0; i < srsWhite.Length; i++)
        {
            srsWhite[i] = allWhiteCells[i].GetComponent<SpriteRenderer>();
        }
        srsBlack = new SpriteRenderer[allBlackCells.Length];
        for (int i = 0; i < allBlackCells.Length; i++)
        {
            srsBlack[i] = allBlackCells[i].GetComponent<SpriteRenderer>();
        }
        //=========================================================== Найти компонент спрайт рендер
        //=========================================================== Цвета
        savedColorsWhite = new Color[allWhiteCells.Length];
        for (int i = 0; i < allWhiteCells.Length; i++)
        {
            savedColorsWhite[i] = srsWhite[i].color;
        }
        savedColorsBlack = new Color[allBlackCells.Length];
        for (int i = 0; i < allBlackCells.Length; i++)
        {
            savedColorsBlack[i] = srsBlack[i].color;
        }
        //=========================================================== Цвета
        //----------------------------------------------------------- Сохранить цвета
        UpdateWhiteCells();
        UpdateBlackCells();
        //=========================================================== Выделить в начале клетки по умолчанию
        srsWhite[0].color = specialOrange;
        srsBlack[0].color = specialOrange;
        //=========================================================== Выделить в начале клетки по умолчанию
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D raycast = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 10f, myLayerMask);

            if (raycast.collider != null)
            {
                GameObject curObj = raycast.collider.gameObject;
                SpriteRenderer sr = raycast.collider.GetComponent<SpriteRenderer>();
                sr.color = specialOrange;
                for (int i = 0; i < allWhiteCells.Length; i++)
                {
                    if (curObj == allWhiteCells[i])
                    {
                        whiteSelectedCell = i;
                        for (int j = 0; j < srsWhite.Length; j++)
                        {
                            if (i != j)
                            {
                                srsWhite[j].color = savedColorsWhite[j];
                            }
                        }
                    }
                    if (curObj == allBlackCells[i])
                    {
                        blackSelectedCell = i;
                        for (int j = 0; j < srsBlack.Length; j++)
                        {
                            if (i != j)
                            {
                                srsBlack[j].color = savedColorsBlack[j];
                            }
                        }
                    }
                }
            }
        }
    }
    public void NewOne()
    {
        GameObject obj = Instantiate(DetectSelectedCell(whiteSelectedCell, figures, whiteCellInt));
        FigureS figure = obj.GetComponent<FigureS>();
        int spamCell = 1;
        figure.Setting(spamCell, board.cells[spamCell].transform.position);
        board.cells[spamCell].GetFigure(obj);
        UpdateWhiteCells();
    }
    public void NewBlackOne()
    {
        GameObject obj = Instantiate(DetectSelectedCell(blackSelectedCell, figuresBlack, blackCellInt));
        BlackFigureS figure = obj.GetComponent<BlackFigureS>();
        int spamCell = board.cells.Length - 2;
        figure.Setting(spamCell, board.cells[spamCell].transform.position);
        board.cells[spamCell].GetFigure(obj);
        UpdateBlackCells();
    }
    public void NewFigureForCell
        (ref int cellInt_, ref GameObject inCellObj_, Vector2 objCell_,
        GameObject[] figuresImage_, bool isFirst_, ref List<int> selectedArray_)
    {
        if (inCellObj_ != null)
        {
            Destroy(inCellObj_);
        }
        //=========================================================== Работа со списком для случайности
        if (isFirst_)
        {
            selectedArray_ = new List<int> { 1, 1, 1, 1, 1 };
        }
        List<int> randomRel = new List<int> { 1, 1, 1, 1, 1 };
        randomRel = randomRel.Zip(selectedArray_, (x, y) => x * y).ToList();
        //=========================================================== Работа со списком для случайности
        //=========================================================== Слуйчаный объект
        cellInt_ = 0;
        RandomS.Function(randomRel, ref cellInt_);
        selectedArray_[cellInt_] = 0;
        for (int i = 0; i < figuresImage.Length; i++)
        {
            if (cellInt_ == i)
            {
                inCellObj_ = Instantiate(figuresImage_[i], objCell_, Quaternion.identity);
            }
        }
        //=========================================================== Слуйчаный объект
    }
    public void UpdateWhiteCells()
    {
        NewFigureForCell(ref whiteCellInt[0],
            ref inCellObjWhite[0],
            allWhiteCells[0].transform.position,
            figuresImage,
            true,
            ref selectedArrayWhite);
        NewFigureForCell(ref whiteCellInt[1],
            ref inCellObjWhite[1],
            allWhiteCells[1].transform.position,
            figuresImage,
            false,
            ref selectedArrayWhite);
        NewFigureForCell(ref whiteCellInt[2],
                    ref inCellObjWhite[2],
                    allWhiteCells[2].transform.position,
                    figuresImage,
                    false,
                    ref selectedArrayWhite);
    }
    public void UpdateBlackCells()
    {
        NewFigureForCell(ref blackCellInt[0],
            ref inCellObjBlack[0],
            allBlackCells[0].transform.position,
            figuresBlackImage,
            true,
            ref selectedArrayBlack);
        NewFigureForCell(ref blackCellInt[1],
            ref inCellObjBlack[1],
            allBlackCells[1].transform.position,
            figuresBlackImage,
            false,
            ref selectedArrayBlack);
        NewFigureForCell(ref blackCellInt[2],
            ref inCellObjBlack[2],
            allBlackCells[2].transform.position,
            figuresBlackImage,
            false,
            ref selectedArrayBlack);
    }
    public GameObject DetectSelectedCell(int selectedCell, GameObject[] figures_, int[] cellsInt)
    {
        GameObject nextObject = null;
        for (int i = 0; i < cellsInt.Length; i++)
        {
            if (selectedCell == i)
            {
                nextObject = figures_[cellsInt[i]];
                magicBalance.BlameIt(cellsInt[i]);
            }
        }
        return nextObject;
    }
}
