using UnityEngine;

public class PhotonWrapper : Photon.PunBehaviour {
	
	public string roomName = "TestRoom";
	public bool showRoomInfo;
	public bool IsConnected { get; private set; }

	public PhotonView player;
	
	public void TryToConnect () {
		IsConnected = false;
        PhotonNetwork.ConnectUsingSettings("0.0.1");
	}
	
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
		if (stream.isWriting) {
			//We own this player: send the others our data 
			Debug.Log("My Input");
			//stream.SendNext((int)controllerScript._characterState);
			//stream.SendNext(transform.position);
			//stream.SendNext(transform.rotation);
		} else {
			//Network player, receive data 
			Debug.Log("Other player input");
			//controllerScript._characterState = (CharacterState)(int)stream.ReceiveNext();
			//correctPlayerPos = (Vector3)stream.ReceiveNext();
			//correctPlayerRot = (Quaternion)stream.ReceiveNext();
		}
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
        PhotonNetwork.JoinRoom(roomName);
    }
	
	public override void OnJoinedRoom(){
		ShowPhotonLogInMenu("[PHTN] Joined the room: " + roomName);
		IsConnected = true;
		if (showRoomInfo)
			UpdateRoomInfo();
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
