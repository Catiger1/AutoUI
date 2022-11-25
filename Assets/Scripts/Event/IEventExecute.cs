using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEventExecute<T> where T : EventData
{
    public void ExecuteEvent(Action<T> events);
}
