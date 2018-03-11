using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonWrapper : Photon.PunBehaviour {
	
	public string roomName = "TestRoom";
	public bool IsConnected { get; private set; }
	
	public void TryToConnect () {
		IsConnected = false;
        PhotonNetwork.ConnectUsingSettings("0.0.1");
	}

    public override void OnConnectedToPhoton(){
        UIController.Instance.ShowPhotonLog("[FTN] Connected to photon server.");
    }

    public override void OnConnectedToMaster(){
		UIController.Instance.ShowPhotonLog("[FTN] Connected to photon master server. Try to create room: " + roomName);
		PhotonNetwork.CreateRoom(roomName);
    }

    public override void OnPhotonCreateRoomFailed(object[] codeAndMsg){
        UIController.Instance.ShowPhotonLog("[FTN] Failed to create room. Error msg: " + codeAndMsg[1]);
        UIController.Instance.ShowPhotonLog("[FTN] Assume that room already created. Try to join the room: " + roomName);
        PhotonNetwork.JoinRoom(roomName);
    }

	public override void OnJoinedRoom(){
		UIController.Instance.ShowPhotonLog("[FTN] Joined the room: " + roomName);
		IsConnected = true;
	}

    public override void OnPhotonJoinRoomFailed(object[] codeAndMsg){
        UIController.Instance.ShowPhotonLog("[FTN] Failed to join room. Error msg: " + codeAndMsg[1]);
		IsConnected = false;
    }
}
