using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameControllers
{
    public class TimerController : MonoBehaviour, ISaveable
    {
        int globalGameTimer = 0;
        TimerTictocEvent timerTTEvent;

        public int GlobalGameTimer
        {
            get { return globalGameTimer; }
            set { globalGameTimer = value; }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        public void StartNewDay()
        {
            globalGameTimer++;
            timerTTEvent.Raise();
        }

        // Just as an interface stub
        public object CaptureObject()
        {
            //..........
            return null;
        }

        // Just as an interface stub
        public void RestoreObject(object obj)
        {
            //.........
            return;
        }
    }
}