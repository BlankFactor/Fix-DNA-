using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAction
{
    void Action();
    void Initial(GameObject _go,float _actionTime);
    void ResetTimer();
}
