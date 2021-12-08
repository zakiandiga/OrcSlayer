using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Event", fileName = "CustomUtilities/New Game Event")]
public class GameEvent : ScriptableObject
{
    private HashSet<EventSubscriber> subscribers = new HashSet<EventSubscriber>();

    public void Invoke()
    {
        foreach (EventSubscriber subscriber in subscribers)
        {
            subscriber.RaiseEvent();
        }
    }

    public void AddSubscriber(EventSubscriber eventListener) => subscribers.Add(eventListener);

    public void RemoveSubscriber(EventSubscriber eventListener) => subscribers.Remove(eventListener);
}
