using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class ButtonEnableUIController : MonoBehaviour
    {

		List<Button> allButtions = new List<Button>();
        // Use this for initialization
        void Start()
        {

        }

		void RefreshButtons()
		{
			allButtions.Clear();

		}

        public void BlockDeblockAllButtons()
		{
			var allButtons = FindObjectsOfType<Button>();
			Button button;
			for (int i = 0; i < allButtions.Count; ++i)
			{
				button = (Button)allButtons[i];
				button.interactable = !button.interactable;
			}
		}
    }
}