using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameElement;
using GameControllers;

namespace GameUI
{
    public class ExchangeUIController : MonoBehaviour
    {

        public Camera cam;

        public PlayerStorageUIController playerPrefab;
        public ChestStorageUIController chestPrefab;
        public SalesmanStorageUIController salesPrefab;
        public Button exitButton;

        StorageScript SSc_obj;
        StorageScript SSc_subj;

        // Use this for initialization
        void Start()
        {
            cam = Camera.main;
        }

        public void ShowPlayerChest(ref StorageScript ssc_1, ref StorageScript ssc_2) // player is the Subject(!)
        {
            SSc_obj = ssc_1;
            SSc_subj = ssc_2;

            // Instantiate storage panels by the placement of objects
            Vector3 obj_pos = cam.WorldToScreenPoint(ssc_1.gameObject.transform.position);
            Vector3 subj_pos = cam.WorldToScreenPoint(ssc_2.gameObject.transform.position);

            Vector2 deltaObj = new Vector2(0.0f, 0.0f);
            Vector2 deltaSubj = new Vector2(0.0f, 0.0f);

            if (obj_pos.x <= subj_pos.x && obj_pos.y >= subj_pos.y)
            {
                deltaObj = new Vector2(0.2f, 0.2f);
                deltaSubj = new Vector2(0.7f, 0.8f);
            }
            else if (obj_pos.x > subj_pos.x && obj_pos.y >= subj_pos.y)
            {
                deltaObj = new Vector2(0.8f, 0.2f);
                deltaSubj = new Vector2(0.3f, 0.8f);
            }
            else if (obj_pos.x <= subj_pos.x && obj_pos.y < subj_pos.y)
            {
                deltaObj = new Vector2(0.2f, 0.8f);
                deltaSubj = new Vector2(0.7f, 0.2f);
            }
            else if (obj_pos.x > subj_pos.x && obj_pos.y < subj_pos.y)
            {
                deltaObj = new Vector2(0.8f, 0.8f);
                deltaSubj = new Vector2(0.3f, 0.2f);
            }

            playerPrefab.gameObject.SetActive(true);
            playerPrefab.SetSelfScaling(deltaSubj);

            chestPrefab.gameObject.SetActive(true);
            chestPrefab.SetSelfScaling(deltaObj);
        }

        public void ShowPlayerSales()
        {

        }
    }
}