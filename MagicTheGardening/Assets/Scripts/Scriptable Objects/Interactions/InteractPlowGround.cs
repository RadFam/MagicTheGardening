using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameElement;

[CreateAssetMenu(fileName = "PlowGround", menuName = "ScriptableObjects/Interactions/Interaction_PlowGround", order = 0)]
public class InteractPlowGround : Interaction
{
    GroundStateControl GSC;

    public override void Init()
    {
        GSC = parentObj.GetComponent<GroundStateControl>();
    }

    public override void React()
    {
        bool ans = GSC.AddPlowing();
    }
}
