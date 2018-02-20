using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController{
	public AxisEvent verticalAxisEvent;
	public AxisEvent horizontalAxisEvent;
	public KeyEvent runEvent;
	public KeyEvent jumpEvent;
	public KeyEvent crouchEvent;

    public void Initialize () {
        verticalAxisEvent = new AxisEvent(OnVerticalAxisEvent, "YAxisKeyboard");
        horizontalAxisEvent = new AxisEvent(OnhorizontalAxisEvent, "XAxisKeyboard");
        runEvent = new KeyEvent(OnRunEvent, KeyCode.LeftShift);
	}

	public void Update () {
        GetKeyboardInputs();
	}

    private void GetKeyboardInputs(){
		verticalAxisEvent.GetEvent();
		horizontalAxisEvent.GetEvent();
		runEvent.CheckEvent();
    }

    void OnVerticalAxisEvent(float input){
        
    }
	void OnhorizontalAxisEvent(float input) {

	}
    void OnRunEvent(){
        
    }
}
