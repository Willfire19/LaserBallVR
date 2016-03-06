using UnityEngine;
using System.Collections;

public class HasHealth : MonoBehaviour {

	public float hitPoints = 100f;

	[PunRPC]
	public void RecieveDamage( float hit_amount ){
		hitPoints -= hit_amount;
		if (gameObject.CompareTag("NetworkedPlayer")) {
			Debug.Log ("NeworkedPlayer is taking damage!");
			if( GetComponent<PhotonView>().isMine ){
				GameObject.FindGameObjectWithTag ("Player").GetComponent<HasHealth> ().RecieveDamage (hit_amount);
			}
		}
		if( hitPoints <= 0 ){
			Die();
		}
	}

	public void Die(){

		// Game crashes when Player dies. The Player gameObject gets destroyed.

		// DeathAnimation()
		Debug.Log (gameObject.name + " died!");
		// Check if Photon Network Instantiated this object
		if (GetComponent<PhotonView> ().instantiationId == 0) {			
			if (!gameObject.CompareTag ("Player")) {
				Destroy (gameObject);
			}
			else {
				Debug.Log( "Player died!");
			}
		} else {
			// This means that the NetworkedPlayer is dead
			// PhotonNetwork.Destroy( GetComponent<PhotonView>() );

			// Check if the dead person is the player
			if (gameObject.CompareTag ("Player")) {
				Debug.Log ("Player died!");
				gameObject.GetComponent<FirstPersonController> ().Die ();
			} else {				
				gameObject.active = false;
			}
		} 

	}
}
