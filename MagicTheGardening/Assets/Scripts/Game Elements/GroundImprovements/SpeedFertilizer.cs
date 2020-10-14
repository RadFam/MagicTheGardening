using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameElement;

namespace Improvements
{
    public class SpeedFertilizer : BaseImprovement
    {
        public SpeedFertilizer(ScriptableImprovement sci, GameObject obj)
            : base(sci, obj)
        {

        }

        public Mesh GetMesh()
        {
            SpeedFertilizeScriptableImpovement sfsi = improveSC as SpeedFertilizeScriptableImpovement;
            return sfsi.fertilizerMesh;
        }

        public Material GetMaterial()
        {
            SpeedFertilizeScriptableImpovement sfsi = improveSC as SpeedFertilizeScriptableImpovement;
            return sfsi.fertilizerMaterial;
        }

        public override void ApplyImprovemenetEffect()
        {
            int tmp = gameObj.GetComponent<GroundStateControl>().groundType;
            if (gameObj.GetComponent<GroundStateControl>().AddFertilizer(this, GetMaxDates()[tmp], GetFlowDates()[tmp], GetMaxDates()[tmp]))
            {
                SpeedFertilizeScriptableImpovement sfsi = improveSC as SpeedFertilizeScriptableImpovement;
                // Here mast be a condition check if plant already exists (!!!!)
                if (gameObj.GetComponent<GroundStateControl>().GetPlantObject() != null)
                {
                    gameObj.GetComponent<GroundStateControl>().GetPlantObject().GetComponent<IPlantImprovement>().SetGrowthAccelerationRate(sfsi.speedGrowthImps);
                }            
            }
        }

        public override void CancelImprovementEffect()
        {
            // Here mast be a condition check if plant already exists (!!!!)
            if (gameObj.GetComponent<GroundStateControl>().GetPlantObject() != null)
            {
                gameObj.GetComponent<GroundStateControl>().GetPlantObject().GetComponent<IPlantImprovement>().RemoveGrowthAccelerationRate();
            }
        }
    }
}