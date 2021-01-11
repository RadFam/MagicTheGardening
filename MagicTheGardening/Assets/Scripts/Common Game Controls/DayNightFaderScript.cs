using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameUI;
using PlayerController;

namespace GameControllers
{
    public class DayNightFaderScript : MonoBehaviour
    {
        [SerializeField]
        float timeToFade;

		public Image fadeImage;
		float transp;
		Color imgColor;

		PlayerClickControl PCC;
		PlayerMoveControl PMC;
		ButtonEnableUIController BEUIC;
        // Use this for initialization
        void Start()
        {
			transp = 0.0f;
			PCC = FindObjectOfType<PlayerClickControl>();
			PMC = FindObjectOfType<PlayerMoveControl>();
			BEUIC = FindObjectOfType<ButtonEnableUIController>();
        }

        public void MakeDayNightFading()
		{
			transp = 0.0f;
			
			// Block Player and UI mouse clicks
			PCC.ClickOnOff();
			PMC.Cancel();
			BEUIC.BlockDeblockAllButtons();
			// FadeIn/FadeOut
			//StartCoroutine(FadeAll());
			
			// Unblock Player and UI mouse clicks
			
		}

		public IEnumerator FadeIn()
		{
			transp = 0.0f;
			
			// Block Player and UI mouse clicks
			PCC.ClickOnOff();
			PMC.Cancel();
			BEUIC.BlockDeblockAllButtons();

			while (transp < 1.0f)
			{
				transp += Time.deltaTime / timeToFade;
				imgColor = new Color(0.0f, 0.0f, 0.0f, transp);
				fadeImage.color = imgColor;
				yield return null;
			}		
		}

		public IEnumerator FadeOut()
		{
			while (transp > 0.0f)
			{
				transp -= Time.deltaTime / timeToFade;
				imgColor = new Color(0.0f, 0.0f, 0.0f, transp);
				fadeImage.color = imgColor;
				yield return null;
			}

			PCC.ClickOnOff();
			BEUIC.BlockDeblockAllButtons();
			transp = 0.0f;
		}

		public IEnumerator FadeAll()
		{
			yield return FadeIn();
			yield return FadeOut();

			PCC.ClickOnOff();
			BEUIC.BlockDeblockAllButtons();
			transp = 0.0f;
		}
    }
}