using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisEvent {
	
    private System.Action<float> OnAxisEvent;
    private string axisName;
	private float inputDeadZone = 0.2f;
	private float axisInput;
    private bool onlyPositive;
    private bool onlyNegative;

    public AxisEvent(System.Action<float> EventToAdd, string axisName, bool onlyPositive = false, bool onlyNegative = false){
        OnAxisEvent += EventToAdd;
        this.axisName = axisName;
        this.onlyPositive = onlyPositive;
        this.onlyNegative = onlyNegative;
    }

	public void GetEvent () {
		axisInput = Input.GetAxis (axisName);
		if (!onlyPositive && !onlyNegative) {
			if (axisInput < -inputDeadZone || axisInput > inputDeadZone)
				OnAxisEvent (axisInput);
		} else if (onlyPositive) {
			if (axisInput > inputDeadZone)
				OnAxisEvent (axisInput);
		} else if (onlyNegative) {
			if (axisInput < -inputDeadZone)
				OnAxisEvent (axisInput);
		}
	}
}
