using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SalesmanAssort", menuName = "ScriptableObjects/Salesman_ShopAssort", order = 7)]
public class SalesmanAssort : ScriptableObject
{
    [SerializeField]
    List<int> changeIntervals;

    [SerializeField]
    List<int> initMoney;

    [SerializeField]
    List<string> goodsToSale_One;
    [SerializeField]
    List<float> averageProbabs_One;
    [SerializeField]
    List<int> averageCounts_One;

    [SerializeField]
    List<string> goodsToSale_Two;
    [SerializeField]
    List<float> averageProbabs_Two;
    [SerializeField]
    List<int> averageCounts_Two;

    [SerializeField]
    List<string> goodsToSale_Three;
    [SerializeField]
    List<float> averageProbabs_Three;
    [SerializeField]
    List<int> averageCounts_Three;

    public List<string> GetGoods_One
    {
        get {return goodsToSale_One;}
    }

    public List<string> GetGoods_Two
    {
        get {return goodsToSale_Two;}
    }

    public List<string> GetGoods_Three
    {
        get {return goodsToSale_Three;}
    }

    public List<float> GetGoodsProbs_One
    {
        get {return averageProbabs_One;}
    }

    public List<float> GetGoodsProbs_Two
    {
        get {return averageProbabs_Two;}
    }

    public List<float> GetGoodsProbs_Three
    {
        get {return averageProbabs_Three;}
    }

    public List<int> GetGoodsVols_One
    {
        get {return averageCounts_One;}
    }

    public List<int> GetGoodsVols_Two
    {
        get {return averageCounts_Two;}
    }

    public List<int> GetGoodsVols_Three
    {
        get {return averageCounts_Three;}
    }

    public List<int> GetIntervals
    {
        get {return changeIntervals;}
    }

    public List<int> GetMoney
    {
        get {return initMoney;}
    }
}
