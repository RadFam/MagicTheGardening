using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Improvements
{
    public abstract class ScriptableImprovement : ScriptableObject
    {
        public string soLoadName;
        public List<int> maxDuration;
        public List<int> speedWastage;

        public abstract BaseImprovement InitializeImprovement(GameObject obj);
    }
}
