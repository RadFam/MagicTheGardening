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
            exchangeCtrl = gameObject.GetComponent<ExchangeUIController>();
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                // Check, which of two storage rectangles is closer to eventData.pointerDrag
                float distToSubj = Vector2.Distance(eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition, exchangeCtrl.RectSubj.GetComponent<RectTransform>().anchoredPosition);
                float distToObj = Vector2.Distance(eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition, exchangeCtrl.RectObj.GetComponent<RectTransform>().anchoredPosition);

                if (distToSubj <= distToObj)
                {
                    // Place object to players inventory
                    if (eventData.pointerDrag.GetComponent<DragAndDropScript>().myOwnerCode == 1) // from Object Rect to Players Rect
                    {
                        bool ans = exchangeCtrl.RectSubj.GetComponent<PlayerStorageUIController>().PlusDDElement(eventData.pointerDrag);
                        if (ans)
                        {
                            exchangeCtrl.RectObj.GetComponent<AbstractStorageUIController>().MinusDDElement(eventData.pointerDrag.GetComponent<DragAndDropScript>().myNumberInStorage);
                            exchangeCtrl.PutObjectIntoStash(1, eventData.pointerDrag.GetComponent<DragAndDropScript>().myNumberInStorage);
                        }
                        else
                        {
                            // Nothing serious changes
                        }
                    }
                    else  // from Players Rect to Players Rect
                    {
                        // Nothing serious changes
                    }
                }
                else
                {
                    // Place object to chest/boiler/salesman inventory
                    if (eventData.pointerDrag.GetComponent<DragAndDropScript>().myOwnerCode == 2) // from Object Rect to Object Rect
                    {
                        // Nothing serious changes
                    }
                    else  // from Players Rect to Object Rect
                    {
                        bool ans = exchangeCtrl.RectSubj.GetComponent<PlayerStorageUIController>().PlusDDElement(eventData.pointerDrag);
                        if (ans)
                        {
                            exchangeCtrl.RectObj.GetComponent<AbstractStorageUIController>().MinusDDElement(eventData.pointerDrag.GetComponent<DragAndDropScript>().myNumberInStorage);
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