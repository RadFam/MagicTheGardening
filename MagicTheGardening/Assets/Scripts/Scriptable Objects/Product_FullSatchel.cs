using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Product_All", menuName = "ScriptableObjects/Products/Product_All", order = 4)]
public class Product_FullSatchel : ScriptableObject {

    [SerializeField]
    List<ProductCommon> allProducts;

    public List<ProductCommon> GetAllProducts
    {
        get { return allProducts; }
    }

    public ProductCommon GetProductByName(string name)
    {
        int ind = GetAllProducts.FindIndex(x => x.productName == name);
        if (ind != -1)
        {
            return allProducts[ind];
        }

        return null;
    }

    public ProductCommon GetProductByGlobalInd(int ind)
    {
        if (ind < allProducts.Count)
        {
            return allProducts[ind];
        }
        return null;
    }
}
