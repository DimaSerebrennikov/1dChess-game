using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptationS : MonoBehaviour
{
    float aspectWH;
    float iphone12K;
    private void Awake()
    {
        iphone12K = 10.82051f;
        aspectWH = (float)Screen.width / (float)Screen.height;
        Camera.main.orthographicSize = iphone12K / aspectWH;
    }
}
