using UnityEngine;

public class DontShowComponentOnPC : MonoBehaviour {

#if UNITY_STANDALONE
	void Awake() {
		gameObject.SetActive(false);
	}
#endif

}
