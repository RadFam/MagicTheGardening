using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerController;
using GameControllers;

namespace GameElement
{
    public class BigLocationsPortalScript : MonoBehaviour
    {
		Collider myCollider;
        bool playerClose;
		bool playerIn;
        // Use this for initialization
        void Start()
        {
			myCollider = GetComponent<Collider>();
            playerClose = false;
			playerIn = false;
        }

        void OnTriggerEnter(Collider obj)
        {
            if (!playerClose && obj.gameObject.tag == "Player")
            {
                playerClose = true;
            }
        }

        void OnTriggerExit(Collider obj)
        {
			playerClose = false;
			playerIn = false;
        }

		void OnTriggerStay(Collider obj)
		{
			//Debug.Log("Something in area");
			if (playerClose && obj.gameObject.tag == "Player")
			{
				//Debug.Log("Player in area");
				if (!playerIn && myCollider.bounds.Contains(obj.transform.position))
				{
					//Debug.Log("Player can portalize");
					// Set signal to controller, that player has reached city
					playerIn = true;
					Portalize();

				}
				if (!myCollider.bounds.Contains(obj.transform.position))
				{
					playerIn = false;
				}
			}
		}

		void Portalize()
		{
			PlayerMoveControl PMC = FindObjectOfType<PlayerMoveControl>();
			PMC.Cancel();

			StartCoroutine(PortalCoroutine());
		}

		public IEnumerator PortalCoroutine()
		{
			yield return new WaitForSeconds(0.4f);
			SceneLoaderScript.instance.LoadScene(1);
		}
    }
}