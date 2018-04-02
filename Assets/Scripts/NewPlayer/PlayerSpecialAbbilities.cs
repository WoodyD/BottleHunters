using UnityEngine;

public class PlayerSpecialAbbilities : Photon.MonoBehaviour {
	public ParticleSystem burp;
	public bool isControllable = true;

	private JoyButton joyButtonBurp;
	private bool isJoystickEnable;
	
	private void Awake(){
		//bad way to implement. only for tests
		joyButtonBurp = FindObjectOfType<JoyButton>();
		isJoystickEnable = joyButtonBurp && joyButtonBurp.gameObject.activeInHierarchy;
	}
	
	private void Update () {
		if (isControllable)
			if (GetBurpButton())
				TryBurp();
	}

	private bool GetBurpButton() {
		if (Input.GetKeyDown(KeyCode.E) || (isJoystickEnable && joyButtonBurp.Pressed))
			return true;
		else
			return false;
	}
	
	private void TryBurp() {
		if (burp && !burp.isPlaying) {
			this.photonView.RPC("MakeBurp", PhotonTargets.All);
		}
	}
	
	[PunRPC]
	private void MakeBurp() {
		burp.Play();
	}
	
}

