using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
                if (DragScript.dragItem.GetComponent<DragScript>().myOwnerCode == myAccessory)
                {
                    return null;
                }
                if (gameObject.transform.childCount > 0)
                {
                    return gameObject.transform.GetChild(0).gameObject;
                }

                return null;
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (!MyChild)
            {
                bool check = false;
                check = FindObjectOfType<ExchangeUIController>().ExchangeStorages(DragScript.dragItem.GetComponent<DragScript>().myProductName, DragScript.dragItem.GetComponent<DragScript>().myOwnerCode, myAccessory);

                if (!check)
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
}
