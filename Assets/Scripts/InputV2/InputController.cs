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

    IControllerEvent<ControllerSet> someEvent;

    public void Initialize () {
        //someEvent = new KeyEvent();
	}

	public void Update () {
		verticalAxisEvent.GetAxisEvent ("Vertical");
		horizontalAxisEvent.GetAxisEvent ("Horizontal");
		//runEvent.GetKeyEvent (KeyCode.LeftShift);

	}

    private void GetKeyboardInputs(){
		verticalAxisEvent.GetAxisEvent("Vertical");
		horizontalAxisEvent.GetAxisEvent("Horizontal");
		//runEvent.GetKeyEvent(KeyCode.LeftShift);
    }
}
