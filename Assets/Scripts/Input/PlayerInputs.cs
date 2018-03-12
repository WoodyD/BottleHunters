using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControllerType { Keyboard, Pad }

public class PlayerInputs : MonoBehaviour {

    public System.Action<float> HorizontalAxisEvent;
    public System.Action<float> VerticalAxisEvent;
	public System.Action<float> verticalCameraEvent;
	public System.Action<float> horizontalCameraEvent;
	public System.Action<bool> RunEvent;
    public System.Action<bool> JumpEvent;
    public System.Action<bool> CrouchEvent;
	public System.Action BurpEvent;

    public int ControllerID { get; internal set; }

    ControllerType Controller;

    bool initialized;
	public void InitializePlayerInputs(ControllerType controller) {
        initialized = true;
		Controller = controller;
		ControllerID = PhotonNetwork.player.ID;
    }

    void Update() {
        if (initialized) {
            if (Controller == ControllerType.Keyboard)
                GetKeyboadInputs();
            else if (Controller == ControllerType.Pad)
                GetPadInputs();
        }
    }

    void GetKeyboadInputs() {
        HorizontalAxisEvent(Input.GetAxis("XAxisKeyboard"));
        VerticalAxisEvent(Input.GetAxis("YAxisKeyboard"));

        RunEvent(GetBoolKeyEvent (KeyCode.LeftShift));
        JumpEvent(GetBoolKeyEvent (KeyCode.Space));
        CrouchEvent(GetBoolKeyEvent (KeyCode.LeftControl));
		GetKeyEvent (BurpEvent, KeyCode.E);

	}

    void GetPadInputs() {
        HorizontalAxisEvent(Input.GetAxis("XAxisPad" + ControllerID));
        VerticalAxisEvent(Input.GetAxis("YAxisPad" + ControllerID));
        RunEvent(GetPadRunEvent());
        //JumpEvent (GetPadJumpEvent ());
    }

	void GetKeyEvent (Action action, KeyCode key) {
		if(Input.GetKeyDown(key))
			action ();
	}

	bool GetBoolKeyEvent(KeyCode code) {
        if (Input.GetKey(code))
            return true;
        else
            return false;
    }

    bool GetPadRunEvent() {
        float runTrigger = Input.GetAxis("RunAxisPad" + ControllerID);
        if (runTrigger > 0.2)
            return true;
        else
            return false;
    }

    private bool GetPadJumpEvent() {
        throw new NotImplementedException();
    }
}
