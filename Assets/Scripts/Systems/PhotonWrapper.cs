using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonWrapper : Photon.PunBehaviour {

    private string roomName = "TestRoom";

	public void TryToConnect () {
        PhotonNetwork.ConnectUsingSettings("0.0.1");
	}

    public override void OnConnectedToPhoton(){
        Debug.Log("[FTN] Connected to photon server.");
    }

    public override void OnConnectedToMaster(){
		Debug.Log("[FTN] Connected to photon master server. Try to create room: " + roomName);
		PhotonNetwork.CreateRoom(roomName);
    }

    public override void OnPhotonCreateRoomFailed(object[] codeAndMsg){
        Debug.Log("[FTN] Failed to create room. Error msg: " + codeAndMsg[1]);
        Debug.Log("[FTN] Assume that room already created. Try to join the room: " + roomName);
        PhotonNetwork.JoinRoom(roomName);
    }

	public override void OnJoinedRoom(){
		Debug.Log("[FTN] Joined the room: " + roomName);
	}

    public override void OnPhotonJoinRoomFailed(object[] codeAndMsg){
        Debug.Log("[FTN] Failed to join room. Error msg: " + codeAndMsg[1]);
    }
}
