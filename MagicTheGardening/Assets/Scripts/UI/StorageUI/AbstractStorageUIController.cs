using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class AbstractStorageUIController : MonoBehaviour
    {
        public GameObject microElementPrefab;
        public virtual void SetSelfScaling(Vector2 centerCoords)
        {

        }
        public virtual void SetAnotherDDElement(Sprite spr, string nm, int vol)
        {

        }

        public virtual void CreateOnceMoreDDElement(int cellElement, Sprite spr, string prName)
        {
            
        }
    }
}