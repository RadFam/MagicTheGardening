using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameElement;

[CreateAssetMenu(fileName = "SeedGround", menuName = "ScriptableObjects/Interactions/Interaction_SeedGround", order = 2)]
public class InteractSeedGround : Interaction
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
            // Check if Storage "hand" has ProductCommon object, and this products belong to Product_TypeTwo
            // (i.e. has PlantGrowthCycle field)
            ProductCommon PCm = SSc.OnHand;
            if (PCm != null && PCm is Product_TypeTwo)
            {
                Product_TypeTwo PCm_two = PCm as Product_TypeTwo;
                GSC.AddPlant(PCm_two.plantToGrow);
                string res = SSc.FromHandToWorld();
            }
        }
    }
}
