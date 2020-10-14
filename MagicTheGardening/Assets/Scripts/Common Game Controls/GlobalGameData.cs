using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameControllers
{
    public class GlobalGameData : MonoBehaviour
    {
        public Product_FullSatchel allExistableProducts;
        public static GlobalGameData instance = null;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        // Use this for initialization
        void Start()
        {
            DontDestroyOnLoad(this);
        }

        public void SelfInitiate()
        {
            allExistableProducts = Resources.Load<Product_FullSatchel>("ScriptObjects/Product_All");
        }

        public ProductCommon GetProductByName(string name)
        {
            return allExistableProducts.GetProductByName(name);
        }
    }
}