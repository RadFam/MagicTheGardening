using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Improvements
{
    public abstract class BaseImprovement
    {
        public ScriptableImprovement improveSC;
        public GameObject gameObj;

        public BaseImprovement(ScriptableImprovement sci, GameObject obj)
        {
            improveSC = sci;
            gameObj = obj;
        }

        public void ActivateImprovement()
        {
            ApplyImprovemenetEffect();
        }

        public void DeactivateImprovement()
        {
            CancelImprovementEffect();
        }

        public List<int> GetMaxDates()
        {
            return improveSC.maxDuration;
        }

        public List<int> GetFlowDates()
        {
            return improveSC.speedWastage;
        }

        public abstract void ApplyImprovemenetEffect();

        public abstract void CancelImprovementEffect();
    }
}