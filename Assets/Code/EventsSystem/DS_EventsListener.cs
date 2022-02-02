using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DS_EventsListener : MonoBehaviour
{
    [Serializable]
    public class EventContainer
    {
        public DS_Event Event = null;
        public UnityEvent Actions = null;
    }

    [SerializeField] List<EventContainer> events = new List<EventContainer>();

    private void OnEnable()
    {
        for (int i = 0; i < events.Count; i++)
        {
            events[i].Event?.Register(this);
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < events.Count; i++)
        {
            events[i].Event?.Unregister(this);
        }
    }

    public void Invoke(DS_Event _event)
    {
        for (int i = 0; i < events.Count; i++)
        {
            if (events[i].Event == _event)
            {
                events[i].Actions?.Invoke();
            }
        }
    }
}
