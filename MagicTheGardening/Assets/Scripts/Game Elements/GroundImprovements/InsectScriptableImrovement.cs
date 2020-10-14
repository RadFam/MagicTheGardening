using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Improvements
{
    public class InsectScriptableImprovement : ScriptableImprovement
    {
        public List<float> insectImps;

        public override BaseImprovement InitializeImprovement(GameObject gameObj)
        {
            return new InsectImprovement(this, gameObj);
        }
    }
}