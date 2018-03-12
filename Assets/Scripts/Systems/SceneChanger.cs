using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes{IntroTestScene, TestScene}

public class SceneChanger : MonoBehaviour {	
	
	public void LoadScene(Scenes nextScene){
		SceneManager.LoadScene(nextScene.ToString());
		SceneManager.sceneLoaded += OnSceneloaded;
	}

	private void OnSceneloaded (Scene arg0, LoadSceneMode arg1) {
		if(arg0.ToString() == Scenes.TestScene.ToString())
			PhotonNetwork.Instantiate ("ZombieCop", new Vector3 (5, 5, 5), Quaternion.identity, 1);
		SceneManager.sceneLoaded -= OnSceneloaded;
	}
}
