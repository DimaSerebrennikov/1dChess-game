using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApearFigureS : MonoBehaviour
{
    public SpriteRenderer sr;
    float timer;
    Material mat;
    private void Awake()
    {
        timer = 0f;
        sr = GetComponent<SpriteRenderer>();
        mat = sr.material;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer <= 2f)
        {
            mat.SetFloat("_transitionTime", timer / 2f);
        }
    }
}
