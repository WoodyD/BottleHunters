using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes{IntroTestScene, TestScene}

public class SceneChanger : MonoBehaviour {

	private bool gameCreated;
	
	public void LoadScene(Scenes nextScene, bool joinGame = false){
		gameCreated = joinGame;
		SceneManager.LoadScene(nextScene.ToString());
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnSceneLoaded (Scene arg0, LoadSceneMode arg1) {		
		if(gameCreated)
			PhotonNetwork.JoinRoom(PhotonWrapper.roomName);
		else
			GameSystemsController.Instance.player.SpawnPlayer ();
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
}
