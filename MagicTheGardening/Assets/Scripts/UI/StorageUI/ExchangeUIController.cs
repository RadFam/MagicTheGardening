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
        public BoilerStorageUIController boilerPrefab;
        public SalesmanStorageUIController salesPrefab;

        public TradePanelUIController tradePrefab;
        public GameObject exchangePrefab;

        public AbstractStorageUIController objPrefab;
        public AbstractStorageUIController subjPrefab;

        public Button exitButton;

        public bool tradeAnswer;
        public bool tradeAnswer_wassell;
        public bool tradeAnswer_partsell;
        public bool tradeAnswer_wasprod;


        StorageScript SSc_obj;
        StorageScript SSc_subj;

        // Use this for initialization
        void Start()
        {
            cam = GameObject.Find("Main Camera").GetComponent<Camera>();
            tradeAnswer = false;
            tradeAnswer_wassell = false;
            tradeAnswer_partsell = false;
            tradeAnswer_wasprod = false;
        }

        public void OnCloseStorages()
        {
            playerPrefab.gameObject.SetActive(false);
            chestPrefab.gameObject.SetActive(false);
            boilerPrefab.gameObject.SetActive(false);
            salesPrefab.gameObject.SetActive(false);

            exchangePrefab.gameObject.SetActive(false);
        }

        public void ShowPlayerChest(ref StorageScript ssc_1, ref StorageScript ssc_2, StorageTypes stType) // player is the Subject(!)
        {
            cam = GameObject.Find("Main Camera").GetComponent<Camera>();
            exchangePrefab.gameObject.SetActive(true);

            SSc_obj = ssc_1;
            SSc_subj = ssc_2;

            objPrefab = chestPrefab;
            subjPrefab = playerPrefab;

            // Instantiate storage panels by the placement of objects
            Vector3 obj_pos = cam.WorldToScreenPoint(SSc_obj.gameObject.transform.position);
            Vector3 subj_pos = cam.WorldToScreenPoint(SSc_subj.gameObject.transform.position);

            //Debug.Log("obj_pos: " + obj_pos + "   subj_pos: " + subj_pos);

            Vector2 deltaObj = new Vector2(0.0f, 0.0f);
            Vector2 deltaSubj = new Vector2(0.0f, 0.0f);

            if (obj_pos.x <= subj_pos.x && obj_pos.y >= subj_pos.y)
            {
                deltaObj = new Vector2(0.2f, 0.8f);
                deltaSubj = new Vector2(0.7f, 0.2f);
            }
            else if (obj_pos.x > subj_pos.x && obj_pos.y >= subj_pos.y)
            {
                deltaObj = new Vector2(0.8f, 0.8f);
                deltaSubj = new Vector2(0.3f, 0.2f); // (!!!!)
            }
            else if (obj_pos.x <= subj_pos.x && obj_pos.y < subj_pos.y)
            {
                deltaObj = new Vector2(0.2f, 0.2f);
                deltaSubj = new Vector2(0.7f, 0.8f);
            }
            else if (obj_pos.x > subj_pos.x && obj_pos.y < subj_pos.y)
            {
                deltaObj = new Vector2(0.8f, 0.2f);
                deltaSubj = new Vector2(0.3f, 0.8f);
            }
            
            playerPrefab.gameObject.SetActive(true);
            playerPrefab.SetSelfScaling(deltaSubj);

            int tmp = SSc_subj.GetStorageLength();
            
            for (int i = 0; i < tmp; ++i)
            {
                playerPrefab.SetAnotherDDElement(SSc_subj.GetStorageProduct(i).productSprite, SSc_subj.GetStorageProduct(i).productName, SSc_subj.GetStorageProductVol(i));
            }

            if (stType == StorageTypes.Chest)
            {
                chestPrefab.gameObject.SetActive(true);
                chestPrefab.SetSelfScaling(deltaObj);

                tmp = SSc_obj.GetStorageLength();
                for (int i = 0; i < tmp; ++i)
                {
                    chestPrefab.SetAnotherDDElement(SSc_obj.GetStorageProduct(i).productSprite, SSc_obj.GetStorageProduct(i).productName, SSc_obj.GetStorageProductVol(i));
                }
            }

            if (stType == StorageTypes.Boiler)
            {
                boilerPrefab.gameObject.SetActive(true);
                boilerPrefab.SetSelfScaling(deltaObj);

                tmp = SSc_obj.GetStorageLength();
                for (int i = 0; i < tmp; ++i)
                {
                    boilerPrefab.SetAnotherDDElement(SSc_obj.GetStorageProduct(i).productSprite, SSc_obj.GetStorageProduct(i).productName, SSc_obj.GetStorageProductVol(i));
                }
            }
            
        }

        public void ShowPlayerBoiler()
        {
            // May be this is not nessesary function
        }

        public void ShowPlayerSales(ref StorageScript ssc_1, ref StorageScript ssc_2, StorageTypes stType)
        {
            // Do not need
            cam = GameObject.Find("Main Camera").GetComponent<Camera>();

            //Debug.Log("Start ShowPlayerSales");
            exchangePrefab.gameObject.SetActive(true);

            SSc_obj = ssc_1;
            SSc_subj = ssc_2;

            //salesPrefab = GameObject.Find("SalesStoragePanel").GetComponent<SalesmanStorageUIController>();
            //playerPrefab =  GameObject.Find("PlayerStoragePanel").GetComponent<PlayerStorageUIController>();

            objPrefab = salesPrefab; // Make the SalesUI script
            subjPrefab = playerPrefab;

            Vector3 obj_pos = cam.WorldToScreenPoint(SSc_obj.gameObject.transform.position);
            Vector3 subj_pos = cam.WorldToScreenPoint(SSc_subj.gameObject.transform.position);

            Vector2 deltaObj = new Vector2(0.0f, 0.0f);
            Vector2 deltaSubj = new Vector2(0.0f, 0.0f);

            if (obj_pos.x <= subj_pos.x && obj_pos.y >= subj_pos.y)
            {
                deltaObj = new Vector2(0.2f, 0.8f);
                deltaSubj = new Vector2(0.7f, 0.2f);
            }
            else if (obj_pos.x > subj_pos.x && obj_pos.y >= subj_pos.y)
            {
                deltaObj = new Vector2(0.8f, 0.8f);
                deltaSubj = new Vector2(0.3f, 0.2f); // (!!!!)
            }
            else if (obj_pos.x <= subj_pos.x && obj_pos.y < subj_pos.y)
            {
                deltaObj = new Vector2(0.2f, 0.2f);
                deltaSubj = new Vector2(0.7f, 0.8f);
            }
            else if (obj_pos.x > subj_pos.x && obj_pos.y < subj_pos.y)
            {
                deltaObj = new Vector2(0.8f, 0.2f);
                deltaSubj = new Vector2(0.3f, 0.8f);
            }
            
            playerPrefab.gameObject.SetActive(true);
            playerPrefab.SetSelfScaling(deltaSubj);

            int tmp = SSc_subj.GetStorageLength();
            
            for (int i = 0; i < tmp; ++i)
            {
                playerPrefab.SetAnotherDDElement(SSc_subj.GetStorageProduct(i).productSprite, SSc_subj.GetStorageProduct(i).productName, SSc_subj.GetStorageProductVol(i));
            }

            if (stType == StorageTypes.Salesman)
            {
                salesPrefab.gameObject.SetActive(true);
                salesPrefab.SetSelfScaling(deltaObj);

                tmp = SSc_obj.GetStorageLength();
                for (int i = 0; i < tmp; ++i)
                {
                    salesPrefab.SetAnotherDDElement(SSc_obj.GetStorageProduct(i).productSprite, SSc_obj.GetStorageProduct(i).productName, SSc_obj.GetStorageProductVol(i));
                }
            }
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
                SSc_subj.AddProduct(product, prNum);
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
                SSc_obj.AddProduct(product, prNum);
            }

            return res;
        }

        public void TradeStorages(string product, int fromSt, int toSt) // also change money
        {
            int maxGold = 0;
            int maxVol = 0;
            int costGold = 0;

            if (fromSt == 1 && toSt == 2)
            {
                maxGold = SSc_subj.GetMoneyVol();
                maxVol = SSc_obj.HasProduct(product);
            }
            if (fromSt == 2 && toSt == 1)
            {
                maxGold = SSc_obj.GetMoneyVol();
                maxVol = SSc_subj.HasProduct(product);
            }
            costGold = GlobalGameData.instance.GetProductByName(product).cost;

            //Debug.Log("maxGold, maxVol, costGold, fromSt, toSt, product : " + maxGold.ToString() + " " + maxVol.ToString() + " " + costGold.ToString() + " " + fromSt.ToString() + " " + toSt.ToString() + " " + product);

            tradePrefab.gameObject.SetActive(true);
            tradePrefab.SetInitParams(maxGold, maxVol, costGold, fromSt, toSt, product);
        }

        public void TradeProceedStorages(string product, int changeVol, int moneyVol, int fromSt, int toSt)
        {
            //Debug.Log("product, changeVol, moneyVol, fromSt, toSt : " + product + " " + changeVol.ToString() + " " + moneyVol.ToString() + " " + fromSt.ToString() + " " + toSt.ToString() + " ");

            tradeAnswer_wassell = true;
            tradeAnswer_partsell = false;
            tradeAnswer_wasprod = false;

            if (changeVol == 0)
            {
                tradeAnswer_wassell = false;
            }
            if (changeVol > 0 && fromSt == 1 && toSt == 2)
            {
                //SSc_obj.RemoveProduct(product, changeVol);
                //SSc_subj.AddProduct(product, changeVol);
                if (SSc_subj.HasProduct(product) > 0)
                {
                    tradeAnswer_wasprod = true;
                }
                SSc_obj.SellProduct(product, changeVol);
                SSc_subj.BuyProduct(product, changeVol);
                if (SSc_obj.HasProduct(product) > 0)
                {
                    tradeAnswer_partsell = true;
                }
            }
            if (changeVol > 0 && fromSt == 2 && toSt == 1)
            {
                //SSc_subj.RemoveProduct(product, changeVol);
                //SSc_obj.AddProduct(product, changeVol);
                if (SSc_obj.HasProduct(product) > 0)
                {
                    tradeAnswer_wasprod = true;
                }
                SSc_subj.SellProduct(product, changeVol);
                SSc_obj.AddProduct(product, changeVol);
                if (SSc_subj.HasProduct(product) > 0)
                {
                    tradeAnswer_partsell = true;
                }
            }

            tradeAnswer = true;
        }
    }
}