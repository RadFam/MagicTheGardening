using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Product", menuName = "ScriptableObjects/Products/Product_Type_3", order = 3)]
public class Product_TypeThree : ProductCommon 
{
    public int daysOfCook;
    public List<ProductCommon> craftProducts;
    public List<int> craftProductsVol;
}
