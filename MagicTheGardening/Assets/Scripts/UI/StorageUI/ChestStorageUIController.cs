using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class ChestStorageUIController : AbstractStorageUIController
    {

        //public DragAndDropScript microElementPrefab;
        public GameObject microElementPrefab;

        int cellsWidth;
        int cellsHeight;

        int cntr = 0;

        // Magic coefficients
        float widthCoeff = 2.5f;
        float heightCoeff = 2.7f;

        RectTransform myRectTransform;
        GridLayoutGroup glg;
        Canvas myCanvas;

        [SerializeField]
        List<GameObject> innerElements = new List<GameObject>();

        [SerializeField]
        List<GameObject> ddElements = new List<GameObject>();
        List<Vector2> ddElementsPositions = new List<Vector2>();

        // Use this for initialization
        void Start()
        {
            cellsHeight = 2;
            cellsWidth = 4;

            myRectTransform = GetComponent<RectTransform>();
            myCanvas = FindObjectOfType<Canvas>();

            cntr = 0;
        }

        void OnEnable()
        {
            cellsHeight = 2;
            cellsWidth = 4;

            myRectTransform = GetComponent<RectTransform>();
            myCanvas = FindObjectOfType<Canvas>();
            glg = GetComponent<GridLayoutGroup>();

            cntr = 0;
            ddElementsPositions.Clear();

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

        public override void SetSelfScaling(Vector2 centerCoords)
        {
            Vector2 canvasScale = myCanvas.GetComponent<CanvasScaler>().referenceResolution;
            Vector2 canvasSize = new Vector2(myCanvas.GetComponent<RectTransform>().rect.width, myCanvas.GetComponent<RectTransform>().rect.height);

            Vector2 newCenterCoordinates = new Vector2((centerCoords.x - 0.5f) * canvasSize.x, (centerCoords.y - 0.5f) * canvasSize.y);

            myRectTransform.anchoredPosition = newCenterCoordinates;

            float deltaSizeCoeff = Mathf.Min(canvasSize.x / 10.0f, canvasSize.y / 5.0f);

            float myWidth = (int)(deltaSizeCoeff * cellsWidth);
            float myHeight = (int)(deltaSizeCoeff * cellsHeight);

            myRectTransform.sizeDelta = new Vector2(myWidth, myHeight);

            glg.cellSize = new Vector2(myWidth * 0.9f / cellsWidth, myHeight * 0.9f / cellsHeight);
            glg.padding.left = (int)(myWidth * 0.1f / (cellsWidth + 1));
            glg.padding.top = (int)(myHeight * 0.1f / (cellsHeight + 1));
            glg.spacing = new Vector2(myWidth * 0.1f / (cellsWidth + 1), myHeight * 0.1f / (cellsHeight + 1));

            int tmpHeight = (int)(myHeight * 0.9f / cellsHeight);
            int tmpWidth = (int)(myHeight * 0.9f / cellsHeight);

            //Debug.Log("tmpWidth: " + tmpWidth.ToString() + "  tmpHeight: " + tmpHeight.ToString());

            foreach (GameObject me in innerElements)
            {
                LayoutElement le = me.GetComponent<LayoutElement>();
                le.flexibleHeight = (int)(myHeight * 0.9f / cellsHeight);
                le.flexibleWidth = (int)(myHeight * 0.9f / cellsWidth);
                me.GetComponent<StorageSlotScript>().MyAccessory = 1;
            }

            //Debug.Log("panelCenter: " + newCenterCoordinates);
            //Debug.Log("panelSize: " + myRectTransform.sizeDelta);
            for (int i = 0; i < cellsHeight; ++i)
            {
                for (int j = 0; j < cellsWidth; ++j)
                {
                    Vector2 anchorCenter = new Vector2(newCenterCoordinates.x - myWidth / 2 + glg.padding.left + j * (glg.spacing.x + tmpWidth) + tmpWidth/2,
                        newCenterCoordinates.y + myHeight / 2 - (glg.padding.top + i * (glg.spacing.y + tmpHeight) + tmpHeight / 2));

                    ddElementsPositions.Add(anchorCenter);
                    //Debug.Log("anchorCenter: " + anchorCenter);
                }
            }
        }

        public override void SetAnotherDDElement(Sprite spr, string prName, int prVol)
        {
            if (cntr <= innerElements.Count)
            {
                //GameObject ddElement = Instantiate(microElementPrefab, innerElements[cntr].transform.position, innerElements[cntr].transform.rotation) as GameObject;
                //GameObject ddElement = Instantiate(microElementPrefab, myCanvas.transform, false) as GameObject;
                GameObject ddElement = Instantiate(microElementPrefab, innerElements[cntr].transform, true) as GameObject;

                //ddElement.GetComponent<RectTransform>().anchoredPosition = ddElementsPositions[cntr];
                ddElement.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                ddElement.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                ddElement.GetComponent<RectTransform>().sizeDelta = glg.cellSize;
                //ddElement.GetComponent<DragAndDropScript>().MyCanvas = myCanvas;

                //ddElement.GetComponent<DragAndDropScript>().SetImage(spr, cntr, 1);
                ddElement.GetComponent<DragScript>().SetImage(spr, prName, prVol, 1);
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
            int cntrr = 0;

            foreach (GameObject go in ddElements)
            {
                go.GetComponent<RectTransform>().anchoredPosition = ddElementsPositions[cntrr];

                go.GetComponent<DragAndDropScript>().myNumberInStorage = cntrr; // Don`t know if it will work
                go.GetComponent<DragAndDropScript>().myOwnerCode = 1;

                cntrr++;
            }
        }
    }
}