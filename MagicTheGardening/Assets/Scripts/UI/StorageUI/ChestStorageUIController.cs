using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class ChestStorageUIController : AbstractStorageUIController
    {

        public DragAndDropScript microElementPrefab;

        int cellsWidth;
        int cellsHeight;

        int cntr = 0;

        // Magic coefficients
        float widthCoeff = 2.5f;
        float heightCoeff = 2.7f;

        RectTransform myRectTransform;
        Canvas myCanvas;

        [SerializeField]
        List<GameObject> innerElements = new List<GameObject>();

        List<GameObject> ddElements = new List<GameObject>();

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
            gameObject.transform.parent = myCanvas.transform;
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
            myRectTransform.anchoredPosition = centerCoords;
            float myWidth = (int)myCanvas.GetComponent<RectTransform>().rect.width / widthCoeff;
            float myHeight = (int)myCanvas.GetComponent<RectTransform>().rect.height / heightCoeff;
            myRectTransform.sizeDelta = new Vector2(myWidth, myHeight);

            GridLayoutGroup glg = GetComponent<GridLayoutGroup>();
            glg.cellSize = new Vector2(myHeight * 0.978f / cellsWidth, myHeight * 0.978f / cellsHeight);
            glg.padding.left = (int)(myHeight * 0.011f);
            glg.padding.top = (int)(myHeight * 0.011f);
            glg.spacing = new Vector2(0f, myHeight * 0.0015f);

            foreach (GameObject me in innerElements)
            {
                LayoutElement le = me.GetComponent<LayoutElement>();
                le.flexibleHeight = (int)(myHeight * 0.978f / cellsHeight);
                le.flexibleWidth = (int)(myHeight * 0.978f / cellsWidth);
            }
        }

        public override void SetAnotherDDElement(Sprite spr)
        {
            if (cntr < innerElements.Count)
            {
                DragAndDropScript ddElement = Instantiate(microElementPrefab);
                ddElement.transform.parent = innerElements[cntr].transform;
                ddElement.MyCanvas = myCanvas;

                ddElement.SetImage(spr, cntr, 1);
                ddElements.Add(ddElement.gameObject);

                cntr++;
            }
        }

        public override bool PlusDDElement(GameObject go) // when DnD element is dropped
        {
            if (ddElements.Count < innerElements.Count)
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
                go.GetComponent<DragAndDropScript>().myOwnerCode = 1;

                cntr++;
            }
        }
    }
}