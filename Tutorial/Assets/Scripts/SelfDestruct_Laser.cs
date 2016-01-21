using UnityEngine;
using System.Collections;

public class SelfDestruct_Laser : MonoBehaviour {
	
	public float diration = 5.0f;
	private float timeTaken = 0.0f;
	private LineRenderer laser;
	private Color laserColor;

	private Vector3 original;
	private Vector3 original_dest;
	private Vector3 destination;
	private Vector3 slope;
	private Vector3 slopeNorm;

	private bool pointsClose = false;
	//public float laserSpeed = 1000000.0f;

	void Start () {

		laser = transform.GetComponent<LineRenderer> ();
		laserColor = laser.material.GetColor ("_TintColor");
		//slope = destination - origin;
		Debug.Log("Original: " + this.original);
		Debug.Log("Destination: " + this.destination);
		Debug.Log("Slope: " + this.slope);

	}

	// Update is called once per frame
	void Update () {
		diration -= Time.deltaTime;
		timeTaken += Time.deltaTime;

		if ( !(Vector3.Distance (original_dest, destination) < 0.1f) ) {
			//destination += (Time.deltaTime * slope);
			destination += 500.0f * (Time.deltaTime * slopeNorm);
			//original += (Time.deltaTime * slope);
			//original -= slope;
			//laser.SetPosition (0, original);
			laser.SetPosition (1, destination);
		} else {
			if (pointsClose != true) {
				Debug.Log ("Time Taken: " + timeTaken);
				pointsClose = true;
			}
		}


		laserColor.a = -((float).5 / diration) + (float).5;
		laser.material.SetColor ("_TintColor", laserColor);

		if (diration <= 0) {
			Destroy (gameObject);
		}
	}

	public void SetEndPoints ( Vector3 endpoint1, Vector3 endpoint2 ) {
		this.original = endpoint1;
		this.destination = endpoint1;
		this.original_dest = endpoint2;
		this.slope = endpoint2 - endpoint1;
		this.slopeNorm = this.slope.normalized;

		//this.destination = this.original + (.20f * this.slope);
	}
}