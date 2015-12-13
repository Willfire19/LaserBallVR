using UnityEngine;
using System.Collections;

public class GateCollide : MonoBehaviour {

    //GameObject gate;
	// Use this for initialization
	void Start () {
        //gate = gameObject;
	}

    void OnTriggerEnter(Collider other)
    {
        if( other.gameObject.name == "Player")
        {
            Debug.Log("You Won!");
        }
    }
	
	// Update is called once per frame
	void Update () {
	}
}
