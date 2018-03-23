using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialAbbilities : MonoBehaviour {

	public bool isControllable = true;
	public ParticleSystem burp;
	public bool startBurp;

	private void Update () {
		if (isControllable) {
			if (Input.GetKeyDown (KeyCode.E))
				if (burp && !burp.isPlaying)
					startBurp = true;
			if (startBurp) {
				MakeBurp ();
			}
		}
	}

	public void MakeBurp () {
		burp.Play ();
		startBurp = false;
	}
}

