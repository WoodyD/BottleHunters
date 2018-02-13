﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControllerType{Keyboard, Pad}

public class PlayerInputs : MonoBehaviour {

    public System.Action<float> HorizontalAxisEvent;
    public System.Action<float> VerticalAxisEvent;
    public System.Action<bool> RunEvent;

	public int ControllerID { get; internal set; }

    ControllerType Controller;

    public PlayerInputs(int playerID){
        ControllerID = playerID;
        if (playerID == 0) //TODO: ATM assume that player 0 play on keyboard
            Controller = ControllerType.Keyboard;
        else
        {
            Controller = ControllerType.Pad;

        }
    }

    void Update(){
        if (Controller == ControllerType.Keyboard)
            GetKeyboadInputs();
        else if (Controller == ControllerType.Pad)
            GetPadInputs();
    }

    void GetKeyboadInputs(){
		HorizontalAxisEvent(Input.GetAxis("Horizontal"));
		VerticalAxisEvent(Input.GetAxis("Vertical"));
		if (Input.GetKeyDown(KeyCode.LeftShift))
			RunEvent(true);
		if (Input.GetKeyUp(KeyCode.LeftShift))
			RunEvent(false);
    }

    void GetPadInputs(){
        
    }
}
