using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Improvements
{
    public class MoleratScriptableImrovement : ScriptableImprovement
    {
        public List<float> moleratImps;

        public override BaseImprovement InitializeImprovement(GameObject gameObj)
        {
            return new MoleratImprovement(this, gameObj);
        }
    }   
}