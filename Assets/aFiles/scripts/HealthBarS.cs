using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IS;

public class HealthBarS : MonoBehaviour
{
    public Transform bar; //редактор
    IS.IBlackAndHWhite figure;
    Vector2 startPosition;
    Vector2 startScale;
    float curHealth;
    float maxHealth;
    private void Awake()
    {
        startPosition = bar.localPosition;
        startScale = bar.localScale;
        figure = transform.parent.GetComponent<IBlackAndHWhite>();
        curHealth = 0;
        maxHealth = figure.Health; //должно сработать после скриптов фигуры и спец фигуры
        figure.SetOnHealthChange(CheckCurrentHp);
    }
    public void CheckCurrentHp()
    {
        curHealth = figure.Health;
        if (curHealth < maxHealth)
        {
            bar.localScale = new Vector2(startScale.x * (curHealth / maxHealth), startScale.y);
            bar.localPosition = new Vector2(startPosition.x - startScale.x/2f * (1-(curHealth / maxHealth)),startPosition.y);
        }
    }

}
