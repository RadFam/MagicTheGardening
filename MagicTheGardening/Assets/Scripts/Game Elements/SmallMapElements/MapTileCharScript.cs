using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerController;

namespace SmallMap
{
    public class MapTileCharScript : MonoBehaviour
    {
        [SerializeField]
        MapTileCharacteristics mtCharacters;
        Collider myCollider;

        bool hasPlayerTouched;
        bool hasPlayerContain;

        // Use this for initialization
        void Start()
        {
            myCollider = GetComponent<Collider>();
            hasPlayerContain = false;
            hasPlayerTouched = false;
        }

        // Update is called once per frame
        void OnTriggerEnter(Collider obj)
        {            
            if (!hasPlayerTouched)
            {                
                if (obj.gameObject.tag == "Player")
                {
                    hasPlayerTouched = true;
                }
            }
        }

        void OnTriggerStay(Collider obj)
        {
            if (hasPlayerTouched)
            {
                if (myCollider.bounds.Contains(obj.transform.position))
                {
                    if (!hasPlayerContain)
                    {
                        // Send data about this tile to the player controller
                        SendPlayerMovementData();
                    }
                    hasPlayerContain = true;
                }
                else
                {
                    hasPlayerContain = false;
                }
            }
        }

        void OnTriggerExit(Collider obj)
        {
            if (hasPlayerTouched)
            { 
                if (obj.gameObject.tag == "Player")
                {
                    hasPlayerTouched = false;
                    hasPlayerContain = false;
                }
            }
        }

        void SendPlayerMovementData()
        {
            GameObject player = GameObject.Find("Player");
            player.GetComponent<SmallMapPlayerMoveController>().SetMovingSpeed(mtCharacters.GetWalkingSpeed);
        }
    }
}