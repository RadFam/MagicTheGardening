using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameElement;

namespace GameControllers
{
    public class InterSceneStorage : MonoBehaviour
    {
		public struct Storage
		{
			public string storUnit;
			public int storCount;

			public Storage(string pc, int val)
            {
                this.storUnit = pc;
                this.storCount = val;
            }
		}

		List<Storage> playerStorage;
        // Use this for initialization
        void Start()
        {
			playerStorage = new List<Storage>();
			playerStorage.Add(new Storage("Money", 50));
        }

		public void SaveStorage (StorageScript ssc)
		{
			playerStorage.Clear();
			
			// Add money
			Storage strg = new Storage("Money", ssc.GetMoneyVol());
			playerStorage.Add(strg);

			for (int i = 0; i < ssc.GetStorageLength(); ++i)
			{
				playerStorage.Add(new Storage(ssc.GetStorageProduct(i).productName, ssc.GetStorageProductVol(i)));
			} 
		}

		public void LoadStorage(StorageScript ssc)
		{
			ssc.ClearStorage();

			// By default, money in the first List element
			ssc.SetMoneyVol(playerStorage[0].storCount);

			for (int i = 1; i < playerStorage.Count; ++i)
			{
				ssc.AddProduct(playerStorage[i].storUnit, playerStorage[i].storCount);
			}
		}
        
    }
}