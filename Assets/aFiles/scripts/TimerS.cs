using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerS : MonoBehaviour
{
    static float timer;
    static List<float> finishTime;
    static List<float> startTime;
    static List<Action> actions;
    static List<bool> doned;
    static List<Action<float>> contActions;
    static List<GameObject> initiators;
    private void Awake()
    {
        timer = 0f;
        finishTime = new List<float>();
        actions = new List<Action>();
        doned = new List<bool>();
        contActions = new List<Action<float>>();
        startTime = new List<float>();
        initiators = new List<GameObject>();
    }
    static public void NewTimer(Action action, float targetTime, GameObject curInitiator, Action<float> contAction = null)
    {
        finishTime.Add(timer + targetTime);
        startTime.Add(timer);
        actions.Add(action);
        doned.Add(false);
        contActions.Add(contAction);
        initiators.Add(curInitiator);
    }
    private void Update()
    {
        timer += Time.deltaTime;
        for (int i = 0; i < finishTime.Count; i++)
        {
            if (initiators[i] == null)
            {
                RemoveDoned(i);
                i--;
            }
            else
            {
                if (timer > finishTime[i])
                {
                    actions[i].Invoke();
                    doned[i] = true;
                }
                if (doned[i])
                {
                    RemoveDoned(i);
                    i--;
                }
            }
        }
        ForContActions(timer);
    }
    static void RemoveDoned(int number)
    {
        finishTime.RemoveAt(number);
        actions.RemoveAt(number);
        doned.RemoveAt(number);
        contActions.RemoveAt(number);
        startTime.RemoveAt(number);
        initiators.RemoveAt(number);
    }
    static void ForContActions(float timer)
    {
        for (int i = 0; i < contActions.Count; i++)
        {
            if (contActions[i] != null)
            {
                float curTime = timer - startTime[i];
                contActions[i].Invoke(curTime);
            }
        }
    }
}
