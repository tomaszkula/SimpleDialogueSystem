using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "event_NewEvent", menuName = "Dialogue System/Event")]
public class DS_Event : ScriptableObject
{
    List<DS_EventsListener> eventsListener = new List<DS_EventsListener>();

    public void Register(DS_EventsListener _eventListener)
    {
        eventsListener.Add(_eventListener);
    }

    public void Unregister(DS_EventsListener _eventListener)
    {
        eventsListener.Remove(_eventListener);
    }

    public void Invoke()
    {
        for (int i = 0; i < eventsListener.Count; i++)
        {
            eventsListener[i].Invoke(this);
        }
    }
}
