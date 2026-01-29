using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IS : MonoBehaviour
{
    public interface IFigure
    {
        public void Attack();
        public bool Check();
    }
    public interface IBlackAndHWhite
    {
        public float Health { get; set; }
        public void Act();
        public void OnlyMove();
        public void SetOnHealthChange(Action action);
    }
}
