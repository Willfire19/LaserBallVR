using UnityEngine;
using System.Collections;

public class SelfDestruct_Laser : MonoBehaviour {
	
	public float diration = 5.0f;
	private LineRenderer laser;
	private Color laserColor;

	private Vector3 destination;
	private Vector3 tempDest;
	private Vector3 slopeNorm;

	private bool laserReached = false;
	//public float laserSpeed = 1000000.0f;

	void Start () {

		laser = transform.GetComponent<LineRenderer> ();
		laserColor = laser.material.GetColor ("_TintColor");

	}

	// Update is called once per frame
	void Update () {
		diration -= Time.deltaTime;

		if (!laserReached) {
			if (!(Vector3.Distance (destination, tempDest) < 0.1f)) {
				tempDest += 500.0f * (Time.deltaTime * slopeNorm);
				laser.SetPosition (1, tempDest);
			} else {
				laserReached = true;
			}
		}
			
		laserColor.a = -((float).5 / diration) + (float).5;
		laser.material.SetColor ("_TintColor", laserColor);

		if (diration <= 0) {
			Destroy (gameObject);
		}
	}

	public void SetEndPoints ( Vector3 endpoint1, Vector3 endpoint2 ) {
		this.tempDest = endpoint1;
		this.destination = endpoint2;
		this.slopeNorm = Vector3.Normalize( (endpoint2 - endpoint1) );

	}
}