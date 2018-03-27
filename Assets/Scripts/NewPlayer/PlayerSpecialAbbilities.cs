using UnityEngine;

public class PlayerSpecialAbbilities : Photon.MonoBehaviour {
	public ParticleSystem burp;
	public bool isControllable = true;
	//public bool startBurp;
	
	private void Update () {
		if (isControllable)
			if (Input.GetKeyDown(KeyCode.E))
				TryBurp();
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

