using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisAsKeyEvent{
	public System.Action OnAxisEvent;
	public float inputDeadZone = 0.2f;

	private float axisInput;
	private void GetAxisEvent (string axisName, bool onlyPositive = false, bool onlyNegative = false) {
		axisInput = Input.GetAxis (axisName);
		if (!onlyPositive && !onlyNegative) {
			if (axisInput < -inputDeadZone || axisInput > inputDeadZone)
				OnAxisEvent ();
		} else if (onlyPositive) {
			if (axisInput > inputDeadZone)
				OnAxisEvent ();
		} else if (onlyNegative) {
			if (axisInput < -inputDeadZone)
				OnAxisEvent ();
		}
	}
}
