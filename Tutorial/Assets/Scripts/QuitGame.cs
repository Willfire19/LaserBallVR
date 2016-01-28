using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuitGame : MonoBehaviour {

	public void QuitGameClicked() {
		Debug.Log("Quit Game Button Clicked");
		Application.Quit ();
	}

}
