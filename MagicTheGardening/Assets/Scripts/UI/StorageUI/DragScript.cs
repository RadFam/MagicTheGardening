using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

namespace GameUI
{
    public class DragScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public static GameObject dragItem;

        public int myNumberInStorage;
        public string myProductName;
        public int myProductVolume; // stay in rest for a while 
        public int myOwnerCode; // 1 - object, 2 - subject

        Vector3 initPosition;
        Transform initParent;
        RectTransform myRectangle;
        public RectTransform MyRect
        {
            get { return myRectangle; }
            set { myRectangle = value; }
        }
        void Awake()
        {
            myRectangle = gameObject.GetComponent<RectTransform>();
        }
        public void SetImage(Sprite img, string prName, int prVol, int storageOwn)
        {
            Image spr = GetComponent<Image>();
            spr.sprite = img;
            myProductName = prName;
            myProductVolume = prVol;
            myOwnerCode = storageOwn;
        }

        public void ResetImage()
        {
            Image spr = GetComponent<Image>();
            spr.sprite = null;
        }


        public void OnBeginDrag(PointerEventData eventData)
        {
            dragItem = gameObject;
            initPosition = gameObject.transform.position;
            initParent = gameObject.transform.parent;

            gameObject.transform.SetParent(transform.root);
            gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;

        }

        public void OnDrag(PointerEventData eventData)
        {
            gameObject.transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            dragItem = null;

            if (gameObject.transform.parent == initParent || gameObject.transform.parent == gameObject.transform.root)
            {
                gameObject.transform.position = initPosition;
                gameObject.transform.SetParent(initParent);
            }

            gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }
}