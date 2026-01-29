using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureS : MonoBehaviour, IS.IBlackAndHWhite //должен загружаеться после всех всех скриптов в объекте
{
    //=========================================================== Редактор
    public GameObject soundPref;
    public GameObject soundPrefKick;
    //=========================================================== Редактор
    bool isAnimationActive;
    bool isGettingDamage;
    bool isAfterAnimation;
    bool playMoving;
    public int currentCell;
    public float health; //обновляется в специальном классе фигуры 
    Transform spriteOfObj;
    Vector2 savedLocalPosition;
    public Action attack;
    public Func<bool> check;
    public Action onHealthChange;
    public Action onDefeat;
    HandleAllFigureS handleAllFigure;
    RecordS record;
    StepTimerS stepTimer;
    FailScreenS failScreen;
    public BoardS board;
    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            if (value < health)
            {
                isGettingDamage = true;
            }
            health = value;
            onHealthChange?.Invoke();
            if (health <= 0f)
            {
                if (record != null)
                {
                    record.CurRecord++;
                }
                board.cells[currentCell].OutFigure();
                stepTimer.happendKill = true;
                DetectSoundType();
                Destroy(gameObject);
            }
        }
    }
    private void Awake()
    {
        //=========================================================== HandleAll
        handleAllFigure = FindAnyObjectByType<HandleAllFigureS>();
        record = handleAllFigure.record;
        stepTimer = handleAllFigure.stepTimer;
        board = handleAllFigure.board;
        failScreen = handleAllFigure.failScreen;
        //=========================================================== HandleAll
        spriteOfObj = transform.Find("figure");
        savedLocalPosition = spriteOfObj.localPosition;
        DetectTypeAndSignAttack();
    }
    private void Update()
    {
        //=========================================================== Анимация
        AttackAnimation(0.45f);
        GetDamageAnimation(0.45f);
        AfterAttackAnimation(0.45f);
        AnimationMove(0.2f);
        //=========================================================== Анимация
    }
    public void Act()
    {
        stepTimer.isReady = false;
        //=========================================================== Движение
        if (board.cells[currentCell + 1].isLocked == false)
        {
            playMoving = true;
        }
        //=========================================================== Движение
        //=========================================================== Атака
        else
        {
            if (check != null && check.Invoke())
            {
                isAnimationActive = true;
            }
            else
            {
                stepTimer.isReady = true;
            }
        }
        //=========================================================== Атака
    }
    public void OnlyMove()
    {
        stepTimer.isReady = false;
        if (board.cells[currentCell + 1].isLocked == false)
        {
            playMoving = true;
        }
        else
        {
            stepTimer.isReady = true;
        }
    }
    public void Setting(int currentCell_, Vector2 location_)
    {
        transform.position = location_;
        currentCell = currentCell_;
    }
    public void DetectTypeAndSignAttack()
    {
        IS.IFigure specType = GetComponent<IS.IFigure>();
        if (specType != null)
        {
            attack = specType.Attack;
            check = specType.Check;
        }
    }
    void AttackAnimation(float tarDur)
    {
        if (isAnimationActive)
        {
            isAnimationActive = false;
            TimerS.NewTimer(Result, tarDur, gameObject, Continue);
            void Result()
            {
                isAfterAnimation = true;
                attack?.Invoke();
                stepTimer.isReady = true;
            }
            void Continue(float timer_)
            {
                    float res = Mathf.Lerp(0, -405f, board.curve.Evaluate(timer_ / tarDur));
                    spriteOfObj.rotation = Quaternion.Euler(0f, 0f, res);
            }
        }
    }
    void GetDamageAnimation(float tarDur)
    {
        if (isGettingDamage)
        {
            TimerS.NewTimer(Result, tarDur,gameObject, Continue);
            isGettingDamage = false;
            void Result()
            {
                spriteOfObj.localPosition = savedLocalPosition;
            }
            void Continue(float time_)
            {
                    spriteOfObj.localPosition = spriteOfObj.localPosition +
                    new Vector3(UnityEngine.Random.Range(-0.02f, 0.02f), UnityEngine.Random.Range(-0.02f, 0.02f));
            }
        }
    }
    void AfterAttackAnimation(float tarDur)
    {
        if (isAfterAnimation)
        {
            isAfterAnimation = false;
            TimerS.NewTimer(() => { }, tarDur, gameObject, Continue);
            void Continue(float time_)
            {
                    float res = Mathf.Lerp(-45, 0f, time_ / tarDur);
                    spriteOfObj.rotation = Quaternion.Euler(0f, 0f, res);
            }
        }
    }
    void AnimationMove(float tarDur)
    {
        if (playMoving)
        {
            playMoving = false;
            TimerS.NewTimer(Resutl, tarDur, gameObject, Continue);
            void Resutl()
            {
                currentCell++;
                board.cells[currentCell - 1].OutFigure(); //прошлая ячейка открывается
                board.cells[currentCell].GetFigure(gameObject); //текущая ячейка закрывается
                transform.position = board.cells[currentCell].transform.position;
                if (currentCell == board.cells.Length - 2)
                {
                    failScreen.Fail();
                }
                stepTimer.isReady = true;
            }
            void Continue(float time)
            {
                    Vector2 res = Vector2.Lerp(transform.position, board.cells[currentCell + 1].transform.position, time / tarDur);
                    transform.position = res;
            }
        }
    }
    public void SetOnHealthChange(Action action)
    {
        onHealthChange += action;
    }
    void DetectSoundType()
    {
        GameObject newObj = Instantiate(soundPref);
        if (currentCell >= 6 && currentCell <= 8)
        {
            newObj.GetComponent<SoundS>().ChooseTypeAndPlay(true);
        }
        else
        {
            newObj.GetComponent<SoundS>().ChooseTypeAndPlay(false);
        }
    }
    public void CreateHitSound(float damage)
    {
        GameObject newObj = Instantiate(soundPrefKick);
        if (damage > 1.9f)
        {
            newObj.GetComponent<SoundS>().ChooseTypeAndPlay(true);
        }
        else
        {
            newObj.GetComponent<SoundS>().ChooseTypeAndPlay(false);
        }
    }
}
