using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class AbstractStorageUIController : MonoBehaviour
    {
        public virtual void SetSelfScaling(Vector2 centerCoords)
        {

        }
        public virtual void SetAnotherDDElement(Sprite spr, string nm, int vol)
        {

        }
        public virtual bool PlusDDElement(GameObject go)
        {
            return true;
        }
        public virtual void MinusDDElement(int elNum)
        {

        }
        public virtual void RearrangeDDelements()
        {

        }
    }
}