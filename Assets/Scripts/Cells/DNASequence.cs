using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DNASequence : MonoBehaviour
{
    [Header("碱基对")]
    public int[] leftSingleChain = new int[5];
    public int[] rightSingleChain = new int[5];
    private int[] dnaChainBackup = new int[10];

    public List<int> indexOfError = new List<int>();
    public int countOfError = 0;

    public virtual void Awake() {
        InitialDNASequence();
    }

    /// <summary>
    /// 初始化DNA序列
    /// </summary>
    public void InitialDNASequence() {
        for (int i = 0; i < leftSingleChain.Length; i++) {
            leftSingleChain[i] = BasePairManager.instance.basePair[BasePairManager.instance.GetRandomNucleBase()];
            rightSingleChain[i] = BasePairManager.instance.basePair[BasePairManager.instance.GetOppositeNucleBase(leftSingleChain[i])];
        }

        SaveDNAChain();
    }

    /// <summary>
    /// 破坏DNA序列
    /// </summary>
    public void BreakDNASequence() {
        Debug.Log(gameObject.name + "破坏DNA");

        switch (Random.Range(0, 3)) {
            case 0: {
                    CreateEmptyChain();
                    break;
                }
            case 1: {
                    CreateEmptyNucleBase();
                    break;
                }
            case 2: {
                    CreateWrongNucleBase();
                    break;
                }
        }
    }

    // 备份DNA链
    private void SaveDNAChain() {
        for (int i = 0; i < 5; i++) {
            dnaChainBackup[i] = leftSingleChain[i];
            dnaChainBackup[i + 5] = rightSingleChain[i];
        }
    }
    /// <summary>
    /// 重新载入DNA链
    /// </summary>
    public void LoadDNAChain() {
        for (int i = 0; i < 5; i++) {
            leftSingleChain[i] = dnaChainBackup[i];
            rightSingleChain[i] = dnaChainBackup[i + 5];
        }

        ClearError();
    }

    public void ClearError() {
        indexOfError.Clear();
        countOfError = 0;
    }

    // 创建空碱基
    public void CreateEmptyNucleBase() {
        int count = Random.Range(1, 6);
        countOfError = count;

        bool[] changable = new bool[leftSingleChain.Length];
        for (int i = 0; i < changable.Length; i++) changable[i] = true;

        while (count != 0)
        {
            // 左链凿空
            if (Random.Range(0, 1.0f) > 0.5f) {
                while (true) {
                    int index = Random.Range(0, leftSingleChain.Length);

                    if (rightSingleChain[index] != 0 && changable[index]) {
                        leftSingleChain[index] = 0;
                        indexOfError.Add(index);

                        changable[index] = false;
                        break;
                    }
                }
            }
            // 右链凿空
            else {
                while (true)
                {
                    int index = Random.Range(0, leftSingleChain.Length);

                    if (leftSingleChain[index] != 0 && changable[index])
                    {
                        rightSingleChain[index] = 0;
                        indexOfError.Add(index+5);

                        changable[index] = false;
                        break;
                    }
                }
            }

            count--;
        }
    }
    // 创建错误碱基
    public void CreateWrongNucleBase() {
        int count = Random.Range(1, 6);
        countOfError = count;

        bool[] changable = new bool[leftSingleChain.Length];
        for (int i = 0; i < changable.Length;i++) changable[i] = true;

        while (count != 0)
        {
            // 左链制差
            if (Random.Range(0, 1.0f) > 0.5f)
            {
                while (true)
                {
                    int index = Random.Range(0, leftSingleChain.Length);

                    if (changable[index])
                    {
                        leftSingleChain[index] = BasePairManager.instance.basePair[BasePairManager.instance.GetRandomNucleBase(BasePairManager.instance.GetNucleBaseByValue(leftSingleChain[index]))];
                        changable[index] = false;
                        indexOfError.Add(index);
                        break;
                    }
                }
            }
            // 右链制差
            else
            {
                while (true)
                {
                    int index = Random.Range(0, rightSingleChain.Length);

                    if (changable[index])
                    {
                        rightSingleChain[index] = BasePairManager.instance.basePair[BasePairManager.instance.GetRandomNucleBase(BasePairManager.instance.GetNucleBaseByValue(rightSingleChain[index]))];
                        changable[index] = false;
                        indexOfError.Add(index+5);
                        break;
                    }
                }
            }

            count--;
        }
    }
    // 单链损失
    public void CreateEmptyChain() {
        countOfError = leftSingleChain.Length;

        if (Random.Range(0, 1.0f) < 0.5f) {
            for (int i = 0; i < leftSingleChain.Length; i++) {
                leftSingleChain[i] = 0;
                indexOfError.Add(i);
            }
        }
        else{
            for (int i = 0; i < leftSingleChain.Length; i++)
            {
                rightSingleChain[i] = 0;
                indexOfError.Add(i+5);
            }
        }
    }

    /// <summary>
    /// 获取错误碱基的数量
    /// </summary>
    /// <returns></returns>
    public int GetCountOfError() {
        return countOfError;
    }

    /// <summary>
    /// 检测是否为错误碱基
    /// </summary>
    /// <param name="_index">碱基下标</param>
    /// <returns></returns>
    public bool CheckError(int _index) {
        if (indexOfError.Contains(_index)) {
            return true;
        }
        return false;
    }

}
