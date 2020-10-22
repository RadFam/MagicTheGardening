using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class PlayerStorageUIController : MonoBehaviour
    {

        int cellsWidth;
        int cellsHeight;

        // Magic coefficients
        float widthCoeff = 5.0f;
        float heightCoeff = 2.7f;

        RectTransform myRectTransform;
        Canvas myCanvas;

        [SerializeField]
        List<GameObject> innerElements = new List<GameObject>();

        // Use this for initialization
        void Start()
        {
            cellsHeight = 2;
            cellsWidth = 2;

            myRectTransform = GetComponent<RectTransform>();
            myCanvas = FindObjectOfType<Canvas>();
        }

        public void SetSelfScaling(Vector2 centerCoords)
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
    }
}