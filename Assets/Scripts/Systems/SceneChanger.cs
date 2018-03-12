using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes{IntroTestScene, TestScene}

public class SceneChanger : MonoBehaviour {

	public void LoadScene(Scenes nextScene){
		SceneManager.LoadScene(nextScene.ToString());
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnSceneLoaded (Scene arg0, LoadSceneMode arg1) {
		GameSystemsController.Instance.player.SpawnPlayer ();
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
}
