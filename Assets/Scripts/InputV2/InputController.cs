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

    public InputController () {
        verticalAxisEvent = new AxisEvent(OnVerticalAxisEvent, "YAxisKeyboard");
        horizontalAxisEvent = new AxisEvent(OnhorizontalAxisEvent, "XAxisKeyboard");
        runEvent = new KeyEvent(OnRunEvent, KeyCode.LeftShift);
	}

	public void Update () {
        CheckKeyboardInputs();
	}

    void CheckKeyboardInputs(){
		verticalAxisEvent.CheckEvent ();
		horizontalAxisEvent.CheckEvent ();
		runEvent.CheckEvent();
    }

    public void OnVerticalAxisEvent(float input){
        
    }
	public void OnhorizontalAxisEvent(float input) {

	}
    public void OnRunEvent(){
        
    }
}
