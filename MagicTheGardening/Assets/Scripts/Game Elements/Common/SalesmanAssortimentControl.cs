using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameControllers;

namespace GameElement
{
    public class SalesmanAssortimentControl : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField]
        SalesmanAssort salesmanAssort;
        int periodAssort;
        void Awake()
        {
            // After scene load try to get the period parameter of the assortiment scenario
            int day = FindObjectOfType<DayNightFaderScript>().daysPassed;
            List<int> intervals = salesmanAssort.GetIntervals;

            for (int i = 0; i < intervals.Count; ++i)
            {
                if (day <= intervals[i])
                {
                    periodAssort = i;
                    break;
                }
            }
        }

        void RenewAssortment()
        {
            StorageScript myStorage = GetComponent<StorageScript>();
            List<string> goodsN = new List<string>();
            List<float> goodsP = new List<float>();
            List<int> goodsV = new List<int>();

            if (periodAssort == 0)
            {
                goodsN = salesmanAssort.GetGoods_One;
                goodsP = salesmanAssort.GetGoodsProbs_One;
                goodsV = salesmanAssort.GetGoodsVols_One;
            }

            if (periodAssort == 1)
            {
                goodsN = salesmanAssort.GetGoods_Two;
                goodsP = salesmanAssort.GetGoodsProbs_Two;
                goodsV = salesmanAssort.GetGoodsVols_Two;
            }

            if (periodAssort == 2)
            {
                goodsN = salesmanAssort.GetGoods_Three;
                goodsP = salesmanAssort.GetGoodsProbs_Three;
                goodsV = salesmanAssort.GetGoodsVols_Three;
            }

            myStorage.ClearStorage();

            myStorage.AddMoneyVol(salesmanAssort.GetMoney[periodAssort]);

            for (int i = 0; i < goodsN.Count; ++i)
            {
                if (Random.Range(0.0f, 1.0f) < goodsP[i])
                {
                    myStorage.AddProduct(goodsN[i], goodsV[i]);
                }
            }
        }
    }
}