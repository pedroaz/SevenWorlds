using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

internal class MainThreadHelperService : GameService<MainThreadHelperService>
{
    private Queue<Action> jobs = new Queue<Action>();

    void Awake()
    {
        Object = this;
    }

    void Update()
    {
        while (jobs.Count > 0)
            jobs.Dequeue().Invoke();
    }

    public static void AddJob(Action job)
    {
        Object.jobs.Enqueue(job);
    }
}