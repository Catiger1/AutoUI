using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandData
{
    public GameObject source;
    public Func<bool> condition;
    public Func<bool> removeCondition;
    
    public Action command;
    public bool isCommandExecute = false;
    public float CreateTime { get; set; }
    public bool DelayRemove { get; set; }
    public float Delay { get; set; }
    public bool ImmediatelyRemove { get; set; }
}
