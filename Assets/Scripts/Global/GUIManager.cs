using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GUIManager : MonoBehaviour
{
    public static GUIManager instance;
    [Header("操作状态")]
    public int nucleBaseValue = 0;
    public int countOfError;
    public GameObject cursorFollower;

    [Header("对象设置")]
    public GameObject selectedCell;
    public int[] leftChain;
    public int[] rightChain;
    [Space]
    public GameObject canvas;
    public GameObject dnaPanel;
    public GameObject dnaPanel_Up;
    public GameObject nucleBasePanel;
    public GameObject nucleoBase;
    public GameObject recordPanel;
    public GameObject guidePanel;
    public GameObject playerVictoryPanel;
    public GameObject playerLosePanel;
    public Image timer;

    [Space]
    public GameObject backgroup_Top;

    [Space]
    public Text count_NormalCell;
    public Text count_DiseaseCell;
    public Text count_CancerCell;
    public Text count_Theradhold;
    [Space]
    public GameObject prefab_CursorFollower;

    [Header("Audio播放器")]
    public GUISoundAudio guiSoundAudio;

    Action<bool> OnDisplay;


    void Awake()
    {
      instance = this;

        OnDisplay = delegate(bool _b) { Camera.main.GetComponent<PlayerController>().SetSelected(_b); };
    }

    /// <summary>
    /// 显示DNA面板
    /// </summary>
    /// <param name="_lc">左链</param>
    /// <param name="_rc">右链</param>
    /// <param name="_cell">细胞</param>
    public void DisplayDNAPanel(int[] _lc,int [] _rc ,GameObject _cell) {
        ClearDNAPanel();

        OnDisplay(true);

        selectedCell = _cell;
        leftChain = _lc;
        rightChain = _rc;

        countOfError = selectedCell.GetComponent<CellController>().GetCountOfError();

        dnaPanel.SetActive(true);
        dnaPanel_Up.SetActive(true);

        for (int i = 0; i < _lc.Length; i++) {
            GameObject go = Instantiate(nucleoBase,Vector3.zero,Quaternion.identity,dnaPanel.transform);
            go.transform.name = i.ToString();
            go.GetComponent<NucleBaseData>().Initial(_lc[i], _rc[i]);

            if (_cell.GetComponent<CellController>().CheckError(i)) {
                go.tag = "ErrorNucleoBase";
            }
        }

        for (int i = 0; i < _rc.Length; i++) {
            GameObject go = Instantiate(nucleoBase, Vector3.zero, Quaternion.identity, dnaPanel.transform);
            go.transform.name = (i+5).ToString();
            go.GetComponent<NucleBaseData>().Initial(_rc[i],_lc[i]);

            if (_cell.GetComponent<CellController>().CheckError(i+5))
            {
                go.tag = "ErrorNucleoBase";
            }
        }
    }

    /// <summary>
    /// 重设DNA面板 当修复失败时
    /// </summary>
    public void ResetDNAPanel() {
        ClearDNAPanel();

        ClearNucleBaseValue();
        DisableNucleBasePanel();

        guiSoundAudio.Play_MatchFail();

        countOfError = selectedCell.GetComponent<CellController>().GetCountOfError();

        for (int i = 0; i < leftChain.Length; i++)
        {
            GameObject go = Instantiate(nucleoBase, Vector3.zero, Quaternion.identity, dnaPanel.transform);
            go.transform.name = i.ToString();
            go.GetComponent<NucleBaseData>().Initial(leftChain[i], rightChain[i]);

            if (selectedCell.GetComponent<CellController>().CheckError(i))
            {
                go.tag = "ErrorNucleoBase";
            }
        }

        for (int i = 0; i < rightChain.Length; i++)
        {
            GameObject go = Instantiate(nucleoBase, Vector3.zero, Quaternion.identity, dnaPanel.transform);
            go.transform.name = (i + 5).ToString();
            go.GetComponent<NucleBaseData>().Initial(rightChain[i], leftChain[i]);

            if (selectedCell.GetComponent<CellController>().CheckError(i + 5))
            {
                go.tag = "ErrorNucleoBase";
            }
        }
    }

    /// <summary>
    /// 关闭DNA面板
    /// </summary>
    public void DisableDNAPanel()
    {
        dnaPanel.SetActive(false);

        selectedCell = null;
        leftChain = rightChain = null;

        countOfError = 0;

        ClearNucleBaseValue();
        nucleBasePanel.SetActive(false);
        dnaPanel_Up.SetActive(false);

        OnDisplay(false);

        guiSoundAudio.Play_DisableDNAPanel();
    }

    /// <summary>
    /// 清空DNA面板
    /// </summary>
    private void ClearDNAPanel() {
        for (int i = 0; i < dnaPanel.transform.childCount; i++) {
            Destroy(dnaPanel.transform.GetChild(i).gameObject);
        }
    }

    /// <summary>
    /// 显示碱基面板
    /// </summary>
    public void DisplayNucleBasePanel() {
        nucleBasePanel.SetActive(true);

        ClearNucleBaseValue();

        nucleBasePanel.transform.position = Input.mousePosition;
    }

    /// <summary>
    /// 关闭碱基面板
    /// </summary>
    public void DisableNucleBasePanel() {
        nucleBasePanel.SetActive(false);

        guiSoundAudio.Play_DisableDNAPanel();
    }

    /// <summary>
    /// 设定当前碱基值
    /// </summary>
    /// <param name="_value"></param>
    public void SetNucleBaseValue(int _value) {
        nucleBaseValue = _value;

        cursorFollower = Instantiate(prefab_CursorFollower, Vector3.zero, Quaternion.identity,canvas.transform);
        cursorFollower.SendMessage("SetSprite", _value);

        guiSoundAudio.Play_ClickNucleBase();
        DisableNucleBasePanel();
    }
    /// <summary>
    /// 清除当前选择的碱基值和GUI显示
    /// </summary>
    public void ClearNucleBaseValue() {
        nucleBaseValue = 0;
        if(cursorFollower != null)
        Destroy(cursorFollower);
    }

    /// <summary>
    /// 成功修复碱基并检测是否全部修复完成
    /// </summary>
    private void MatchSuccess() {
        countOfError--;
        ClearNucleBaseValue();

        guiSoundAudio.Play_ClickNucleBase();

        // 全部修复完成
        if (countOfError.Equals(0)) {
            selectedCell.SendMessage("FixSuccess");

            DisableDNAPanel();
            guiSoundAudio.Play_MatchSuccess();
        }
    }

    /// <summary>
    /// 刷新数字
    /// </summary>
    public void ReflashCount() {
        count_CancerCell.text = GameManager.instance.count_CancerCell.ToString();
        count_NormalCell.text = GameManager.instance.count_NormalCell.ToString();
        count_DiseaseCell.text = GameManager.instance.count_DiseaseCell.ToString();
    }

    public void SetThreadHold(int _v) {
        count_Theradhold.text = _v.ToString();
    }

    /// <summary>
    /// 游戏开始 显示计分板 并初始化阈值
    /// </summary>
    /// <param name="_v"></param>
    public void StartGame(int _v)
    {
        SetThreadHold(_v);
        ReflashCount();
        recordPanel.SetActive(true);

        backgroup_Top.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        backgroup_Top.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1,-1) * 50.0f);

        guidePanel.GetComponent<Animator>().Play("DisableGuidePanel");
        Destroy(guidePanel, 3f);
    }


    public void ReflashTimer(float _v)
    {
        timer.fillAmount = _v;
    }

    public void DisplayVictoryPanel() {
        playerVictoryPanel.SetActive(true);
    }
    public void DisplayLosePanel() {
        playerLosePanel.SetActive(true);
    }
}
