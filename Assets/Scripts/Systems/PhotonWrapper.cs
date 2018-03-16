﻿using UnityEngine;

public class PhotonWrapper : Photon.PunBehaviour {
	
	public static string roomName = "TestRoom";
	public bool showRoomInfo;
	public bool IsConnected { get; private set; }

	public bool IsControllerByThisPlayer { get { return PhotonNetwork.player.IsLocal; } }

	public void TryToConnect () {
		IsConnected = false;
        PhotonNetwork.ConnectUsingSettings("0.0.1");
	}

#region Photon events
	
	public override void OnConnectedToPhoton(){
        ShowPhotonLogInMenu("[PHTN] Connected to photon server.");
    }
	
    public override void OnConnectedToMaster(){
		ShowPhotonLogInMenu("[PHTN] Connected to photon master server. Try to create room: " + roomName);
		PhotonNetwork.CreateRoom(roomName);
    }
	
    public override void OnPhotonCreateRoomFailed(object[] codeAndMsg){
        ShowPhotonLogInMenu("[PHTN] Failed to create room. Error msg: " + codeAndMsg[1]);
        ShowPhotonLogInMenu("[PHTN] Assume that room already created. Try to join the room: " + roomName);
		GameSystemsController.Instance.sceneChanger.LoadScene(Scenes.TestScene, true);
        //PhotonNetwork.JoinRoom(roomName);
    }
	
	public override void OnJoinedRoom(){
		ShowPhotonLogInMenu("[PHTN] Joined the room: " + roomName);
		IsConnected = true;
		if (showRoomInfo)
			UpdateRoomInfo();
		if(PhotonNetwork.player.ID>1)
			GameSystemsController.Instance.player.SpawnPlayer();
	}
	
	public override void OnPhotonJoinRoomFailed(object[] codeAndMsg){
        ShowPhotonLogInMenu("[PHTN] Failed to join room. Error msg: " + codeAndMsg[1]);
		IsConnected = false;
    }
	
	public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer){
		Debug.Log("[PHTN] New player connected to this room. ID: " + newPlayer.ID);
		UpdateRoomInfo();
	}

	public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer) {
		Debug.Log("[PHTN] Player disconnected. ID: " + otherPlayer.ID);
		UpdateRoomInfo();
	}
	
#endregion
	
	private void ShowPhotonLogInMenu(string log){
		if(!MenuUIController.IsNull)
			MenuUIController.Instance.ShowPhotonLog(log);
	}
	
	private void UpdateRoomInfo() {
		string roomInfo = "Room: " + roomName;
		roomInfo += "\nCount of players: " + PhotonNetwork.playerList.Length;
		roomInfo += "\nPlayer ID: " + PhotonNetwork.player.ID;
		GameSystemsController.Instance.uiManager.SetLeftCornerText(roomInfo);
	}
}
