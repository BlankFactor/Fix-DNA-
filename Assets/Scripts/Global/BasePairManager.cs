using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasePairManager:MonoBehaviour
{
    public static BasePairManager instance;

    public Dictionary<NucleoBase, int> basePair = new Dictionary<NucleoBase, int>();

    [Header("精灵")]
    public Sprite sprite_NormalCell;
    public Sprite sprite_CancerCell;
    public Sprite sprite_DiseaseCell;
    public Sprite sprite_HealthCell;
    [Space]
    public Sprite sprite_NucleBase_A;
    public Sprite sprite_NucleBase_T;
    public Sprite sprite_NucleBase_C;
    public Sprite sprite_NucleBase_G;
    public Sprite sprite_NucleBase_Null;

    public enum NucleoBase {
        A = 0,
        T,
        C,
        G,
        Null
    };

    /// <summary>
    /// 随机获取碱基
    /// </summary>
    /// <returns></returns>
    public NucleoBase GetRandomNucleBase() {
        NucleoBase[] nbs = NucleoBase.GetValues(typeof(NucleoBase)) as NucleoBase[];
        Random random = new Random();
        NucleoBase nb = nbs[Random.Range(0, nbs.Length - 1)];

        return nb;
    }

    /// <summary>
    /// 随机获得指定碱基以外的碱基
    /// </summary>
    /// <param name="_nb">指定的碱基</param>
    /// <returns></returns>
    public NucleoBase GetRandomNucleBase(NucleoBase _nb)
    {
        NucleoBase[] nbs = NucleoBase.GetValues(typeof(NucleoBase)) as NucleoBase[];
        Random random = new Random();
        NucleoBase nb = nbs[Random.Range(0, nbs.Length - 1)]; ;

        while (nb.Equals(_nb)) {
            nb = nbs[Random.Range(0, nbs.Length - 1)];
        }

        return nb;
    }

    /// <summary>
    /// 获取可匹配碱基
    /// </summary>
    /// <param name="_nb">需要配对的碱基</param>
    /// <returns></returns>
    public NucleoBase GetOppositeNucleBase(NucleoBase _nb) {
        switch (_nb) {
            case NucleoBase.A:return NucleoBase.T;
            case NucleoBase.T:return NucleoBase.A;
            case NucleoBase.C:return NucleoBase.G;
            case NucleoBase.G:return NucleoBase.C;
            default:return NucleoBase.Null;
        }
    }

    public NucleoBase GetOppositeNucleBase(int _value)
    {
        switch (_value)
        {
            case -1: return NucleoBase.T;
            case 1: return NucleoBase.A;
            case -2: return NucleoBase.G;
            case 2: return NucleoBase.C;
            default: return NucleoBase.Null;
        }
    }

    /// <summary>
    /// 通过值获取相对应碱基
    /// </summary>
    /// <param name="_value"></param>
    /// <returns></returns>
    public NucleoBase GetNucleBaseByValue(int _value) {
        foreach (var i in basePair.Keys) {
            if (basePair[i].Equals(_value)) {
                return i;
            }
        }
        return NucleoBase.Null;
    }

    public Sprite GetSprite(int _value) {
        switch (_value)
        {
            case -1: return sprite_NucleBase_A;
            case 1: return sprite_NucleBase_T;
            case -2: return sprite_NucleBase_C;
            case 2: return sprite_NucleBase_G;
            case 3:return sprite_HealthCell;
            default: return sprite_NucleBase_Null;
        }
    }

    protected void InitialDictionary()
    {
        basePair.Add(NucleoBase.A, -1);
        basePair.Add(NucleoBase.T, 1);
        basePair.Add(NucleoBase.C, -2);
        basePair.Add(NucleoBase.G, 2);
    }

    private void Awake()
    {
        instance = this;

        InitialDictionary();
    }
}
