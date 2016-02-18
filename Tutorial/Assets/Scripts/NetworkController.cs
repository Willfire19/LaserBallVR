using UnityEngine;
using System.Collections;

public class NetworkController : MonoBehaviour
{
	public GameObject player;
	public float respawnTimer = 0;

	string _room = "Tutorial_Room";

	void Start()
	{
		PhotonNetwork.ConnectUsingSettings("0.1");
	}

	void Update() {
		if( respawnTimer > 0 ){
			respawnTimer -= Time.deltaTime;

			if (respawnTimer <= 0) {
				// Time to respawn the player!
				Debug.Log ("Time to respawn player!");
				//Instantiate (player, Vector3.zero, Quaternion.identity);
				player.transform.position = Vector3.zero;
//				PhotonNetwork.Instantiate("NetworkedPlayer", Vector3.zero, Quaternion.identity, 0);
			}
		}
	}

	void OnJoinedLobby()
	{
		Debug.Log("joined lobby");

		RoomOptions roomOptions = new RoomOptions() { };
		PhotonNetwork.JoinOrCreateRoom(_room, roomOptions, TypedLobby.Default);
	}

	void OnJoinedRoom()
	{
		PhotonNetwork.Instantiate("NetworkedPlayer", Vector3.zero, Quaternion.identity, 0);
	}
}