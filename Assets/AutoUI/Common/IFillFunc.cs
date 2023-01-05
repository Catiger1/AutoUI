using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFillFunc<T>
{
    void Add(T func);
    void Clear();
}
