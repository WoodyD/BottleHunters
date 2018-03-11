using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes{IntroTestScene, TestScene}

public class SceneChanger : MonoBehaviour {	
	
	public void LoadScene(Scenes nextScene){
		SceneManager.LoadScene(nextScene.ToString());
	}
}
