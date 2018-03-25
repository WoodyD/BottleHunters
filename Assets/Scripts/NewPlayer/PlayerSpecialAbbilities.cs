using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack{	
	public bool startBurp;
}

public class PlayerSpecialAbbilities : Photon.MonoBehaviour {
	public ParticleSystem burp;

	public bool isControllable = true;
	public Attack attackWithBurp;

	private void OnEnable() {
		PhotonNetwork.OnEventCall += this.CustomBurpEvent;
	}

	private void OnDisable() {
		PhotonNetwork.OnEventCall -= this.CustomBurpEvent;
	}
	
	private void Update () {
		if (isControllable)
			if (Input.GetKeyDown(KeyCode.E))
				OnStartBurp();
		//		attackWithBurp.TryBurp();
		//}else{
		//	Debug.Log("IsControllable by other player. Start burp? " + attackWithBurp.startBurp);
		//	if (attackWithBurp.startBurp) 
		//		attackWithBurp.MakeBurp();
			
		//}
	}

	private void OnStartBurp(){
		TryBurp();
		byte evCode = 0;
		bool reliable = true;
		PhotonNetwork.RaiseEvent(evCode, attackWithBurp, reliable, null);
	}
	
	private void CustomBurpEvent(byte eventCode, object content, int senderId) {
		if(eventCode == 0){
			attackWithBurp = content as Attack;
			if (attackWithBurp.startBurp)
				MakeBurp();
		}
	}
	
	public void TryBurp() {
		if (burp && !burp.isPlaying) {
			attackWithBurp.startBurp = true;
			MakeBurp();
		}
	}
	
	public void MakeBurp() {
		attackWithBurp.startBurp = false;
		burp.Play();
	}

	//public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
	//	if (stream.isWriting) {
	//		Debug.Log("Send bool: " + startBurp);
	//		stream.SendNext(startBurp);
	//	}else{
	//		startBurp = (bool)stream.ReceiveNext();
	//		Debug.Log("Receive bool: " + startBurp);
	//	}		
	//}
	
}

