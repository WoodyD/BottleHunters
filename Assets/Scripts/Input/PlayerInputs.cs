using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControllerType{Keyboard, Pad}

public class PlayerInputs : MonoBehaviour {

    public System.Action<float> HorizontalAxisEvent;
    public System.Action<float> VerticalAxisEvent;
    public System.Action<bool> RunEvent;
	public System.Action<bool> JumpEvent;
	public System.Action<bool> CrouchEvent;

	public int ControllerID { get; internal set; }

    ControllerType Controller;

    public void InitializePlayerInputs(int playerID){
        ControllerID = playerID;
        if (ControllerID == 0) //TODO: ATM assume that player 0 play on keyboard
            Controller = ControllerType.Keyboard;
        else
            Controller = ControllerType.Pad;
    }

    void Update(){
        if (Controller == ControllerType.Keyboard)
            GetKeyboadInputs();
        else if (Controller == ControllerType.Pad)
            GetPadInputs();
    }

    void GetKeyboadInputs(){
		HorizontalAxisEvent(Input.GetAxis("XAxisKeyboard"));
		VerticalAxisEvent(Input.GetAxis("YAxisKeyboard"));
		RunEvent (GetKeyEvent (KeyCode.LeftShift));
		JumpEvent (GetKeyEvent (KeyCode.Space));
		CrouchEvent (GetKeyEvent (KeyCode.LeftControl));
    }

    void GetPadInputs(){
        HorizontalAxisEvent(Input.GetAxis("XAxisPad" + ControllerID));
        VerticalAxisEvent(Input.GetAxis("YAxisPad" + ControllerID));
		RunEvent (GetPadRunEvent ());
		JumpEvent (GetPadJumpEvent ());
    }

	bool GetKeyEvent (KeyCode code) {
		if (Input.GetKey (code))
			return true;
		else
			return false;
	}

	bool GetPadRunEvent () {
		float runTrigger = Input.GetAxis ("RunAxisPad" + ControllerID);
		if (runTrigger > 0.2)
			return true;
		else
			return false;
	}

	private bool GetPadJumpEvent () {
		throw new NotImplementedException ();
	}
}
