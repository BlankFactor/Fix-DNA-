using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NucleBaseData : MonoBehaviour
{
    private int value;
    private int oppsiteValue;
    
    private Button btn;

    private void Start()
    {
        btn = GetComponent<Button>();

        if (tag.Contains("Error"))
        {
            btn.onClick.AddListener(delegate () { Match(GUIManager.instance.nucleBaseValue); });

            // 临时使用
            Color c = GetComponent<Image>().color;
            c.a = 0.5f;
            GetComponent<Image>().color = c;
        }
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="_value">碱基值</param>
    /// <param name="_oppValue">配对碱基值</param>
    public void Initial(int _value,int _oppValue)
    {
        value = _value;
        oppsiteValue = _oppValue;

        ChangeSprite();
    }

    public void ChangeSprite() {
        GetComponent<Image>().sprite = BasePairManager.instance.GetSprite(value);
    }

    // 碱基配对
    private void Match(int _value) {
        if (_value.Equals(0)) return;

        // 配对成功
        if ((_value + oppsiteValue).Equals(0))
        {
            GetComponent<Image>().sprite = BasePairManager.instance.GetSprite(-oppsiteValue);

            GUIManager.instance.SendMessage("MatchSuccess");

            Color c = GetComponent<Image>().color;
            c.a = 1f;
            GetComponent<Image>().color = c;

            // 关闭GUI检测
            btn.interactable = false;
        }
        else {
            GUIManager.instance.ResetDNAPanel();
        }
    }

    public int GetValue() { return value; }
    public int GetOppValue() { return oppsiteValue; }
}
