using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public Dropdown selectControlList;
	public Button connectButton;

	public Text photonLog;
	private string playerController;
	
	private void Start(){
		playerController = selectControlList.captionText.ToString();
		selectControlList.onValueChanged.AddListener(delegate {
			OnDropdownValueChange(selectControlList);
		});
		connectButton.onClick.AddListener(OnConnectbuttonClick);
	}

	private void OnDropdownValueChange(Dropdown ddValue){
		playerController = ddValue.captionText.ToString();
	}
	
	private void OnConnectbuttonClick() {
		GameSystemsController.Instance.photon.TryToConnect();
	}
}
