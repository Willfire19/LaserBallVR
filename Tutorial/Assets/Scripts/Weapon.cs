using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	//public GameObject bullet_prefab;
	//public float bulletImpulse = 20f;
	//public float fireRate = 0.5f;
	public float accuracy = 0.0f;
	public float gunFireRate= 0.1f;
	private float gunCoolDownRemaining = 0;
	public float gunRange = 100.0f;
	public float gunDamage = 10.0f;
	//private float lastFire = 0.0f;
	
	public GameObject gunDebrisPrefab;
	public GameObject gunTrail;

	private AudioSource audioSrc;
	public AudioClip shootFX;

	// Use this for initialization
	void Start () {
		audioSrc = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		gunCoolDownRemaining -= Time.deltaTime;
			
	}

	public void Fire( Vector3 target ){

		
		if ( gunCoolDownRemaining <= 0 ) {
			audioSrc.PlayOneShot(shootFX);
			gunCoolDownRemaining = gunFireRate;
			//Ray gun_ray = new Ray (transform.position + transform.forward, transform.forward);
			Ray gun_ray = new Ray (transform.position + transform.forward, target - transform.position);
			RaycastHit hitInfo;
			
			if (Physics.Raycast (gun_ray, out hitInfo, gunRange)) {
				Vector3 hitPoint = hitInfo.point;
				GameObject gameHit = hitInfo.collider.gameObject;
				Debug.Log ("Enemy GameObject Hit: " + gameHit.name);
				Debug.Log ("Enemy Hit Point: " + hitPoint);
				
				if (gunDebrisPrefab != null) {
					Instantiate (gunDebrisPrefab, hitPoint, Quaternion.identity);
				}
				
				if (gunTrail != null){
					//gunTrail.GetComponent<LineRenderer>().SetPosition(0, transform.position + transform.forward);
					gunTrail.GetComponent<LineRenderer>().SetPosition(0, transform.position + transform.forward);
					gunTrail.GetComponent<LineRenderer>().SetPosition(1, hitPoint);
					Instantiate (gunTrail);
					
				}
				
				HasHealth hitObject = gameHit.GetComponent<HasHealth>();
				if(hitObject != null){
					hitObject.RecieveDamage(gunDamage);
				}
			}
		}

	}
}
