using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEventManager<T> where T : IConvertible
{
    void Register(T key,Action<object> onEvent);

    void UnRegister(T key,Action<object> onEvent);

    void UnRegisterAll();
}
