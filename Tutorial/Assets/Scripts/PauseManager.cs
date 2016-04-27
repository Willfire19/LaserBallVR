using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class PauseManager : MonoBehaviour {

	GameObject player;
	GameObject pausePanel;

	public UnityEvent pauseFunction;
	public UnityEvent resumeFunction;

	public bool isPaused;
	private bool prevPauseState;

	// Use this for initialization
	void Start () {
		player = gameObject.transform.root.gameObject;
		Debug.Log (player);
		pausePanel = gameObject.transform.FindChild("PauseCanvas").gameObject;
		Debug.Log(pausePanel);
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
			// TODO: Write Backup call if pauseFunction is not set
			//player.GetComponent<FirstPersonController> ().Pause();
			pauseFunction.Invoke();
		} else {			
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			// TODO: Write Backup call if resumeFunction is not set
			//player.GetComponent<FirstPersonController> ().Resume();
			resumeFunction.Invoke();
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