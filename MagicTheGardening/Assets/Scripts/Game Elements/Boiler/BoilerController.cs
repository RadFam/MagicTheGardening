using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GameControllers;

namespace GameElement
{
    public class BoilerController : MonoBehaviour, ISaveable
    {

        bool isBoiling;
        int daysOfBoiling;
        int daysOfCurrentCook;
        ProductCommon cookingProduct;
        int cookingProductsVal;

        Dictionary<ProductCommon, int> tmpCookingDict = new Dictionary<ProductCommon, int>();

        StorageScript SSc;

        // Use this for initialization
        void Start()
        {
            isBoiling = false;
            daysOfBoiling = 0;
            daysOfCurrentCook = 0;
            cookingProduct = null;
            cookingProductsVal = 0;

            SSc = gameObject.GetComponent<StorageScript>();
        }


        public void OnTictocMoment()
        {
            if (!isBoiling) // Make choose, can we start to cook any potion
            {
                if (cookingProduct == null)
                {
                    FindRecipeByContent(); // Get All Possible recipes
                    // Just for test, lets make the last potion from the "tmpCookingDict"

                    SSc.ClearStorage();
                    isBoiling = true;
                    daysOfCurrentCook = 0;
                    return;
                }
            }
            else
            {
                daysOfCurrentCook++;
                if (daysOfCurrentCook == daysOfBoiling)
                {
                    Debug.Log("POTION IS READY");

                    // Move cookingProduct to Storage
                    SSc.AddProduct(cookingProduct.productName, cookingProductsVal);

                    isBoiling = false;
                }
            }
        }

        void FindRecipeByContent()
        {
            Product_FullSatchel pFS = GlobalGameData.instance.allExistableProducts;
            tmpCookingDict.Clear();

            foreach (ProductCommon PC in pFS.GetAllProducts)
            {
                // If PC is Product_type_3
                if (PC is Product_TypeThree)
                {
                    Product_TypeThree PC_three = PC as Product_TypeThree;
                    List<ProductCommon> recipeNeed = PC_three.craftProducts;
                    List<int> recipeNeedVol = PC_three.craftProductsVol;

                    bool firstRes = true;
                    bool secRes = true;
                    int tmpVol = 0;
                    int tmpFinVol = 100000;

                    for (int i = 0; i < recipeNeed.Count; ++i )
                    {
                        tmpVol = SSc.HasProduct(recipeNeed[i].productName);
                        if (tmpVol < 0)
                        {
                            firstRes = false;
                        }
                    }

                    if (firstRes)
                    {
                        for (int i = 0; i < recipeNeedVol.Count; ++i)
                        {
                            tmpVol = SSc.HasProduct(recipeNeed[i].productName);
                            if (tmpVol < recipeNeedVol[i])
                            {
                                secRes = false;
                            }
                            else
                            {
                                if ((tmpVol / recipeNeedVol[i]) < tmpFinVol)
                                {
                                    tmpFinVol = tmpVol / recipeNeedVol[i];
                                }
                            }
                        }
                    }

                    if (firstRes && secRes)
                    {
                        tmpCookingDict.Add(PC, tmpFinVol);
                        // TEMPORARY PATCH (!!!)
                        cookingProduct = PC;
                        cookingProductsVal = tmpFinVol;
                        daysOfBoiling = ((Product_TypeThree)PC).daysOfCook;
                    }
                }
            }
        }

        public void TakeReadyPotion(ref StorageScript SScc)
        {
            if (cookingProduct != null)
            {
                SScc.AddProduct(cookingProduct.productName, cookingProductsVal);

                cookingProduct = null;
                cookingProductsVal = 0;
                SSc.ClearStorage();
            }
        }

        public void PutIngredient(string prodName, int vol)
        {
            SSc.AddProduct(prodName, vol);
        }

        // Define functions later
        public object CaptureObject()
        {
            /*
                isBoiling = false;
                daysOfBoiling = 0;
                daysOfCurrentCook = 0;
                cookingProduct = null;
                cookingProductsVal = 0;
             */

            Dictionary<string, int> myActualBoilerContent = new Dictionary<string, int>();
            if (isBoiling)
            {
                myActualBoilerContent.Add("isBoiling", 1);
                myActualBoilerContent.Add("daysOfBoiling", daysOfBoiling);
                myActualBoilerContent.Add("daysOfCurrentCook", daysOfCurrentCook);
                myActualBoilerContent.Add(cookingProduct.productName, cookingProductsVal);
            }
            else
            {
                myActualBoilerContent.Add("isBoiling", 0);
            }
            
            //.........
            return myActualBoilerContent;
        }

        public void RestoreObject(object obj)
        {
            Dictionary<string, int> myActualBoilerContent = obj as Dictionary<string, int>;
            if (myActualBoilerContent["isBoiling"] == 0)
            {
                isBoiling = false;
                daysOfBoiling = 0;
                daysOfCurrentCook = 0;
                cookingProduct = null;
                cookingProductsVal = 0;
            }
            if (myActualBoilerContent["isBoiling"] == 1)
            {
                isBoiling = true;
                daysOfBoiling = myActualBoilerContent["daysOfBoiling"];
                daysOfCurrentCook = myActualBoilerContent["daysOfCurrentCook"];
                
                foreach (KeyValuePair<string, int> keyValue in myActualBoilerContent)
                {
                    if (keyValue.Key != "daysOfBoiling" && keyValue.Key != "daysOfCurrentCook" && keyValue.Key != "isBoiling")
                    {
                        cookingProductsVal = keyValue.Value;
                        Product_FullSatchel pFS = GlobalGameData.instance.allExistableProducts;
                        foreach (ProductCommon PC in pFS.GetAllProducts)
                        {
                            if (PC.productName == keyValue.Key)
                            {
                                cookingProduct = PC;
                                break;
                            }
                        }
                        break;
                    }
                }

            }

            return;
        }
    }

}