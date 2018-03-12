using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

	public void SpawnPlayer () {
		PhotonNetwork.Instantiate ("ZombieCop", new Vector3 (5, 5, 5), Quaternion.identity, 1);
	}
}
