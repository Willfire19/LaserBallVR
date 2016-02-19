using UnityEngine;
using System.Collections;

public class HasHealth : MonoBehaviour {

	public float hitPoints = 100f;

	[PunRPC]
	public void RecieveDamage( float hit_amount ){
		hitPoints -= hit_amount;
		if( hitPoints <= 0 ){
			Die();
		}
	}

	public void Die(){
		// DeathAnimation()
		Destroy (gameObject);
	}
}
