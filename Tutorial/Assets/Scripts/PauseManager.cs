using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseManager : MonoBehaviour {

	GameObject player;
	GameObject pausePanel;

	public bool isPaused;

	// Use this for initialization
	void Start () {
		player = gameObject;
		pausePanel = GameObject.Find ("Player_Body/PauseCanvas");
		isPaused = false;
	
	}
	
	// Update is called once per frame
	void Update () {

		if (isPaused) {
			PauseGame (true);
		} else {
			PauseGame (false);
		}

		if (Input.GetButtonDown("Cancel") ) {
			Debug.Log ("Cancel Key is pressed");
			switchPause();
		}
	
	}

	void PauseGame(bool state) {
		pausePanel.SetActive (state);
		player.GetComponent<FirstPersonController> ().Pause (state);

		if (state) {			
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		} else {			
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}

	public void switchPause() {
		isPaused = !isPaused;
	}

	public void QuitGameClicked() {
		Debug.Log("Quit Game Button Clicked");
		Application.Quit ();
	}
}