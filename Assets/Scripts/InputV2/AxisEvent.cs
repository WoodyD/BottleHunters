using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisEvent {
	public System.Action<float> OnAxisEvent;
	public float inputDeadZone = 0.2f;

	private float axisInput;
	public void GetAxisEvent (string axisName, bool onlyPositive = false, bool onlyNegative = false) {
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
