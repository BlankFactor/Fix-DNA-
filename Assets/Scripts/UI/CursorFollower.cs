using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorFollower : MonoBehaviour
{
    void Update()
    {
        Follow();
    }

    void Follow() {
        while (transform.position != Input.mousePosition)
        {
            transform.position = Vector3.Lerp(transform.position, Input.mousePosition, 0.8f);
        }
    }

    void SetSprite(int _value) {
        GetComponent<Image>().sprite = BasePairManager.instance.GetSprite(_value);
    }
}
