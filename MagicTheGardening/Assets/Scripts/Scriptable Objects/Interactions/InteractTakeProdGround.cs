using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameElement;

[CreateAssetMenu(fileName = "TakeProdGround", menuName = "ScriptableObjects/Interactions/Interaction_TakeProdGround", order = 3)]
public class InteractTakeProdGround : Interaction
{

    GroundStateControl GSC;
    StorageScript SSc;

    static Dictionary<string, GameObject> parentObjects = new Dictionary<string, GameObject>();
    static Dictionary<string, GameObject> clientObjects = new Dictionary<string, GameObject>();

    public override void SetParent(GameObject go)
    {
        parentObjects[go.name] = go;
    }
    public override void SetClient(GameObject go)
    {
        clientObj = go;
    }

    public override GameObject GetParent(string name)
    {
        return parentObjects[name];
    }

    public override GameObject GetClient(string name)
    {
        return clientObj;
    }

    public override void Init(string name)
    {
        //GSC = parentObj.GetComponent<GroundStateControl>();
    }

    public override void React(string name)
    {
        GSC = GetParent(name).GetComponent<GroundStateControl>();
        SSc = GetClient(name).GetComponent<StorageScript>();
        if (SSc != null)
        {
            GSC.TakePlantGround(ref SSc);
        }
    }
}
