using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameElement;

namespace Improvements
{
    public class InsectImprovement : BaseImprovement
    {
        public InsectImprovement(ScriptableImprovement sci, GameObject obj)
            : base(sci, obj)
        {

        }

        public override void ApplyImprovemenetEffect()
        {
            int tmp = gameObj.GetComponent<GroundStateControl>().groundType;

            if (gameObj.GetComponent<GroundStateControl>().AddImprovement(this, GetMaxDates()[tmp], GetFlowDates()[tmp], GetMaxDates()[tmp]))
            {
                InsectScriptableImprovement isi = improveSC as InsectScriptableImprovement;
                gameObj.GetComponent<BaseDamagesControl>().DecInsectFactor = isi.insectImps[tmp];
            }
        }

        public override void CancelImprovementEffect()
        {
            if (gameObj.GetComponent<BaseDamagesControl>() != null)
            {
                gameObj.GetComponent<BaseDamagesControl>().CancelInsectFactor();
            }
        }
    }
}