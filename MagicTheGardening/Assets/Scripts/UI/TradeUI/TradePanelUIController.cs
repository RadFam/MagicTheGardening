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

		public void SetInitParams(int maxGold, int maxVol, int costGold)
		{
			costOne = costGold;
			mySlide.minValue = 0;
			mySlide.maxValue = Mathf.Min(maxVol, maxGold/costGold);
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
			
		}
    }
}