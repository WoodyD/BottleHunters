using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialAbbilities : Photon.MonoBehaviour, IPunObservable {

	public bool isControllable = true;
	public ParticleSystem burp;
	public bool startBurp;

	private void Update () {
		if (isControllable) {
			if (Input.GetKeyDown(KeyCode.E))
				TryBurp();
		}else{
			if (startBurp)
				MakeBurp();
		}
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting) {
			stream.SendNext(startBurp);
		}else{
			startBurp = (bool)stream.ReceiveNext();
		}
		
	}
	private void TryBurp(){
		if (burp && !burp.isPlaying) {
			startBurp = true;
			MakeBurp();
		}
	}
	
	public void MakeBurp () {
		startBurp = false;
		burp.Play ();
	}

}

