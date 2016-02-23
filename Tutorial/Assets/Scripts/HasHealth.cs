using UnityEngine;
using System.Collections;

public class HasHealth : MonoBehaviour {

	public float hitPoints = 100f;

	[PunRPC]
	public void RecieveDamage( float hit_amount ){
		Debug.Log ("Object was hit");
		hitPoints -= hit_amount;
		if (gameObject.CompareTag("NetworkedPlayer")) {
			Debug.Log ("NeworkedPlayer is taking damage!");
		}
		if( hitPoints <= 0 ){
			Die();
		}
	}

	public void Die(){
		// DeathAnimation()
		if (GetComponent<PhotonView>().instantiationId == 0) {
			Debug.Log (gameObject.name + " died!");
			if (!gameObject.CompareTag ("Player")) {
				Destroy (gameObject);
			}
		}
//		else {
//			if( 

	}
}
