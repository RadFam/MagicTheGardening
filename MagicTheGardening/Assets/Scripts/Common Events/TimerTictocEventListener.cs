using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerTictocEventListener : MonoBehaviour
{
    [SerializeField]
    private TimerTictocEvent timerEvent; // 2
    [SerializeField]
    private UnityEvent response; // 3

    private void OnEnable() // 4
    {
        timerEvent.RegisterListener(this);
    }

    private void OnDisable() // 5
    {
        timerEvent.UnregisterListener(this);
    }

    public void OnEventRaised() // 6
    {
        timerEvent.Invoke();
    }
}
