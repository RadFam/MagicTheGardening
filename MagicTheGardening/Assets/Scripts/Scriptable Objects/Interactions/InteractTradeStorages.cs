using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameElement;
using GameControllers;
using GameUI;

[CreateAssetMenu(fileName = "TradeItems", menuName = "ScriptableObjects/Interactions/Interaction_TradeItems", order = 5)]
public class InteractTradeStorages : Interaction {

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
        myStType = StorageTypes.Salesman;
    }

    public override void React(string name)
    {
        SSc_obj = parentObj.GetComponent<StorageScript>();
        SSc_subj = clientObj.GetComponent<StorageScript>();

        if (SSc_subj != null && SSc_obj != null)
        {
            exCtrl.ShowPlayerSales(ref SSc_obj, ref SSc_subj, myStType);
        }
    }
}
