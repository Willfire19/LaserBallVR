using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour {

	public float diration = 1.0f;
	// Update is called once per frame
	void Update () {
		diration -= Time.deltaTime;
		if (diration <= 0) {
			Destroy (gameObject);
		}
	}
}
