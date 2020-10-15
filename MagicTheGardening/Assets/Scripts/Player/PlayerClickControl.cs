using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using GameControllers;

namespace PlayerController
{
    public class PlayerClickControl : MonoBehaviour
    {

        public Camera myCam;

        // Use this for initialization
        void Start()
        {
            myCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        }

        // Update is called once per frame
        void Update()
        {
            if (ReactWithInteraction())
            {
                return;
            }

            if (ReactWithMovement())
            {
                return;
            }
        }

        private bool ReactWithInteraction()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                Interactible inter = hit.transform.GetComponent<Interactible>();
                if (inter == null) continue;

                if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
                {
                    Debug.Log("Was click to move and interact");
                    GetComponent<PlayerInteractController>().Interact(inter);
                }
                return true;
            }
            
            return false;
        }

        private bool ReactWithMovement()
        {
            return MoveToCursor();
        }

        private bool MoveToCursor()
        {
            RaycastHit hit;
            if (Physics.Raycast(GetMouseRay(), out hit))
            {
                if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
                {
                    GetComponent<PlayerMoveControl>().StartMoveAction(hit.point);
                }
                return true;
            }

            return false;
        }

        private Ray GetMouseRay()
        {
            //return Camera.main.ScreenPointToRay(Input.mousePosition);
            return myCam.ScreenPointToRay(Input.mousePosition);
        }
    }
}