using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Event", menuName = "ScriptableObjects/Timer Tictoc Event", order = 1)] // 1
public class TimerTictocEvent : ScriptableObject
{
    private List<TimerTictocEventListener> listeners = new List<TimerTictocEventListener>(); // 3

    public void Raise() // 4
    {
        for (int i = listeners.Count - 1; i >= 0; i--) // 5
        {
            listeners[i].OnEventRaised(); // 6
        }
    }

    public void RegisterListener(TimerTictocEventListener listener) // 7
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(TimerTictocEventListener listener) // 8
    {
        listeners.Remove(listener);
    }
}
