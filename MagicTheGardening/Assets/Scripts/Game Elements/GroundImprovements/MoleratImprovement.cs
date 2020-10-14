using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameElement;

namespace Improvements
{
    public class MoleratImprovement : BaseImprovement
    {
        public MoleratImprovement(ScriptableImprovement sci, GameObject obj)
            : base(sci, obj)
        {

        }

        public override void ApplyImprovemenetEffect()
        {
            int tmp = gameObj.GetComponent<GroundStateControl>().groundType;

            if (gameObj.GetComponent<GroundStateControl>().AddImprovement(this, GetMaxDates()[tmp], GetFlowDates()[tmp], GetMaxDates()[tmp]))
            {
                MoleratScriptableImrovement msi = improveSC as MoleratScriptableImrovement;
                gameObj.GetComponent<BaseDamagesControl>().DecMoleratFactor = msi.moleratImps[tmp];
            }
        }

        public override void CancelImprovementEffect()
        {
            if (gameObj.GetComponent<BaseDamagesControl>() != null)
            {
                gameObj.GetComponent<BaseDamagesControl>().CancelMoleratFactor();
            }
        }
    }
}