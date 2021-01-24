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

        public Sprite GetInsectPic
        {
            get {return baseDamagesProbs.insectDamage;}
        }

        public Sprite GetMoleratPic
        {
            get {return baseDamagesProbs.moleratDamage;}
        }

        public int GroundType
        {
            get { return groundType; }
            set { groundType = value; }
        }

        // Use this for initialization
        void Awake()
        {
            baseDamagesProbs = Resources.Load<BaseDamageGround>("ScriptableObjects/Ground_Damage");
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

        public int EvalForAllDamages() // 0 - none, 1 - insect, 2 - molerate
        {
            if (EvalInsectDamage())
            {
                return 1;
            }
            if (EvalMoleratDamage())
            {
                return 2;
            }
            return 0;
        }
    }
}