using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameControllers;
using GameElement;

namespace PlayerController
{
    public class TmpPlayerFullControl : MonoBehaviour
    {
        [SerializeField]
        TimerTictocEvent TttE;

        PlayerActionControl PAC;
        StorageScript SSc;


        int dayCnt;

        // Use this for initialization
        void Start()
        {
            PAC = gameObject.GetComponent<PlayerActionControl>();
            SSc = gameObject.GetComponent<StorageScript>();

            dayCnt = 0;

            // Organize initial player`s storage
            // .... some time later. We can make initial player storage just in player`s StorageScript(!!!!)
        }

        public void SetPlowAction()
        {
            PAC.GetAction = Actions.Plow;
            Debug.Log("Current PLOW action");
        }

        public void SetWaterAction()
        {
            PAC.GetAction = Actions.Water;
            Debug.Log("Current WATER action");
        }

        public void SetSeedSimpleAction()
        {
            // Put seeds in hand
            // ........
            SSc.FromStorageToHand("RedFlowerSeeds");

            PAC.GetAction = Actions.PutSeeds;
            Debug.Log("Current PUTSEEDS action");
        }

        public void SetFertilizeSimpleAction() // Not now for use - We nave no fertilize SO object
        {
            // Put fertilizer in hand
            // ........
            PAC.GetAction = Actions.PutFertilizer;
            Debug.Log("Current PUTFERTILIZER action");
        }

        public void GetPlantProductsAction()
        {
            PAC.GetAction = Actions.NoAction;
            Debug.Log("Current NOACTION action");
        }

        public void SetClearAction()
        {
            PAC.ClearAction();
            Debug.Log("Current NOACTION action");
        }

        public void SkipDay()
        {
            TttE.Raise();
            dayCnt++;
            Debug.Log("NEW DAY COMES: " + dayCnt.ToString());
        }

        public void SaveAllData()
        {
            Debug.Log("DATA WAS SAVED");
        }

        public void LoadAllData()
        {
            Debug.Log("DATA WAS LOADED");
        }
    }
}