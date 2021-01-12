using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class SalesmanStorageUIController : AbstractStorageUIController
    {

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

		void Start()
        {
            cellsHeight = 3;
            cellsWidth = 5;

            myRectTransform = GetComponent<RectTransform>();
            myCanvas = FindObjectOfType<Canvas>();

            cntr = 0;
        }

        void OnEnable()
        {
            cellsHeight = 3;
            cellsWidth = 5;

            myRectTransform = GetComponent<RectTransform>();
            myCanvas = FindObjectOfType<Canvas>();
            glg = GetComponent<GridLayoutGroup>();

            cntr = 0;
            ddElementsPositions.Clear();
        }

        void OnDisable()
        {
            foreach (GameObject go in ddElements)
            {
                Destroy(go);
            }

            ddElements.Clear();
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

            foreach (GameObject me in innerElements)
            {
                LayoutElement le = me.GetComponent<LayoutElement>();
                le.flexibleHeight = (int)(myHeight * 0.9f / cellsHeight);
                le.flexibleWidth = (int)(myHeight * 0.9f / cellsWidth);
                me.GetComponent<StorageSlotScript>().MyAccessory = 1;
            }
		}

		public override void SetAnotherDDElement(Sprite spr, string prName, int prVol)
        {
            if (cntr <= innerElements.Count)
            {
                GameObject ddElement = Instantiate(microElementPrefab, innerElements[cntr].transform, true) as GameObject;
                
                ddElement.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                ddElement.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                ddElement.GetComponent<RectTransform>().sizeDelta = glg.cellSize;
                ddElement.GetComponent<DragScript>().SetImage(spr, prName, prVol, 1);
                ddElements.Add(ddElement.gameObject);

                cntr++;
            }
        }

    }
}