using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour{

	public GameObject player;
	public GameObject playerPrefab;

	public void SpawnPlayer () {
		PhotonNetwork.Instantiate ("ZombieCop", new Vector3 (5, 0, 5), Quaternion.identity, 0);
		//Instantiate (playerPrefab, new Vector3 (5, 0, 5), Quaternion.identity, player.transform);
	}

}
