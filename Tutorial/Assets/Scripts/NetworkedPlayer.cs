using UnityEngine;
using System.Collections;

public class NetworkedPlayer : Photon.MonoBehaviour
{
	//public GameObject avatar;

	public GameObject avatarBody;
	public GameObject avatarHead;

	public Transform player;

	public Transform playerBody;
	public Transform playerHead;

	// Next fields used for respawn
	private float respawnTimer = 0f;
	//private bool isDead = false;

	void Start ()
	{
		Debug.Log("i'm instantiated");

		if (photonView.isMine)
		{
			Debug.Log("player is mine");

			player = GameObject.Find ("Player").transform;
			playerBody = player.Find("Player_Body");
			playerHead = player.Find("Main Camera");

			this.transform.SetParent(player);
			this.transform.localPosition = Vector3.zero;

			//avatar.SetActive (false);
			avatarBody.SetActive(false);
			avatarHead.SetActive(false);
		}
	}

	void Update() {
		if( respawnTimer > 0 ){
			respawnTimer -= Time.deltaTime;

			if (respawnTimer <= 0) {
				// Time to respawn the player!
				Debug.Log ("Time to respawn Networked player!");
				gameObject.GetComponent<HasHealth> ().Heal (100f);
				Enable ();
			}
		}
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(player.position);
			stream.SendNext(player.rotation);
			stream.SendNext(playerBody.localPosition);
			stream.SendNext(playerBody.localRotation);
			stream.SendNext(playerHead.localPosition);
			stream.SendNext(playerHead.localRotation);
		}
		else
		{
			this.transform.position = (Vector3)stream.ReceiveNext();
			this.transform.rotation = (Quaternion)stream.ReceiveNext();

			avatarBody.transform.localPosition = (Vector3)stream.ReceiveNext();
			avatarBody.transform.localRotation = (Quaternion)stream.ReceiveNext();

			//avatar.transform.position = this.transform.position;
			//avatar.transform.rotation = this.transform.rotation;
			avatarHead.transform.localPosition = (Vector3)stream.ReceiveNext();
			//avatarHead.transform.localPosition = this.transform.position + new Vector3(0, 1, 0);
			avatarHead.transform.localRotation = (Quaternion)stream.ReceiveNext();
		}
	}

	public void Die() {
		Disable();
		respawnTimer = 3.2f;
	}

	void Enable(){
		if (!photonView.isMine) {
			avatarBody.SetActive (true);
			avatarHead.SetActive (true);
			gameObject.GetComponent<CapsuleCollider> ().enabled = true;
		}
	}

	void Disable(){
		if (!photonView.isMine) {
			avatarBody.SetActive (false);
			avatarHead.SetActive (false);
			gameObject.GetComponent<CapsuleCollider> ().enabled = false;
		}
	}
}