using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameElement;

[CreateAssetMenu(fileName = "WaterGround", menuName = "ScriptableObjects/Interactions/Interaction_WaterGround", order = 1)]
public class InteractWaterGround : Interaction
{
    GroundStateControl GSC;

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
        bool ans = GSC.AddWaterizing();
    }
}
