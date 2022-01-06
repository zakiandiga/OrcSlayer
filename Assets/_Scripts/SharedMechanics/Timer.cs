using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private static List<Timer> activeTimers;
    private static GameObject initializedTimer;    

    private static void InitializationCheck()
    {
        if (initializedTimer == null)
        {
            initializedTimer = new GameObject("Timer_Initialized");
            activeTimers = new List<Timer>();
        }
    }

    public static Timer Create(Action action, float tick, string timerName = null)
    {
        InitializationCheck();

        GameObject timerObject = new GameObject("TimerObject_" + timerName, typeof(MonoBehaviourProxy));
        Timer timer = new Timer(action, tick, timerName, timerObject);
        timerObject.GetComponent<MonoBehaviourProxy>().onUpdate = timer.Update;
        activeTimers.Add(timer);
        return timer;
    }

    private static void RemoveTimer(Timer timer)
    {
        InitializationCheck();
        activeTimers.Remove(timer);
    }

    public static void ForceStopTimer(string timerName)
    {
        InitializationCheck();
        for (int i = 0; i<activeTimers.Count; ++i)
        {
            if(activeTimers[i].timerName == timerName)
            {
                activeTimers[i].RemoveTimer();
                i--;
            }
        }
    }

    public static bool TimerRunning(string timerName)
    {
        InitializationCheck();
        for (int i = 0; i < activeTimers.Count; ++i)
        {
            if (activeTimers[i].timerName == timerName)
            {
                return true;
            }            
        }

        return false;
    }

    //proxy to access monobehaviour functions (update())
    public class MonoBehaviourProxy: MonoBehaviour
    {
        public Action onUpdate;
        private void Update()
        {
            if (onUpdate != null)
                onUpdate();
        }
    }

    private Action action;
    private float tick;
    private string timerName;
    private GameObject timerObject;
    private bool isRemoved;

    private Timer (Action action, float tick, string timerName, GameObject timerObject)
    {
        this.action = action;
        this.tick = tick;
        this.timerName = timerName;
        this.timerObject = timerObject;
        isRemoved = false;
    }

    public void Update()
    {
        if(!isRemoved)
        {
            tick -= Time.deltaTime;
            if(tick < 0)
            {
                action();
                RemoveTimer();
            }

        }
    }

    private void RemoveTimer()
    {
        isRemoved = true;
        GameObject.Destroy(timerObject);
        RemoveTimer(this);
    }

}

