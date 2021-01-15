using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using GameControllers;

namespace GameUI
{
    public class StorageSlotScript : MonoBehaviour, IDropHandler
    {
        int myAccessory;

        int myPerconalNum;

        public int MyAccessory
        {
            get { return myAccessory; }
            set { myAccessory = value; }
        }

        public int MyPersonalNum
        {
            get {return myPerconalNum;}
            set {myPerconalNum = value;}
        }

        public GameObject MyChild
        {
            get
            {

                if (gameObject.transform.childCount > 0)
                {
                    return gameObject.transform.GetChild(0).gameObject;
                }
                /*
                if (DragScript.dragItem.GetComponent<DragScript>().myOwnerCode == myAccessory)
                {
                    return null;
                }
                */

                return null;
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            //Debug.Log("Drop on me: " + gameObject.name);
            //Debug.Log("MyChild: " + MyChild);
            if (!MyChild)
            {
                bool check = false;
                string scName = FindObjectOfType<SceneLoaderScript>().currentSceneName;
                //Debug.Log("Screen name: " + scName);
                if (scName == "FarmOneLocation")
                {
                    check = FindObjectOfType<ExchangeUIController>().ExchangeStorages(DragScript.dragItem.GetComponent<DragScript>().myProductName, DragScript.dragItem.GetComponent<DragScript>().myOwnerCode, myAccessory);

                    if (!check)
                    {
                        DragScript.dragItem.transform.SetParent(gameObject.transform);
                        DragScript.dragItem.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                        DragScript.dragItem.GetComponent<DragScript>().myOwnerCode = myAccessory;
                        //eventData.pointerEnter.GetComponent<DragScript>().myOwnerCode = myAccessory;
                    }
                    else
                    {
                        // Destroy DragScript object
                        Destroy(DragScript.dragItem);
                    }
                }
                if (scName == "CityOneLocation" || scName == "CityTwoLocation")
                {
                    //Debug.Log("Product name: " + DragScript.dragItem.GetComponent<DragScript>().myProductName);
                    if (DragScript.dragItem.GetComponent<DragScript>().myOwnerCode != myAccessory)
                    {
                        FindObjectOfType<ExchangeUIController>().TradeStorages(DragScript.dragItem.GetComponent<DragScript>().myProductName, DragScript.dragItem.GetComponent<DragScript>().myOwnerCode, myAccessory);
                        StartCoroutine(WaitForExUIAnswer());
                    }
                    else
                    {
                        DragScript.dragItem.transform.SetParent(gameObject.transform);
                        DragScript.dragItem.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                        DragScript.dragItem.GetComponent<DragScript>().myOwnerCode = myAccessory;
                    }  
                }

            }
            else // Занято, вернуть DragScript.dragItem в исходное положение
            {

            }
        }

        IEnumerator WaitForExUIAnswer()
        {
            while (!FindObjectOfType<ExchangeUIController>().tradeAnswer)
            {
                yield return new WaitForSeconds(0.05f);
            }

            FindObjectOfType<ExchangeUIController>().tradeAnswer = false;

            transform.parent.GetComponent<AbstractStorageUIController>().CreateOnceMoreDDElement(myPerconalNum, DragScript.dragItem.GetComponent<DragScript>().GetSprite(), DragScript.dragItem.GetComponent<DragScript>().myProductName);
            Debug.Log("Tried to instantiate object");

            
            if (!FindObjectOfType<ExchangeUIController>().tradeAnswer_wasprod) // HAS NOT already such product
            {
                // Case of full purchase
                if (!FindObjectOfType<ExchangeUIController>().tradeAnswer_partsell && FindObjectOfType<ExchangeUIController>().tradeAnswer_wassell)
                {
                    DragScript.dragItem.transform.SetParent(gameObject.transform);
                    DragScript.dragItem.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                    DragScript.dragItem.GetComponent<DragScript>().myOwnerCode = myAccessory;
                }

                // Case of partial purchase
                if (FindObjectOfType<ExchangeUIController>().tradeAnswer_partsell && FindObjectOfType<ExchangeUIController>().tradeAnswer_wassell)
                {
                    // Make additional DragScript
                    transform.parent.GetComponent<AbstractStorageUIController>().CreateOnceMoreDDElement(myPerconalNum, DragScript.dragItem.GetComponent<DragScript>().GetSprite(), DragScript.dragItem.GetComponent<DragScript>().myProductName);
                }


                // Case of non purchase
                if (!FindObjectOfType<ExchangeUIController>().tradeAnswer_wassell)
                {
                    // Do nothing
                }
            }
            else // HAS already such product
            {
                // Case of full purchase
                if (!FindObjectOfType<ExchangeUIController>().tradeAnswer_partsell && FindObjectOfType<ExchangeUIController>().tradeAnswer_wassell)
                {
                    Destroy(DragScript.dragItem);
                }


                // Case of partial purchase
                if (FindObjectOfType<ExchangeUIController>().tradeAnswer_partsell && FindObjectOfType<ExchangeUIController>().tradeAnswer_wassell)
                {
                    // Do nothing
                    //transform.parent.GetComponent<AbstractStorageUIController>().CreateOnceMoreDDElement(myPerconalNum, DragScript.dragItem.GetComponent<DragScript>().GetSprite(), DragScript.dragItem.GetComponent<DragScript>().myProductName);
                }


                // Case of non purchase
                if (!FindObjectOfType<ExchangeUIController>().tradeAnswer_wassell)
                {
                    // Do nothing
                }
            }
            


            /*
            if (FindObjectOfType<ExchangeUIController>().tradeAnswer_2)
            {
                DragScript.dragItem.transform.SetParent(gameObject.transform);
                DragScript.dragItem.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            }
            else
            {
                // Destroy DragScript object
                Destroy(DragScript.dragItem);
            }
            */

        }
    }
}
