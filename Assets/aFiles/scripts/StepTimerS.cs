using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StepTimerS : MonoBehaviour
{
    //=========================================================== Редактор
    public FigureS[] figures;
    public BlackFigureS[] blackFigures;
    public CreatorS creator;
    public BoardS board;
    public FailScreenS failScreen;
    //=========================================================== Редактор
    public bool isReady;
    public bool happendKill;
    private void Awake()
    {
        happendKill = false;
        isReady = true;
        StartCoroutine(everySecond());
    }
    IEnumerator everySecond()
    {
        yield return new WaitForSeconds(0.35f);
        while (true)
        {
            if (failScreen.isFailed) yield break;
            //=========================================================== Ход белых
            ResearchSequence();
            yield return PlayAct(figures);
            yield return WaitSpam(true);
            ResearchSequence();
            yield return OnlyMove(figures);
            yield return WaitSpam(true);

            //=========================================================== Ход белых
            //=========================================================== Ход чёрных
            ResearchBlackSequence();
            yield return PlayAct(blackFigures);
            yield return WaitSpam(false);
            ResearchBlackSequence();
            yield return OnlyMove(blackFigures);
            yield return WaitSpam(false);

            //=========================================================== Ход чёрных
            yield return null;
        }
    }
    public void ResearchSequence()
    {
        figures = FindObjectsByType<FigureS>(FindObjectsSortMode.None);
        Array.Sort(figures, (figure1, figure2) => figure2.currentCell.CompareTo(figure1.currentCell));
    }
    public void ResearchBlackSequence()
    {
        blackFigures = FindObjectsByType<BlackFigureS>(FindObjectsSortMode.None);
        Array.Sort(blackFigures, (figure1, figure2) => figure1.currentCell.CompareTo(figure2.currentCell));
    }
    IEnumerator OnlyMove(IS.IBlackAndHWhite[] figures_)
    {
        if (happendKill)
        {
            for (int i = 0; i < figures_.Length; i++)
            {
                if (failScreen.isFailed) yield break;
                if (figures_[i] != null)
                {
                    figures_[i].OnlyMove();
                }
                yield return new WaitUntil(() => (isReady));
                yield return null;
            }
            happendKill = false;
        }
    }
    IEnumerator PlayAct(IS.IBlackAndHWhite[] figures_)
    {
        for (int i = 0; i < figures_.Length; i++)
        {
            if (failScreen.isFailed) yield break;
            if (figures_[i] != null)
            {
                figures_[i].Act();
            }
            yield return new WaitUntil(() => (isReady));
            yield return null;
            if (happendKill)
            {
                yield break;
            }
        }
    }
    IEnumerator WaitSpam(bool isWhite)
    {
        
        if (isWhite)
        {
            if (board.cells[1].isLocked == false)
            {
                yield return new WaitForSeconds(0.35f);
                if (failScreen.isFailed) yield break;
                creator.NewOne();
            }
        }
        else
        {
            if (board.cells[board.cells.Length - 2].isLocked == false)
            {
                yield return new WaitForSeconds(0.35f);
                if (failScreen.isFailed) yield break;
                creator.NewBlackOne();
            }
        }
    }
}
