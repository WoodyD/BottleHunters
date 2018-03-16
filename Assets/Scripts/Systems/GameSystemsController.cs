using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystemsController : MonoBehaviourSingleton<GameSystemsController> {

	public SceneChanger sceneChanger;
	public PhotonWrapper photon;
	public UIManager uiManager;
	public Camera mainGameCamera;
	public PlayerSpawner player;

	void Start(){
		if (photon)
			photon.TryToConnect();
	}
}
