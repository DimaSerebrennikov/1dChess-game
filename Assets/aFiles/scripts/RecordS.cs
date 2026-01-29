using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecordS : MonoBehaviour
{
    int curRecord;
    public TextMeshPro tmp;
    public int CurRecord
    {
        get
        {
            return curRecord;
        }
        set
        {
            curRecord = value;
            tmp.SetText(curRecord.ToString());
            if (curRecord > PlayerPrefs.GetInt("record"))
            {
                PlayerPrefs.SetInt("record", curRecord);
            }
        }
    }
    private void Awake()
    {
        curRecord = 0;
    }
}
