using UnityEngine;

public class PlayerSpecialAbbilities : Photon.MonoBehaviour {
	public ParticleSystem burp;
	public bool isControllable = true;
	//public bool startBurp;

	private JoyButton joyButtonBurp;
	
	private void Awake(){
		//bad way to implement. only for tests
		joyButtonBurp = FindObjectOfType<JoyButton>();
	}
	
	private void Update () {
		if (isControllable)
			if (GetBurpButton())
				TryBurp();
	}

	private bool GetBurpButton() {
		if (Input.GetKeyDown(KeyCode.E) || joyButtonBurp.Pressed)
			return true;
		else
			return false;
	}
	
	private void TryBurp() {
		if (burp && !burp.isPlaying) {
			this.photonView.RPC("MakeBurp", PhotonTargets.All);
			//startBurp = true;
			//MakeBurp();
		}
	}
	
	[PunRPC]
	private void MakeBurp() {
		//startBurp = false;
		burp.Play();
	}
	
}

