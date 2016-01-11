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
}