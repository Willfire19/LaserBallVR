using UnityEngine;
using System.Collections;

public class NetworkController : MonoBehaviour
{
	public GameObject playerPrefab;
	public GameObject player;
	public GameObject networkedPlayer;
	public float respawnTimer = 0;

	string _room = "Tutorial_Room";

	void Start()
	{
		PhotonNetwork.ConnectUsingSettings("0.1");
		//player = GameObject.FindGameObjectWithTag ("Player");
		player = (GameObject)Instantiate(playerPrefab, new Vector3(0, 4, 0), Quaternion.identity);
//		player = Resources.Load("Player", typeof(GameObject)) as GameObject;
//		Debug.Log (player);
//		if(player == null){
//			Debug.Log (player);
//		}
		//player.transform.position = new Vector3 (0, 4, 0);
	}

	void Update() {
		if( respawnTimer > 0 ){
			respawnTimer -= Time.deltaTime;

			if (respawnTimer <= 0) {
				// Time to respawn the player!
				Debug.Log ("Time to respawn player!");
				//Instantiate (player, Vector3.zero, Quaternion.identity);
				player.GetComponent<HasHealth> ().Heal (100f);
//				player.transform.position = Vector3.zero;
				player.transform.position = new Vector3(0, 4, 0);
				player.GetComponent<FirstPersonController> ().Enable ();
				player.GetComponent<FirstPersonController> ().isDead = false;
				networkedPlayer.SetActive(true);
				//this.GetComponent<PhotonView>().RPC ("RespawnNetworkedPlayer", PhotonTargets.All);
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
		networkedPlayer = PhotonNetwork.Instantiate("NetworkedPlayer", Vector3.zero, Quaternion.identity, 0);
	}

	[PunRPC]
	void RespawnNetworkedPlayer() {
		networkedPlayer.SetActive (true);
	}
}