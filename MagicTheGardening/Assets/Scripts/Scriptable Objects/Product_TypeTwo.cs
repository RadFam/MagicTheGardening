using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Product", menuName = "ScriptableObjects/Products/Product_Type_2", order = 2)]
public class Product_TypeTwo : ProductCommon 
{
    public PlantGrowthCycle plantToGrow;	
}
