using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseManager : MonoBehaviour {

	GameObject player;
	GameObject pausePanel;

	public bool isPaused;
	private bool prevPauseState;

	// Use this for initialization
	void Start () {
		player = gameObject;
		pausePanel = GameObject.Find ("Player_Body/PauseCanvas");
		prevPauseState = isPaused = false;
		PauseGame (false);
	}
	
	// Update is called once per frame
	void Update () {

		if (prevPauseState != isPaused) {
			prevPauseState = isPaused;
			if (isPaused) {
				PauseGame (true);
			} else {
				PauseGame (false);
			}
		}

		if (Input.GetButtonDown("Cancel") ) {
			Debug.Log ("Cancel Key is pressed");
			switchPause();
		}	
	}

	void PauseGame(bool state) {
		pausePanel.SetActive (state);

		if (state) {			
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			player.GetComponent<FirstPersonController> ().Pause();
		} else {			
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			player.GetComponent<FirstPersonController> ().Resume();
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