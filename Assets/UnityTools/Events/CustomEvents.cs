using UnityEngine;

namespace UnityTools.Events
{
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

        protected virtual void RaiseEvent<T>(string className, string eventName, bool isSilent, EventHandler<T> Event, T parameter)
        {
            if (Event != null)
            {
                if (!isSilent)
                    Debug.Log(className + ": The event '" + eventName + "' was raised: " + parameter);

                Event.Invoke(parameter);
            }
            else
            {
                if (!isSilent)
                    Debug.Log(className + ": The event '" + eventName + "' was not raised because nothing subscibes to it.");
            }
        }
    }
}