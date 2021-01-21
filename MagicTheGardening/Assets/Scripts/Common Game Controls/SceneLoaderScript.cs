using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GameElement;
using GameControllers;

namespace GameControllers
{
    public class SceneLoaderScript : MonoBehaviour
    {
        public static SceneLoaderScript instance;
        private int sceneToLoad;
        List<string> sceneNames = new List<string> { "FarmOneLocation", "SmallWorldMap", "CityOneLocation", "CityTwoLocation" };

        [SerializeField]
        Image fadeImage;

        [SerializeField]
        float fadeInterval;
        [SerializeField]
        float fadeAlpha;
        public string currentSceneName;

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
            currentSceneName = SceneManager.GetActiveScene().name;
        }

        public void LoadScene(int vol, PortalNames destPortal)
        {
            // Check, if we leave the trade/farm/battle location
            if (currentSceneName != "SmallWorldMap")
            {
                // Save player storage
                StorageScript ssc = GameObject.Find("Player").GetComponent<StorageScript>();
                ssc.IntersceneSave();
            }

            sceneToLoad = vol;
            StartCoroutine(LoadGameScene(destPortal));
        }
        IEnumerator LoadGameScene(PortalNames destPortal)
        {
            yield return StartCoroutine(FadeIn());

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneNames[sceneToLoad], LoadSceneMode.Single);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            currentSceneName = sceneNames[sceneToLoad];

			// After scene load place player in the place near the appropriate portal point
			// We need to know, what portal on scene we must use to get the position
            GameObject player = GameObject.FindWithTag("Player");

            foreach (PortalMainScript PMS in FindObjectsOfType<PortalMainScript>())
            {
                if (PMS.myPortalTag == destPortal)
                {
                    player.GetComponent<NavMeshAgent>().Warp(PMS.PlayerInitSpawn().position);
                    player.transform.rotation = PMS.PlayerInitSpawn().rotation;
                    break;
                }
            }

            // Special check for restoration players storage
            if (currentSceneName != "SmallWorldMap")
            {
                // Save player storage
                StorageScript ssc = player.GetComponent<StorageScript>();
                ssc.IntersceneRestore();
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