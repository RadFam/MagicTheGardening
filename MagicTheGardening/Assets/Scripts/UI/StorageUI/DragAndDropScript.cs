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

        void Awake()
        {
            myRectangle = gameObject.GetComponent<RectTransform>();
        }
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
            //Debug.Log("OnDrag starts");
            myRectangle.anchoredPosition += eventData.delta / myCanvas.scaleFactor;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            //Debug.Log("OnBeginDrag starts");
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            //Debug.Log("OnEndDrag starts");
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            //Debug.Log("OnPointerDown starts");
        }
    }
}