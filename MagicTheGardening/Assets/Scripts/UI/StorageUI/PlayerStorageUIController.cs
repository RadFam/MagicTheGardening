using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class PlayerStorageUIController : AbstractStorageUIController
    {

        public DragAndDropScript microElementPrefab;

        int cellsWidth;
        int cellsHeight;

        int cntr = 0;

        // Magic coefficients
        float widthCoeff = 5.0f;
        float heightCoeff = 2.7f;

        RectTransform myRectTransform;
        Canvas myCanvas;

        [SerializeField]
        List<GameObject> innerElements; // = new List<GameObject>();

        List<GameObject> ddElements = new List<GameObject>();

        // Use this for initialization
        void Start()
        {
            cellsHeight = 2;
            cellsWidth = 3;

            myRectTransform = GetComponent<RectTransform>();
            myCanvas = FindObjectOfType<Canvas>();

            cntr = 0;
        }

        void OnEnable()
        {
            cellsHeight = 2;
            cellsWidth = 3;

            myRectTransform = GetComponent<RectTransform>();
            myCanvas = FindObjectOfType<Canvas>();

            cntr = 0;

            //gameObject.transform.parent = myCanvas.transform;
        }

        void OnDisable()
        {
            // Destroy all DnD Elements
            foreach (GameObject go in ddElements)
            {
                Destroy(go);
            }

            ddElements.Clear();

            /*
            foreach (GameObject go in innerElements)
            {
                GameObject goCh = go.transform.GetChild(0).gameObject;
                if (goCh != null)
                {
                    Destroy(goCh);
                }
            }
             * */
        }

        public override void SetSelfScaling(Vector2 centerCoords) // produced on gameObject initiation
        {
            Vector2 canvasScale = myCanvas.GetComponent<CanvasScaler>().referenceResolution;
            //Debug.Log("canvasScale: " + canvasScale);
            Vector2 canvasSize = new Vector2(myCanvas.GetComponent<RectTransform>().rect.width, myCanvas.GetComponent<RectTransform>().rect.height);
            //Debug.Log("canvasSize: " + canvasSize);

            Vector2 newCenterCoordinates = new Vector2((centerCoords.x-0.5f) * canvasSize.x, (centerCoords.y-0.5f) * canvasSize.y);

            myRectTransform.anchoredPosition = newCenterCoordinates;

            float deltaSizeCoeff = Mathf.Min(canvasSize.x / 10.0f, canvasSize.y / 5.0f);

            float myWidth = (int)(deltaSizeCoeff * cellsWidth);
            float myHeight = (int)(deltaSizeCoeff * cellsHeight);

            //float myWidth = (int)myCanvas.GetComponent<RectTransform>().rect.width / widthCoeff;
            //float myHeight = (int)myCanvas.GetComponent<RectTransform>().rect.height / heightCoeff;
            myRectTransform.sizeDelta = new Vector2(myWidth, myHeight);

            GridLayoutGroup glg = GetComponent<GridLayoutGroup>();
            glg.cellSize = new Vector2(myWidth * 0.9f / cellsWidth, myHeight * 0.9f / cellsHeight);
            glg.padding.left = (int)(myWidth * 0.1f / (cellsWidth+1));
            glg.padding.top = (int)(myHeight * 0.1f / (cellsHeight+1));
            glg.spacing = new Vector2(myWidth * 0.1f / (cellsWidth+1), myHeight * 0.1f / (cellsHeight+1));

            foreach (GameObject me in innerElements)
            {
                LayoutElement le = me.GetComponent<LayoutElement>();
                le.flexibleHeight = (int)(myHeight * 0.9f / cellsHeight);
                le.flexibleWidth = (int)(myHeight * 0.9f / cellsWidth);
            }
        }

        public override void SetAnotherDDElement(Sprite spr) // produced on gameObject initiation
        {
            if (cntr <= innerElements.Count)
            {
                DragAndDropScript ddElement = Instantiate(microElementPrefab);
                //ddElement.transform.parent = innerElements[cntr].transform;
                //ddElement.transform.localPosition = new Vector3(0.0f, 0.0f, -1.0f);
                ddElement.transform.parent = myCanvas.transform;
                ddElement.transform.position = innerElements[cntr].transform.position;
                ddElement.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                ddElement.GetComponent<RectTransform>().sizeDelta = innerElements[cntr].GetComponent<RectTransform>().sizeDelta;
                ddElement.MyCanvas = myCanvas;

                ddElement.SetImage(spr, cntr, 2);
                ddElements.Add(ddElement.gameObject);

                cntr++;
            }
        }

        public override bool PlusDDElement(GameObject go) // when DnD element is dropped
        {
            if (ddElements.Count <= innerElements.Count)
            {
                ddElements.Add(go);
                return true;
            }
            return false;
        }

        public override void MinusDDElement(int elNum) // when DnD element is dropped
        {
            ddElements.RemoveAt(elNum);
        }

        public override void RearrangeDDelements() // when DnD element is dropped
        {
            int cntr = 0;

            foreach (GameObject go in ddElements)
            {
                go.transform.parent = innerElements[cntr].transform;
                go.GetComponent<DragAndDropScript>().myNumberInStorage = cntr; // Don`t know if it will work
                go.GetComponent<DragAndDropScript>().myOwnerCode = 2;

                cntr++;
            }
        }
    }
}