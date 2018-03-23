using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour{

	public ControllerType controller;
	
	public void SpawnPlayer () {
		PhotonNetwork.Instantiate ("ZombieCop", new Vector3 (5, 0, 5), Quaternion.identity, 0);		
	}

	public void SetPlayerController(string playerController) {
		try {
			controller = (ControllerType)Enum.Parse(typeof(ControllerType), playerController);
		}catch (Exception e) {
			Debug.LogError("Error in parsing enum. \n" + e);
			controller = ControllerType.Keyboard;
		}
	}
}
