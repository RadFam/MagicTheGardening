using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerController;

namespace SmallMap
{
    public class CityPassScript : PortalMainScript
    {
		[SerializeField]
		string cityName;

		[SerializeField]
		int cityNum;
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
			if (playerClose && obj.gameObject.tag == "Player")
			{
				if (!playerIn && myCollider.bounds.Contains(obj.transform.position))
				{
					// Set signal to controller, that player has reached city
					Debug.Log("City " + cityName + " is reached");
					playerIn = true;
					SmallMapPlayerEventController SMPEC = FindObjectOfType<SmallMapPlayerEventController>();
					SMPEC.SetCityLocation(cityNum, myDestinationTag);

				}
				if (!myCollider.bounds.Contains(obj.transform.position))
				{
					playerIn = false;
				}
			}
		}
    }
}