﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GameControllers;

namespace GameElement
{
    public class StorageScript : MonoBehaviour
    {
        [System.Serializable]
        public struct myProductStorage
        {
            public ProductCommon product;
            public int count;

            public myProductStorage(ProductCommon pc, int val)
            {
                this.product = pc;
                this.count = val;
            }

            public void AddCount(int newCount)
            {
                Debug.Log("Prev count: " + count.ToString());
                count += newCount;
                Debug.Log("After count: " + count.ToString());
            }
        }

        [SerializeField] // Just temporary case, only for game test and test player start
        List<myProductStorage> myStorage;
        int myMoneyStorage;
        ProductCommon handProduct;

        public ProductCommon OnHand
        {
            get { return handProduct; }
            set { handProduct = value; }
        }

        public int GetStorageLength()
        {
            return myStorage.Count;
        }

        public ProductCommon GetStorageProduct(int num)
        {
            if (num < myStorage.Count)
            {
                return myStorage[num].product;
            }
            else
            {
                return null;
            }
        }

        public int GetStorageProductVol(int num)
        {
            if (num < myStorage.Count)
            {
                return myStorage[num].count;
            }
            else
            {
                return 0;
            }
        }

        // Use this for initialization
        void Start()
        {
            //myStorage = new List<myProductStorage>(); // It is just temporary
            myMoneyStorage = 0;
            handProduct = null;
        }

        public void AddProduct(string productName, int value)
        {
            int ind = myStorage.FindIndex(x => x.product.productName == productName);
            if (ind >= 0)
            {
                myProductStorage mps = new myProductStorage(myStorage[ind].product, myStorage[ind].count + value);
                myStorage[ind] = mps;
                //myStorage[ind].AddCount(value);
            }
            else
            {
                myProductStorage mPS = new myProductStorage (GlobalGameData.instance.GetProductByName(productName), value);
                myStorage.Add(mPS);
            }
        }

        public void RemoveProduct(string productName, int value)
        {
            int ind = myStorage.FindIndex(x => x.product.productName == productName);
            if (ind < 0)
            {
                return;
            }
            else
            {
                // Check how mush we can remove from product
                int val = myStorage[ind].count;
                if (val > value)
                {
                    myProductStorage mps = new myProductStorage(myStorage[ind].product, myStorage[ind].count - 1);
                    myStorage[ind] = mps;
                    //myStorage[ind].AddCount(-1 * value);
                    //Debug.Log("STORAGE RemoveProduct func, myStorage[ind].count: " + myStorage[ind].count.ToString());
                }
                else
                {
                    myStorage.RemoveAt(ind);
                }
            }
        }

        public void BuyProduct(string productName, int value)
        {
            int allProductCost = 0;
            allProductCost = GlobalGameData.instance.GetProductByName(productName).cost * value;
            if (allProductCost > myMoneyStorage)
            {
                return;
            }

            myMoneyStorage -= allProductCost;
            AddProduct(productName, value);
        }

        public void SellProduct(string productName, int value)
        {
            int ind = myStorage.FindIndex(x => x.product.productName == productName);
            if (ind < 0)
            {
                return;
            }
            else
            {
                if (myStorage[ind].count < value)
                {
                    return;
                }
                else if (myStorage[ind].count > value)
                {
                    myProductStorage mps = new myProductStorage(myStorage[ind].product, myStorage[ind].count - 1);
                    myStorage[ind] = mps;

                    //myStorage[ind].AddCount(-1 * value);
                    myMoneyStorage += GlobalGameData.instance.GetProductByName(productName).cost * value;
                }
                else if (myStorage[ind].count == value)
                {
                    myMoneyStorage += GlobalGameData.instance.GetProductByName(productName).cost * value;
                    myStorage.RemoveAt(ind);
                }
            }
        }

        public void FromStorageToHand(string productName)
        {
            int ind = myStorage.FindIndex(x => x.product.productName == productName);
            if (ind < 0)
            {
                return;
            }
            
            if (handProduct != null)
            {
                AddProduct(handProduct.productName, 1);
                handProduct = null;
            }

            handProduct = myStorage[ind].product;
            RemoveProduct(productName, 1);
        }

        public void FromHandToStorage()
        {
            if (handProduct == null)
            {
                return;
            }

            AddProduct(handProduct.productName, 1);
            handProduct = null;
        }

        public void FromWorldToHand(string productName)
        {
            FromHandToStorage();

            handProduct = GlobalGameData.instance.GetProductByName(productName);
        }

        public string FromHandToWorld()
        {
            string toReturn = "";
            if (handProduct != null)
            {
                toReturn = handProduct.productName;
                handProduct = null;
            }

            return toReturn;
        }

        public int HasProduct(string prodName)
        {
            int stEl = myStorage.FindIndex(x => x.product.productName == prodName);
            if (stEl >= 0)
            {
                return myStorage[stEl].count;
            }
   
            return 0;
        }

        public void ClearStorage()
        {
            myStorage.Clear();
            myMoneyStorage = 0;
            handProduct = null;
        }
    }
}