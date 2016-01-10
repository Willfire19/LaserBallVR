using UnityEngine;
using System.Collections;

public class NetworkedPlayer : Photon.MonoBehaviour
{
	public GameObject avatar;

	public GameObject avatarBody;
	public GameObject avatarHead;

	public Transform playerBody;
	public Transform playerHead;

	void Start ()
	{
		Debug.Log("i'm instantiated");

		if (photonView.isMine)
		{
			Debug.Log("player is mine");

			playerBody = GameObject.Find("Player").transform.Find("Player_Body");
			//playerLocal = playerGlobal.Find("Main Camera");

			this.transform.SetParent(playerBody);
			this.transform.localPosition = Vector3.zero;

			avatar.SetActive (false);
			avatarBody.SetActive(false);
			avatarHead.SetActive(false);
		}
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(playerBody.position);
			stream.SendNext(playerBody.rotation);
			//stream.SendNext(playerLocal.localPosition);
			//stream.SendNext(playerLocal.localRotation);
		}
		else
		{
			//this.transform.position = (Vector3)stream.ReceiveNext();
			//this.transform.rotation = (Quaternion)stream.ReceiveNext();
			avatar.transform.localPosition = (Vector3)stream.ReceiveNext();
			avatar.transform.localRotation = (Quaternion)stream.ReceiveNext();
		}
	}
}