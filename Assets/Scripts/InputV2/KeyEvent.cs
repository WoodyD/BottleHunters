using System;
using UnityEngine;

public class KeyEvent /*: IControllerEvent<ControllerSet>*/ {
    
    private event Action OnKeyEvent;
    private KeyCode key;

    public KeyEvent(Action EventToAdd, KeyCode key){
        OnKeyEvent += EventToAdd;
        this.key = key;
    }

    public void CheckEvent() {
        if(Input.GetKeyDown(key)){
            OnKeyEvent();
        }
    }
}
