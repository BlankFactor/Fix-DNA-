using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AberrantSplite : IAction
{
    GameObject go;
    public float timer;
    public float actionTime;

    public void Action()
    {
        if (!GameManager.instance.GameStatu()) return;

        if (timer <= 0)
        {
            GameObject g = GameObject.Instantiate(go, go.transform.position + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0), Quaternion.identity);
            g.transform.name = Time.time.ToString();
            g.SendMessage("ClearError");
            g.SendMessage("SetCancerCell");
            ResetTimer();
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
