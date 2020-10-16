using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameElement;

[CreateAssetMenu(fileName = "TakeProdGround", menuName = "ScriptableObjects/Interactions/Interaction_TakeProdGround", order = 3)]
public class InteractTakeProdGround : Interaction
{

    GroundStateControl GSC;
    StorageScript SSc;

    public override void Init()
    {
        GSC = parentObj.GetComponent<GroundStateControl>();
    }

    public override void React()
    {
        SSc = clientObj.GetComponent<StorageScript>();
        if (SSc != null)
        {
            GSC.TakePlantGround(ref SSc);
        }
    }
}
