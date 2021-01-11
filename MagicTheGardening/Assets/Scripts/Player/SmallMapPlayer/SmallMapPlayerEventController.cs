using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameControllers;

namespace PlayerController
{
    public class SmallMapPlayerEventController : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        public void SetCityLocation(int locationNum)
		{
			SmallMapPlayerMoveController SMPMC = GetComponent<SmallMapPlayerMoveController>();
			SMPMC.StopMoveAction();

			StartCoroutine(EnterCityCoroutine(locationNum));
		}

		public IEnumerator EnterCityCoroutine(int loc)
		{
			yield return new WaitForSeconds(0.4f);
			SceneLoaderScript.instance.LoadScene(loc);
		}
    }
}