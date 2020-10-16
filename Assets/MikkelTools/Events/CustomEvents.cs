using UnityEditor.MemoryProfiler;
using UnityEngine;

public class CustomEvents
{
    public delegate void EventHandler();
    public delegate void EventHandler<T>(T parameter);

    protected virtual void RaiseEvent(string className, string eventName, bool isSilent, EventHandler Event)
    {
        if (Event != null)
        {
            if (!isSilent)
                Debug.Log(className + ": The event '" + eventName + "' was raised.");

            Event.Invoke();
        }
        else
        {
            if (!isSilent)
                Debug.Log(className + ": The event '" + eventName + "' was not raised because nothing subscibes to it.");
        }
    }

    protected virtual void RaiseEvent(string className, string eventName, bool isSilent, EventHandler<int> Event, int intParameter)
    {
        if (Event != null)
        {
            if (!isSilent)
                Debug.Log(className + ": The event '" + eventName + "' was raised: " + intParameter);

            Event.Invoke(intParameter);
        }
        else
        {
            if (!isSilent)
                Debug.Log(className + ": The event '" + eventName + "' was not raised because nothing subscibes to it.");
        }
    }

    protected virtual void RaiseEvent(string className, string eventName, bool isSilent, EventHandler<GameObject> Event, GameObject gameObjectParameter)
    {
        if (Event != null)
        {
            if (!isSilent)
                Debug.Log(className + ": The event '" + eventName + "' was raised: " + gameObjectParameter);

            Event.Invoke(gameObjectParameter);
        }
        else
        {
            if (!isSilent)
                Debug.Log(className + ": The event '" + eventName + "' was not raised because nothing subscibes to it.");
        }
    }

    protected virtual void RaiseEvent(string className, string eventName, bool isSilent, EventHandler<Rigidbody> Event, Rigidbody rigidbodyParamerter)
    {
        if (Event != null)
        {
            if (!isSilent)
                Debug.Log(className + ": The event '" + eventName + "' was raised: " + rigidbodyParamerter);

            Event.Invoke(rigidbodyParamerter);
        }
        else
        {
            if (!isSilent)
                Debug.Log(className + ": The event '" + eventName + "' was not raised because nothing subscibes to it.");
        }
    }
}
