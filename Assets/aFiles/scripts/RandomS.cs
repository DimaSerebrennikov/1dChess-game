using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomS : MonoBehaviour
{
    static public void Function(List<int> myList, ref int result)//в результате послучаем 1 случайное число от 0 до вписанного количества элементов, с шаносом соотношения значений. Значение 0 = 0%
    {
        System.Random myRandomer = new System.Random();
        int counter = 0;
        for (int i = 0; i < myList.Count; i++)
        {
            if (myList[i] < 0)
            {
                myList[i] = 1;
            }
            counter = counter + myList[i];
        }
        int randomer = myRandomer.Next(counter);
        for (int i = 0, tempFromList = 0; i < myList.Count; i++)
        {
            tempFromList = tempFromList + myList[i];
            if (randomer < tempFromList)
            {
                result = i;
                break;
            }
        }
    }
}
