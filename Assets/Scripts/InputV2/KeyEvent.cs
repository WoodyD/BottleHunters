using System;
using UnityEngine;

public class KeyEvent : IControllerEvent<ControllerSet> {
    
    public event Action ControllerEvent;

    ControllerSet set = new ControllerSet();

    public KeyEvent(ControllerSet set){
        this.set = set;
    }

    public void GetEvent(ControllerSet controllerSet) {
        
    }
}
