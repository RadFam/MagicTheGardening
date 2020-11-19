using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StorageSlotScript : MonoBehaviour, IDropHandler
{
    int myAccessory;

    public int MyAccessory
    {
        get { return myAccessory; }
        set { myAccessory = value; }
    }

    public GameObject MyChild
    {
        get
        {
            if (gameObject.transform.childCount > 0)
            {
                return gameObject.transform.GetChild(0).gameObject;
            }

            return null;
        }
    }

	public void OnDrop(PointerEventData eventData)
    {
        if (!MyChild)
        {
            DragScript.dragItem.transform.SetParent(gameObject.transform);
            DragScript.dragItem.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }
}
