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
        public GameObject exchangePrefab;

        public AbstractStorageUIController objPrefab;
        public AbstractStorageUIController subjPrefab;

        public Button exitButton;

        StorageScript SSc_obj;
        StorageScript SSc_subj;

        GameObject rectObj;
        GameObject rectSubj;

        public GameObject RectObj
        {
            get { return rectObj; }
            set { rectObj = value; }
        }
        public GameObject RectSubj
        {
            get { return rectSubj; }
            set { rectSubj = value; }
        }

        // Use this for initialization
        void Start()
        {
            cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        }

        public void OnCloseStorages()
        {
            playerPrefab.gameObject.SetActive(false);
            chestPrefab.gameObject.SetActive(false);
            //salesPrefab.gameObject.SetActive(false);

            exchangePrefab.gameObject.SetActive(false);
        }

        public void ShowPlayerChest(ref StorageScript ssc_1, ref StorageScript ssc_2) // player is the Subject(!)
        {
            exchangePrefab.gameObject.SetActive(true);

            SSc_obj = ssc_1;
            SSc_subj = ssc_2;

            objPrefab = chestPrefab;
            subjPrefab = playerPrefab;

            //Debug.Log("Camera: " + cam + "  Object: " + SSc_obj + "  Subject: " + SSc_subj);

            // Instantiate storage panels by the placement of objects
            Vector3 obj_pos = cam.WorldToScreenPoint(SSc_obj.gameObject.transform.position);
            Vector3 subj_pos = cam.WorldToScreenPoint(SSc_subj.gameObject.transform.position);

            //Debug.Log("obj_pos: " + obj_pos + "   subj_pos: " + subj_pos);

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

            //Debug.Log("deltaObj: " + deltaObj + "   deltaSubj: " + deltaSubj);

            playerPrefab.gameObject.SetActive(true);
            playerPrefab.SetSelfScaling(deltaSubj);

            int tmp = SSc_subj.GetStorageLength();
            //Debug.Log("SSc_subj count: " + tmp.ToString());
            for (int i = 0; i < tmp; ++i)
            {
                playerPrefab.SetAnotherDDElement(SSc_subj.GetStorageProduct(i).productSprite, SSc_subj.GetStorageProduct(i).productName, SSc_subj.GetStorageProductVol(i));
            }


            chestPrefab.gameObject.SetActive(true);
            chestPrefab.SetSelfScaling(deltaObj);

            tmp = SSc_obj.GetStorageLength();
            for (int i = 0; i < tmp; ++i)
            {
                chestPrefab.SetAnotherDDElement(SSc_obj.GetStorageProduct(i).productSprite, SSc_obj.GetStorageProduct(i).productName, SSc_obj.GetStorageProductVol(i));
            }

            rectSubj = playerPrefab.gameObject;
            rectObj = chestPrefab.gameObject;
        }

        public void ShowPlayerSales()
        {
        
        }

        public bool ExchangeStorages(string product, int fromSt, int toSt) // 1 - object menu, 2 - subject menu
        {
            bool res = false;

            if (fromSt == 1 && toSt == 2)
            {
                int prNum = SSc_obj.HasProduct(product);
                SSc_obj.RemoveProduct(product, prNum);
                int prNumA = SSc_subj.HasProduct(product);
                if (prNumA > 0)
                {
                    res = true;
                }
                SSc_subj.RemoveProduct(product, prNum);
            }
            if (fromSt == 2 && toSt == 1)
            {
                int prNum = SSc_subj.HasProduct(product);
                SSc_subj.RemoveProduct(product, prNum);
                int prNumA = SSc_obj.HasProduct(product);
                if (prNumA > 0)
                {
                    res = true;
                }
                SSc_obj.RemoveProduct(product, prNum);
            }

            return res;
        }

        public void PutObjectIntoStash(int SObj, int dndStorageNum) // 1 - to subject menu, 2 - to object menu
        {
            // Here we suppose, that we put object from from one stash to another
            if (SObj == 1)
            {
                // Get item from object storage and put it into subject storage
                string prName = SSc_obj.GetStorageProduct(dndStorageNum).productName;
                int prNum = SSc_obj.GetStorageProductVol(dndStorageNum);
                SSc_obj.RemoveProduct(prName, prNum);
                SSc_subj.AddProduct(prName, prNum);
            }
            else
            {
                // Get item from subject storage and put it into object storage
                string prName = SSc_subj.GetStorageProduct(dndStorageNum).productName;
                int prNum = SSc_subj.GetStorageProductVol(dndStorageNum);
                SSc_subj.RemoveProduct(prName, prNum);
                SSc_obj.AddProduct(prName, prNum);
            }
            
            subjPrefab.RearrangeDDelements();
            objPrefab.RearrangeDDelements();
        }
    }
}