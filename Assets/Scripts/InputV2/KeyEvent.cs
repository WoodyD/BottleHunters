using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyEvent {
	public System.Action OnKeyEvent;

	public void GetKeyEvent (KeyCode key) {
		if (Input.GetKeyDown (key))
			OnKeyEvent ();
	}
}
