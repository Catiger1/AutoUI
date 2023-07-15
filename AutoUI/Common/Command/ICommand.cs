using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    void PushCommand(CommandData data);
    void UpdateCommand();
    void PopupCommand(GameObject go);
}