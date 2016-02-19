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

	public Transform laserGunTip;
	public GameObject laserDebrisPrefab;
	public GameObject laserTrail;
    public GameObject laserCursor;
    private GameObject cursor;

	private AudioSource audioSrc;
	public AudioClip shootFX;

	public bool rightEyeDominant = true;
	public Canvas shootCanvas;

	private bool canFire = true;

	// Use this for initialization
	void Start () {

		audioSrc = GetComponent<AudioSource> ();
        // Does .Find find all GunTips, or just the one that is attached to the current player?
		//laserGunTip = GameObject.Find ("GunTip");
		//laserGunTip = GameObject.Find("Player").transform.Find("BitGun").transform.Find("GunTip");
		laserGunTip = transform.Find("Main Camera/Bit Gun/GunTip");
		//shootCanvas = GameObject.Find ("Canvas").GetComponent<Canvas> ();
        if (laserCursor != null) {
            cursor = (GameObject)Instantiate(laserCursor, new Vector3(0, 0, 0), Quaternion.identity);
            //laserCursor.transform.position = new Vector3(0, 0, 0);
        }
        //Debug.Log ("Canvas mode: " + shootCanvas.renderMode);

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
        Ray laser_sight = new Ray(Camera.main.transform.position + Camera.main.transform.forward, Camera.main.transform.forward);
        RaycastHit laser_aim;

        if (laserCursor != null)
        {
            if (Physics.Raycast(laser_sight, out laser_aim, laserRange))
            {
                cursor.transform.position = laser_aim.point;
            }
        }
        

		if (Input.GetAxis ("Fire1") > 0.5 && laserCoolDownRemaining <= 0) {
			Fire1 ();
		}
	
		if (Input.GetButton("Fire2") && Time.time > fireRate + lastFire) {
			lastFire = Time.time;
			ThrowGrenade ();
		}
	}

	// method to dictate grenade throwing
	void ThrowGrenade () {
		Camera cam = Camera.main;
		GameObject theBullet = (GameObject)(Instantiate (bullet_prefab, cam.transform.position + cam.transform.forward, cam.transform.rotation));
		theBullet.GetComponent<Rigidbody>().AddForce (cam.transform.forward * bulletImpulse, ForceMode.Impulse);
	}

	// method to dictate firing laser
	void Fire1 () {

		if (canFire) {
	
			laserCoolDownRemaining = laserFireRate;
			Ray laser_ray = new Ray (Camera.main.transform.position + Camera.main.transform.forward, Camera.main.transform.forward);
			RaycastHit hitInfo;

			if (Physics.Raycast (laser_ray, out hitInfo, laserRange)) {
				audioSrc.PlayOneShot (shootFX);
				Vector3 hitPoint = hitInfo.point;
				GameObject gameHit = hitInfo.collider.gameObject;
				//Debug.Log ("GameObject Hit: " + gameHit.name);
				//Debug.Log ("Hit Point: " + hitPoint);

				this.GetComponent<PhotonView>().RPC ("RenderFire", PhotonTargets.All, laserGunTip.position, hitPoint);

				HasHealth playerHit = gameHit.GetComponent<HasHealth> ();

				if (playerHit != null) {
					playerHit.GetComponent<PhotonView>().RPC ("RecieveDamage", PhotonTargets.All, laserDamage);
				}


			}

		}

	}

//	[PunRPC]
//	void DealDamage (float damage){
//		playerHit.RecieveDamage (damage);
//	}

	[PunRPC]
	void RenderFire ( Vector3 origin, Vector3 destination) {
		if (laserDebrisPrefab != null) {
			//Instantiate (laserDebrisPrefab, hitPoint, Quaternion.identity);
			Instantiate (laserDebrisPrefab, destination, Quaternion.identity);
		}

		if (laserTrail != null){
			// laserTrail.GetComponent<LineRenderer>().SetPosition(0, Camera.main.transform.position + Camera.main.transform.forward);
			laserTrail.GetComponent<LineRenderer>().SetVertexCount(2);
			laserTrail.GetComponent<LineRenderer>().SetPosition(0, origin);
			//laserTrail.GetComponent<LineRenderer>().SetPosition(1, hitPoint);
			laserTrail.GetComponent<LineRenderer>().SetPosition(1, destination);
			GameObject laserTrailInstance = Instantiate (laserTrail);
			laserTrailInstance.GetComponent<SelfDestruct_Laser> ().SetEndPoints (origin, destination);


		}
	}

	public void StopFire(bool paused){
		canFire = !paused;
	}
}
