using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VolumeSlider : MonoBehaviour {

	Slider volumeSlider;

	// Use this for initialization
	void Start () {

		volumeSlider = transform.GetComponent<Slider> ();
		volumeSlider.normalizedValue = AudioListener.volume;

	}
	
	// Update is called once per frame
	//void Update () {
	//
	//}

	public void VolumeChange() {
		AudioListener.volume = volumeSlider.normalizedValue;
	}
}
