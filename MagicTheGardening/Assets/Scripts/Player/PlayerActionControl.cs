using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameControllers;

namespace PlayerController
{
    public class PlayerActionControl : MonoBehaviour
    {

        private Actions currentAction;

        public Actions GetAction
        {
            get { return currentAction; }
            set { currentAction = value; }
        }

        // Use this for initialization
        void Start()
        {
            currentAction = Actions.NoAction;
        }

        public void ClearAction()
        {
            currentAction = Actions.NoAction;
        }
    }
}