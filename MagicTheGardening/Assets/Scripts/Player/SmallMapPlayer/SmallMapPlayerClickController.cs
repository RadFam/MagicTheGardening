using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerController
{
    public class SmallMapPlayerClickController : MonoBehaviour
    {
		public Camera myCam;
		RaycastHit rayHit;

        // Use this for initialization
        void Start()
        {
			myCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        }

        // Update is called once per frame
        void Update()
        {
			CheckForClick();
        }

		void CheckForClick()
		{
			if (Input.GetMouseButton(0))
			{
				if (Physics.Raycast(GetMouseRay(), out rayHit))
				{
					GetComponent<SmallMapPlayerMoveController>().MoveToAction(rayHit.point);
				}
			}
		}

		Ray GetMouseRay()
		{
			return myCam.ScreenPointToRay(Input.mousePosition);
		}
    }
}