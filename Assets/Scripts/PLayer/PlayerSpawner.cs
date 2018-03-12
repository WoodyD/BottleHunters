using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour{

	public GameObject player;
	public GameObject playerPrefab;

	public void SpawnPlayer () {
		//player = PhotonNetwork.Instantiate ("ZombieCop", new Vector3 (5, 5, 5), Quaternion.identity, 1, new object[] {(int)PunTeams.Team.red });
		Instantiate (playerPrefab, new Vector3 (5, 5, 5), Quaternion.identity);
	}

}
