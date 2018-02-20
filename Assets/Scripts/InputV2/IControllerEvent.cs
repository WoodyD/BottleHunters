using System;
using UnityEngine;

public interface IControllerEvent<ControllerSet> {
    
    event Action ControllerEvent;
    //new ControllerSet();
    void GetEvent(ControllerSet controllerSet);
}

public class ControllerSet{
    public KeyCode ?key;
    public String axisName;
    public bool ?onlyPositive;
    public bool ?onlyNegative;
}