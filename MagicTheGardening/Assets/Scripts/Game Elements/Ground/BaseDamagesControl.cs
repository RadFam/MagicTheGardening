using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameElement
{
    public class BaseDamagesControl : MonoBehaviour
    {

        BaseDamageGround baseDamagesProbs;
        float decreaseInsectFactor;
        float decreaseMoleratFactor;
        int groundType;

        public float DecInsectFactor
        {
            get { return decreaseInsectFactor; }
            set { decreaseInsectFactor = value; }
        }

        public float DecMoleratFactor
        {
            get { return decreaseMoleratFactor; }
            set { decreaseMoleratFactor = value; }
        }

        public int GroundType
        {
            get { return groundType; }
            set { groundType = value; }
        }

        // Use this for initialization
        void Start()
        {
            baseDamagesProbs = Resources.Load<BaseDamageGround>("ScriptObjects/Ground_Damage");
            decreaseInsectFactor = 1.0f;
            decreaseMoleratFactor = 1.0f;
            groundType = GetComponent<GroundStateControl>().groundType;
        }

        public bool EvalInsectDamage()
        {
            float currDamageFactor = baseDamagesProbs.insectDamageProbs[groundType];
            currDamageFactor *= decreaseInsectFactor;

            float val = Random.Range(0.0f, 1.0f);
            return val < currDamageFactor;
        }

        public bool EvalMoleratDamage()
        {
            float currDamageFactor = baseDamagesProbs.moleratDamageProbs[groundType];
            currDamageFactor *= decreaseMoleratFactor;

            float val = Random.Range(0.0f, 1.0f);
            return val < currDamageFactor;
        }

        public void CancelInsectFactor()
        {
            decreaseInsectFactor = 1.0f;
        }

        public void CancelMoleratFactor()
        {
            decreaseMoleratFactor = 1.0f;
        }

        public bool EvalForAllDamages()
        {
            if (EvalInsectDamage())
            {
                return true;
            }
            if (EvalMoleratDamage())
            {
                return true;
            }
            return false;
        }
    }
}