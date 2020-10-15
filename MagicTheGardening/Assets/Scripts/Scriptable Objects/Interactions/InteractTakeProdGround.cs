﻿using System.Collections;
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

        GSC.TakePlantGround();
    }
}