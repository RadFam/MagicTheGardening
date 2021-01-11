using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameElement;
using GameControllers;
using GameUI;

[CreateAssetMenu(fileName = "ExchangeItems", menuName = "ScriptableObjects/Interactions/Interaction_ExchangeItems", order = 4)]
public class InteractExchangeStorages : Interaction
{

    //public GameObject parentObj; // GameObject that own the Interaction object
    //public GameObject clientObj; // Object that turns to the parentObj

    // DO HERE NOTHING HERE BECAUSE WE NEED TO THINK ABOUT HOW 
    // MANY STORAGES CAN SERVE THIS SCRIPTABLE OBJECT(!!!!)

    StorageScript SSc_obj;
    StorageScript SSc_subj;
    ExchangeUIController exCtrl;
    StorageTypes myStType;

    public override void SetParent(GameObject go)
    {
        
    }
    public override void SetClient(GameObject go)
    {
        
    }

    public override GameObject GetParent(string name)
    {
        return null;
    }

    public override GameObject GetClient(string name)
    {
        return null;
    }

    public override void Init(string name)
    {
        exCtrl = FindObjectOfType<ExchangeUIController>();
        myStType = StorageTypes.Chest;
    }

    public override void React(string name)
    {
        SSc_obj = parentObj.GetComponent<StorageScript>();
        SSc_subj = clientObj.GetComponent<StorageScript>();

        if (SSc_subj != null && SSc_obj != null)
        {
            exCtrl.ShowPlayerChest(ref SSc_obj, ref SSc_subj, myStType);
        }
    }
}
