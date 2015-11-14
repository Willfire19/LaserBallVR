using UnityEngine;
using System.Collections;
using UnityEngine.VR;

public class FP_Shooting : MonoBehaviour {

	public GameObject bullet_prefab;
	public float bulletImpulse = 20f;
	public float fireRate = 0.5f;
	public float laserFireRate= 0.1f;
	public float laserCoolDownRemaining = 0;
	public float laserRange = 100.0f;
	public float laserDamage = 10.0f;
	private float lastFire = 0.0f;

	public GameObject laserGunTip;
	public GameObject laserDebrisPrefab;
	public GameObject laserTrail;

	private AudioSource audioSrc;
	public AudioClip shootFX;

	public bool rightEyeDominant = true;
	public Canvas shootCanvas;

	// Use this for initialization
	void Start () {
		audioSrc = GetComponent<AudioSource> ();
		laserGunTip = GameObject.Find ("GunTip");
		shootCanvas = GameObject.Find ("Canvas").GetComponent<Canvas> ();
		Debug.Log ("Canvas mode: " + shootCanvas.renderMode);

		if (VRDevice.isPresent) {
			foreach (Camera cam in Camera.allCameras) {
				Debug.Log ("Cameras: " + cam);
			}
			/*
			if (rightEyeDominant) {
				shootCanvas.worldCamera = VRNode.RightEye;
			} else {
				shootCanvas.worldCamera = VRNode.LeftEye;
			}
			*/
		}
	}
	
	// Update is called once per frame
	void Update () {
		laserCoolDownRemaining -= Time.deltaTime;

		if (Input.GetAxis ("Fire1") > 0.5 && laserCoolDownRemaining <= 0) {
			laserCoolDownRemaining = laserFireRate;
			Ray laser_ray = new Ray (Camera.main.transform.position + Camera.main.transform.forward, Camera.main.transform.forward);
			RaycastHit hitInfo;

			if (Physics.Raycast (laser_ray, out hitInfo, laserRange)) {
				audioSrc.PlayOneShot(shootFX);
				Vector3 hitPoint = hitInfo.point;
				GameObject gameHit = hitInfo.collider.gameObject;
				//Debug.Log ("GameObject Hit: " + gameHit.name);
				//Debug.Log ("Hit Point: " + hitPoint);

				if (laserDebrisPrefab != null) {
					Instantiate (laserDebrisPrefab, hitPoint, Quaternion.identity);
				}

				if (laserTrail != null){
					// laserTrail.GetComponent<LineRenderer>().SetPosition(0, Camera.main.transform.position + Camera.main.transform.forward);
					laserTrail.GetComponent<LineRenderer>().SetPosition(0, laserGunTip.transform.position);
					laserTrail.GetComponent<LineRenderer>().SetPosition(1, hitPoint);
					Instantiate (laserTrail);
					
				}

				HasHealth hitObject = gameHit.GetComponent<HasHealth>();
				if(hitObject != null){
					hitObject.RecieveDamage(laserDamage);
				}
			}
		}
	
		if (Input.GetButton("Fire2") && Time.time > fireRate + lastFire) {
			lastFire = Time.time;
			Fire ();
		}
	}

	// method to dictate firing
	void Fire () {
		Camera cam = Camera.main;
		GameObject theBullet = (GameObject)(Instantiate (bullet_prefab, cam.transform.position + cam.transform.forward, cam.transform.rotation));
		theBullet.GetComponent<Rigidbody>().AddForce (cam.transform.forward * bulletImpulse, ForceMode.Impulse);
	}

}
