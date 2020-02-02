using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellController : DNASequence
{
    [Header("细胞预置   ")]
    public GameObject cellPrefab;

    [Header("细胞状态")]
    public bool mutable = true;
    [Space]
    public bool cancerCell = false;
    public bool diseasedCell = false;

    [Header("细胞运动设置")]

    [Header("DNA突变设置")]
    IAction action;
    public int probability_Range;
    public int probability_DeseaseCell;
    public int probability_CancerCell;

    [Space]
    public float spliteTime_Normal;
    public float spliteTime_Aberrant;
    [Space]
    public float lifeCycle;

    public override void Awake()
    {
        base.Awake();
        SetNormalCell();
    }

    private void FixedUpdate()
    {
        Mutate();
        action.Action();
    }

    private void OnClick() {
        mutable = false;

        GUIManager.instance.DisplayDNAPanel(leftSingleChain,rightSingleChain,gameObject);
    }

    /// <summary>
    /// 随时间发展而突变
    /// </summary>
    private void Mutate() {
        if (!mutable) { return; }

        if (Random.Range(0, probability_Range) < probability_DeseaseCell)
        {
            SetDiseaseCell();
            return;
        }

        if (Random.Range(0, probability_Range) < probability_CancerCell)
        {
            SetCancerCell();
            return;
        }
    }

    #region 细胞状态设置
    public void SetCancerCell() {
        cancerCell = true;
        mutable = false;

        GetComponent<SpriteRenderer>().sprite = BasePairManager.instance.sprite_CancerCell;

        action = new AberrantSplite();
        action.Initial(cellPrefab, spliteTime_Aberrant);

        BreakDNASequence();

        GameManager.instance.AddCancerCell();
    }
    public void SetDiseaseCell() {
        diseasedCell = true;
        mutable = false;

        GetComponent<SpriteRenderer>().sprite = BasePairManager.instance.sprite_DiseaseCell;

        action = new Dying();
        action.Initial(gameObject, lifeCycle);

        BreakDNASequence();

        GameManager.instance.AddDiseaseCell();
    }
    public void SetNormalCell() {
        mutable = true;
        diseasedCell = false;
        cancerCell = false;

        GetComponent<SpriteRenderer>().sprite = BasePairManager.instance.sprite_NormalCell;

        action = new NormalSplite();
        action.Initial(cellPrefab, spliteTime_Normal);

        GameManager.instance.AddNormalCell();
    }
    #endregion

    /// <summary>
    /// 细胞DNA修复完成
    /// </summary>
    private void FixSuccess() {
        // 通知管理器修改数据
        if (diseasedCell)
        {
            GameManager.instance.ReduceDiseaseCell();
        }
        else {
            GameManager.instance.ReduceCancerCell();
        }

        diseasedCell = false;
        cancerCell = false;

        GetComponent<SpriteRenderer>().sprite = BasePairManager.instance.GetSprite(3);

        action = new NormalSplite();
        action.Initial(cellPrefab, spliteTime_Normal);

        LoadDNAChain();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.transform.tag.Equals("Cell")) return;

        Vector2 dir =Vector2.zero;
        dir = collision.transform.position - transform.position;

        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * 30.0f);
    }
}
