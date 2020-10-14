﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameControllers;

namespace PlayerController
{
    public class PlayerInteractController : MonoBehaviour
    {
        [SerializeField]
        float interactRange = 2.0f;

        Transform interactTarget;
        Interactible interactObject;

        PlayerActionControl PAC;

        bool isInRange = false;

        // Use this for initialization
        void Start()
        {
            PAC = gameObject.GetComponent<PlayerActionControl>();
        }

        // Update is called once per frame
        void Update()
        {
            if (interactTarget == null) return;

            if (interactTarget != null && !getIsInRange())
            {
                GetComponent<PlayerMoveControl>().MoveTo(interactTarget.position);
            }
            else
            {
                GetComponent<PlayerMoveControl>().Cancel();
                // Start interaction
                MakeIntraction();
            }
        }

        private bool getIsInRange()
        {
            return Vector3.Distance(transform.position, interactTarget.position) < interactRange;
        }

        public void Interact(Interactible inter)
        {
            interactTarget = inter.transform;
            interactObject = inter;
        }

        public void MakeIntraction()
        {
            if (interactObject != null)
            {
                // Do some interaction
                // ...................
                if (PAC.GetAction == Actions.Plow)
                {
                    interactObject.Connect(TypeOfInteraction.GroundPlow, this.gameObject);
                }

                if (PAC.GetAction == Actions.Water)
                {
                    interactObject.Connect(TypeOfInteraction.GrowndWater, this.gameObject);
                }

                if (PAC.GetAction == Actions.PutSeeds)
                {
                    interactObject.Connect(TypeOfInteraction.GroundPutProduct, this.gameObject);
                }

                //interactObject.Connect(TypeOfInteraction.GroundPlow, this.gameObject); // Вот здесь нужно сделать зависимость от того, что в данный момент активно у игрока (!!!)
                //interactObject.Connect(TypeOfInteraction.GrowndWater, this.gameObject); // Вот здесь нужно сделать зависимость от того, что в данный момент активно у игрока (!!!)
                //interactObject.Connect(TypeOfInteraction.GroundPutProduct, this.gameObject); // Вот здесь нужно сделать зависимость от того, что в данный момент активно у игрока (!!!)
                // And so on....
            }
        }

        public void Cancel()
        {
            interactTarget = null;
            interactObject = null;
        }
    }
}