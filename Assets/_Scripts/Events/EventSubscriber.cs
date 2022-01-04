using System;
using UnityEngine;
using UnityEngine.Events;

public class EventSubscriber : MonoBehaviour
{
    [SerializeField] GameEvent gameEvent;
    [SerializeField] UnityEvent unityEvent;
    public delegate void MyActionDelegate<T1, T2>(T1 t1, T2 t2);
    

    private void Awake() => gameEvent.AddSubscriber(this);
    private void OnDestroy() => gameEvent.RemoveSubscriber(this);

    public void RaiseEvent() => unityEvent.Invoke();


}
