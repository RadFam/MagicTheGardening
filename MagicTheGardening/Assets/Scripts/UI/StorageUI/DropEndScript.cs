using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace GameUI
{
    public class DropEndScript : MonoBehaviour, IDropHandler
    {

        ExchangeUIController exchangeCtrl;

        // Use this for initialization
        void Start()
        {
            exchangeCtrl = FindObjectOfType<ExchangeUIController>();
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                Debug.Log("OnDrop ends");
                // Check, which of two storage rectangles is closer to eventData.pointerDrag
                float distToSubj = Vector2.Distance(eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition, exchangeCtrl.RectSubj.GetComponent<RectTransform>().anchoredPosition);
                float distToObj = Vector2.Distance(eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition, exchangeCtrl.RectObj.GetComponent<RectTransform>().anchoredPosition);

                if (distToSubj <= distToObj)
                {
                    Debug.Log("First if works");
                    // Place object to players inventory
                    if (eventData.pointerDrag.GetComponent<DragAndDropScript>().myOwnerCode == 1) // from Object Rect to Players Rect
                    {
                        Debug.Log("Second if works");
                        bool ans = exchangeCtrl.RectSubj.GetComponent<PlayerStorageUIController>().PlusDDElement(eventData.pointerDrag);
                        if (ans)
                        {
                            Debug.Log("Third if works");
                            exchangeCtrl.RectObj.GetComponent<AbstractStorageUIController>().MinusDDElement(eventData.pointerDrag.GetComponent<DragAndDropScript>().myNumberInStorage);
                            exchangeCtrl.PutObjectIntoStash(1, eventData.pointerDrag.GetComponent<DragAndDropScript>().myNumberInStorage);
                        }
                        else
                        {
                            Debug.Log("Third else works");
                            // Nothing serious changes
                        }
                    }
                    else  // from Players Rect to Players Rect
                    {
                        Debug.Log("Second else works");
                        // Nothing serious changes
                    }
                }
                else
                {
                    Debug.Log("First else works");
                    // Place object to chest/boiler/salesman inventory
                    if (eventData.pointerDrag.GetComponent<DragAndDropScript>().myOwnerCode == 2) // from Object Rect to Object Rect
                    {
                        // Nothing serious changes
                    }
                    else  // from Players Rect to Object Rect
                    {
                        bool ans = exchangeCtrl.RectSubj.GetComponent<AbstractStorageUIController>().PlusDDElement(eventData.pointerDrag);
                        if (ans)
                        {
                            exchangeCtrl.RectObj.GetComponent<PlayerStorageUIController>().MinusDDElement(eventData.pointerDrag.GetComponent<DragAndDropScript>().myNumberInStorage);
                            exchangeCtrl.PutObjectIntoStash(2, eventData.pointerDrag.GetComponent<DragAndDropScript>().myNumberInStorage);
                        }
                        else
                        {
                            // Nothing serious changes
                        }
                    }
                }


                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            }
        }
    }
}