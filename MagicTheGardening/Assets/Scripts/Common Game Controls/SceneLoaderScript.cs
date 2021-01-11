using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace GameControllers
{
    public class SceneLoaderScript : MonoBehaviour
    {
		public static SceneLoaderScript instance;
		private int sceneToLoad;
        List<string> sceneNames = new List<string>{"FarmOneLocation", "SmallWorldMap", "PlainCityOne", "PlainCityTwo"};
		
		[SerializeField]
		Image fadeImage;

		[SerializeField]
		float fadeInterval;
		[SerializeField]
		float fadeAlpha;
		
		// Use this for initialization
        void Start()
        {
			if (instance == null)
			{
				instance = this;
			}
			else
			{
				Destroy(this.gameObject);
			}
			sceneToLoad = 0;
			fadeAlpha = 0.0f;
        }

		public void LoadScene(int vol)
		{
			sceneToLoad = vol;
			StartCoroutine(LoadGameScene());
		}
		IEnumerator LoadGameScene()
		{
			yield return StartCoroutine(FadeIn());

			AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneNames[sceneToLoad], LoadSceneMode.Single);
			while (!asyncLoad.isDone)
			{
				yield return null;
			}

			yield return StartCoroutine(FadeOut());
		}

		IEnumerator FadeIn()
		{
			fadeAlpha = 0.0f;

			while (fadeAlpha < 1.0f)
			{
				fadeAlpha += Time.deltaTime / fadeInterval;
				fadeImage.color = new Color(0.0f, 0.0f, 0.0f, fadeAlpha);
				yield return null;
			}		

			fadeAlpha = 1.0f;
		}

		IEnumerator FadeOut()
		{
			fadeAlpha = 1.0f;

			while (fadeAlpha > 0.0f)
			{
				fadeAlpha -= Time.deltaTime / fadeInterval;
				fadeImage.color = new Color(0.0f, 0.0f, 0.0f, fadeAlpha);
				yield return null;
			}		

			fadeAlpha = 0.0f;
		}
    }
}