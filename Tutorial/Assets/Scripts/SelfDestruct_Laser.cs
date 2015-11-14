using UnityEngine;
using System.Collections;

public class SelfDestruct_Laser : MonoBehaviour {
	
	public float diration = 5.0f;
	// Update is called once per frame
	void Update () {
		diration -= Time.deltaTime;
		if (diration <= 0) {
			Destroy (gameObject);
		}
	}
}