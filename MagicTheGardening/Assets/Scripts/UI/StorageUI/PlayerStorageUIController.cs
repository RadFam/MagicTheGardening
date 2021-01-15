using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class PlayerStorageUIController : AbstractStorageUIController
    {

        //public DragAndDropScript microElementPrefab;
        //public GameObject microElementPrefab;

        int cellsWidth;
        int cellsHeight;

        int cntr = 0;

        // Magic coefficients
        float widthCoeff = 5.0f;
        float heightCoeff = 2.7f;

        RectTransform myRectTransform;
        GridLayoutGroup glg;
        Canvas myCanvas;

        [SerializeField]
        List<GameObject> innerElements; // = new List<GameObject>();

        [SerializeField]
        List<GameObject> ddElements = new List<GameObject>();
        List<Vector2> ddElementsPositions = new List<Vector2>();

        [SerializeField]
        GameObject emptyStub;

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
            glg = GetComponent<GridLayoutGroup>();
            ddElementsPositions.Clear();

            //emptyStub.transform.SetParent(myCanvas.transform, false);
            //emptyStub.transform.localScale = new Vector3(0,0,0);
            //ddElements.Add(emptyStub);
            cntr = 0;
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
            Vector2 canvasSize = new Vector2(myCanvas.GetComponent<RectTransform>().rect.width, myCanvas.GetComponent<RectTransform>().rect.height);

            Vector2 newCenterCoordinates = new Vector2((centerCoords.x-0.5f) * canvasSize.x, (centerCoords.y-0.5f) * canvasSize.y);

            myRectTransform.anchoredPosition = newCenterCoordinates;

            float deltaSizeCoeff = Mathf.Min(canvasSize.x / 10.0f, canvasSize.y / 5.0f);

            float myWidth = (int)(deltaSizeCoeff * cellsWidth);
            float myHeight = (int)(deltaSizeCoeff * cellsHeight);

            myRectTransform.sizeDelta = new Vector2(myWidth, myHeight);
            
            glg.cellSize = new Vector2(myWidth * 0.9f / cellsWidth, myHeight * 0.9f / cellsHeight);
            glg.padding.left = (int)(myWidth * 0.1f / (cellsWidth+1));
            glg.padding.top = (int)(myHeight * 0.1f / (cellsHeight+1));
            glg.spacing = new Vector2(myWidth * 0.1f / (cellsWidth+1), myHeight * 0.1f / (cellsHeight+1));

            int tmpHeight = (int)(myHeight * 0.9f / cellsHeight);
            int tmpWidth = (int)(myHeight * 0.9f / cellsHeight);

            int tmpCntr = 0;
            foreach (GameObject me in innerElements)
            {
                LayoutElement le = me.GetComponent<LayoutElement>();
                le.flexibleHeight = (int)(myHeight * 0.9f / cellsHeight);
                le.flexibleWidth = (int)(myHeight * 0.9f / cellsWidth);
                me.GetComponent<StorageSlotScript>().MyAccessory = 2;
                me.GetComponent<StorageSlotScript>().MyPersonalNum = tmpCntr;
                tmpCntr++;
            }

            for (int i = 0; i < cellsHeight; ++i)
            {
                for (int j = 0; j < cellsWidth; ++j)
                {
                    Vector2 anchorCenter = new Vector2(newCenterCoordinates.x - myWidth / 2 + glg.padding.left + j * (glg.spacing.x + tmpWidth) + tmpWidth / 2,
                        newCenterCoordinates.y + myHeight / 2 - (glg.padding.top + i * (glg.spacing.y + tmpHeight) + tmpHeight / 2));

                    ddElementsPositions.Add(anchorCenter);
                }
            }

            ddElementsPositions.Add(new Vector2(0,0));
        }

        public override void SetAnotherDDElement(Sprite spr, string prName, int prVol) // produced on gameObject initiation
        {
            if (cntr <= innerElements.Count)
            {
                GameObject ddElement = Instantiate(microElementPrefab, innerElements[cntr].transform, true) as GameObject;
                
                ddElement.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                ddElement.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                ddElement.GetComponent<RectTransform>().sizeDelta = glg.cellSize;
                ddElement.GetComponent<DragScript>().SetImage(spr, prName, prVol, 2);
                ddElements.Add(ddElement.gameObject);

                cntr++;
            }
        }

        public override void CreateOnceMoreDDElement(int cellElement, Sprite spr, string prName)
        {
            GameObject ddElement = Instantiate(microElementPrefab, innerElements[cellElement].transform, true) as GameObject;

            ddElement.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            ddElement.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            ddElement.GetComponent<RectTransform>().sizeDelta = glg.cellSize;
            ddElement.GetComponent<DragScript>().SetImage(spr, prName, 0, 2);
            ddElements.Add(ddElement.gameObject);
        }
    }
}