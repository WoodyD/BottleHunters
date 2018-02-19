using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisAsKeyEvent{
	public System.Action<bool> OnAxisEvent;
	public float inputDeadZone = 0.2f;

	private float axisInput;
	public void GetAxisEvent (string axisName) {
		axisInput = Input.GetAxis (axisName);
		if (axisInput < -inputDeadZone || axisInput > inputDeadZone)
			OnAxisEvent (true);
	}
}
