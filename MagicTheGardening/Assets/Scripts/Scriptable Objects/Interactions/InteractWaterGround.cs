using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameElement;

[CreateAssetMenu(fileName = "WaterGround", menuName = "ScriptableObjects/Interactions/Interaction_WaterGround", order = 1)]
public class InteractWaterGround : Interaction
{
    GroundStateControl GSC;

    public override void Init()
    {
        GSC = parentObj.GetComponent<GroundStateControl>();
    }

    public override void React()
    {
        bool ans = GSC.AddWaterizing();
    }
}
