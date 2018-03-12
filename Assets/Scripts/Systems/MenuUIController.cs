using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIController : MonoBehaviourSingleton<MenuUIController> {

	public Dropdown selectControlList;
	public Button connectButton;
	public Button joinGameButton;
	public Text photonLog;
	
	private string playerController;
	
	private void Start(){
		playerController = selectControlList.captionText.ToString();
		selectControlList.onValueChanged.AddListener(delegate {
			OnDropdownValueChange(selectControlList);
		});
		connectButton.onClick.AddListener(OnConnectbuttonClick);
		joinGameButton.onClick.AddListener(OnJoinGameButtonClick);
	}

	public void ShowPhotonLog(string log){
		photonLog.text += "\n";
		photonLog.text += log;
	}
	
	private void OnDropdownValueChange(Dropdown ddValue){
		playerController = ddValue.captionText.ToString();
	}
	
	private void OnConnectbuttonClick() {
		GameSystemsController.Instance.photon.TryToConnect();
	}
	
	private void OnJoinGameButtonClick() {
		if (GameSystemsController.Instance.photon.IsConnected)
			GameSystemsController.Instance.sceneChanger.LoadScene(Scenes.TestScene);
	}
}
