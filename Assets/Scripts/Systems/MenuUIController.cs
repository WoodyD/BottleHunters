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
		SetControlListValue();
		
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
	
	private void SetControlListValue(){
		selectControlList.options.Clear();
		selectControlList.options.Add(new Dropdown.OptionData() { text = ControllerType.Keyboard.ToString() });
		selectControlList.options.Add(new Dropdown.OptionData() { text = ControllerType.Pad.ToString() });
		selectControlList.RefreshShownValue();
		playerController = selectControlList.captionText.text;
	}
	
	private void OnDropdownValueChange(Dropdown ddValue){
		playerController = ddValue.captionText.text;
	}
	
	private void OnConnectbuttonClick() {
		GameSystemsController.Instance.photon.TryToConnect();
	}
	
	private void OnJoinGameButtonClick() {
		if (GameSystemsController.Instance.photon.IsConnected) {
			GameSystemsController.Instance.player.SetPlayerController(playerController);
			GameSystemsController.Instance.sceneChanger.LoadScene(Scenes.TestScene);
		}
	}
}
