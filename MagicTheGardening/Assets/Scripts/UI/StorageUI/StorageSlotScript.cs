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

        public int MyAccessory
        {
            get { return myAccessory; }
            set { myAccessory = value; }
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
            if (!MyChild)
            {
                bool check = false;
                string scName = FindObjectOfType<SceneLoaderScript>().currentSceneName;
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
                    FindObjectOfType<ExchangeUIController>().ExchangeStorages(DragScript.dragItem.GetComponent<DragScript>().myProductName, DragScript.dragItem.GetComponent<DragScript>().myOwnerCode, myAccessory);
                    StartCoroutine(WaitForExUIAnswer());
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

            if (!FindObjectOfType<ExchangeUIController>().tradeAnswer_2)
            {
                DragScript.dragItem.transform.SetParent(gameObject.transform);
                DragScript.dragItem.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            }
            else
            {
                // Destroy DragScript object
                Destroy(DragScript.dragItem);
            }
        }
    }
}
