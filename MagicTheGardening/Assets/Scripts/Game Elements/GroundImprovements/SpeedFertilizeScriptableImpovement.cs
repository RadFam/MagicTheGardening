using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Improvements
{
    public class SpeedFertilizeScriptableImpovement : ScriptableImprovement
    {

        public Mesh fertilizerMesh;
        public Material fertilizerMaterial;

        public List<float> speedGrowthImps; // Different speed for different ground type(!)

        public override BaseImprovement InitializeImprovement(GameObject gameObj)
        {
            return new InsectImprovement(this, gameObj);
        }
    }
}