using UnityEngine;

public class DontShowComponentOnPC : MonoBehaviour {

	void Awake() {
#if UNITY_STANDALONE
		gameObject.SetActive(false);
#else
		gameObject.SetActive(true);
#endif
	}
}
