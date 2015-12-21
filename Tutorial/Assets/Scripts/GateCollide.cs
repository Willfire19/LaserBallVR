using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GateCollide : MonoBehaviour {

	GameObject playerHUD;
	Text gameWin;
	// Use this for initialization
	void Start () {
		playerHUD = GameObject.Find ("PlayerHUD");
		gameWin = playerHUD.transform.Find ("GameStatus/GameStatusText").GetComponent<Text> ();
	}

    void OnTriggerEnter(Collider other)
    {
        if( other.gameObject.name == "Player")
        {
            Debug.Log("You Won!");
			gameWin.text = "You Won!";
        }
    }
	
	// Update is called once per frame
	void Update () {
	}
}
