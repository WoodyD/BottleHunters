using System;
using UnityEngine;

public interface IControllerEvent<ControllerSet> {
    
    event Action ControllerEvent;
    void Initialize(ControllerSet controllerSet);
    void GetEvent();
}

public class ControllerSet{
    public KeyCode ?key;
    public String axisName;
    public bool ?onlyPositive;
    public bool ?onlyNegative;
}