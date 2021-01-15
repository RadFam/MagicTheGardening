using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class TradePanelUIController : MonoBehaviour
    {
		public Text volText;
		public Text fullCostText;
		public Text oneCostText;

		public Slider mySlide;

		int costOne;
		int costFull;
		int volFull;
		int maxVol; // How much is in the 
		int maxCost;

		int fromSt;
		int toSt;
		string prodName;
        // Use this for initialization
        void Start()
        {

        }

		public void OnEnable()
		{
			costOne = 0;
			costFull = 0;
			volFull = 0;
			maxVol = 0;
			maxCost = 0;

			ShowParams();
		}

		public void SetInitParams(int maxGold, int maxVol, int costGold, int frSt, int tSt, string product)
		{
			costOne = costGold;
			mySlide.minValue = 0;
			mySlide.maxValue = Mathf.Min(maxVol, maxGold/costGold);

			fromSt = frSt;
			toSt = tSt;
			prodName = product;
		}
		void ShowParams()
		{
			volFull = (int)mySlide.value;
			volText.text = volFull.ToString();
			oneCostText.text = costOne.ToString();
			costFull = volFull * costOne;
			fullCostText.text = costFull.ToString();
		}

		public void OnOkButtonClick()
		{
			FindObjectOfType<ExchangeUIController>().TradeProceedStorages(prodName, volFull, costFull, fromSt, toSt);
			gameObject.SetActive(false);
		}
    }
}