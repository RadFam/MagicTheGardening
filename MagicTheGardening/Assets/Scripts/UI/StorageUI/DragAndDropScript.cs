using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace GameUI
{
    public class DragAndDropScript : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        public int myNumberInStorage;
        public int myOwnerCode; // 1 - object, 2 - subject

        RectTransform myRectangle;
        Canvas myCanvas;

        public Canvas MyCanvas
        {
            get { return myCanvas; }
            set { myCanvas = value; }
        }

        public RectTransform MyRect
        {
            get { return myRectangle; }
            set { myRectangle = value; }
        }

        public void SetImage(Sprite img, int storageNum, int storageOwn)
        {
            Image spr = GetComponent<Image>();
            spr.sprite = img;
            myNumberInStorage = storageNum;
            myOwnerCode = storageOwn;
        }

        public void ResetImage()
        {
            Image spr = GetComponent<Image>();
            spr.sprite = null;
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            myRectangle.anchoredPosition += eventData.delta / myCanvas.scaleFactor;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {

        }

        public void OnEndDrag(PointerEventData eventData)
        {

        }

        public void OnPointerDown(PointerEventData eventData)
        {

        }
    }
}