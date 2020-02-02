using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dying : IAction
{
    GameObject go;
    public float timer;
    public float actionTime;

    public void Action()
    {
        if (!GameManager.instance.GameStatu()) return;

        if (timer <= 0)
        {
            if (GUIManager.instance.dnaPanel.activeSelf && GUIManager.instance.selectedCell.Equals(go)) {
                GUIManager.instance.DisableDNAPanel();
            }

            GameManager.instance.ReduceDiseaseCell();
            GameManager.instance.ReduceNormalCell();

            GameObject.Destroy(go);
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    public void Initial(GameObject _go, float _actionTime)
    {
        go = _go;
        actionTime = _actionTime;
        timer = actionTime;
    }

    public void ResetTimer()
    {
        timer = actionTime;
    }
}
