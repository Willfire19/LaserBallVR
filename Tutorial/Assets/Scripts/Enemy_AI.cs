using UnityEngine;
using System.Collections;

public class Enemy_AI : MonoBehaviour {

	GameObject player;
	CharacterController enemyController;
	private Weapon laser_gun;
	public float speed = 5.0f;
	public float MinDist = 1.0f;
	public float MaxDist = 10.0f;

	// Use this for initialization
	void Start () {
		enemyController = GetComponent<CharacterController> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		laser_gun = GetComponent<Weapon> ();
		//Debug.Log (transform.gameObject);
	}
	
	// Update is called once per frame
	void Update () {

		if (player != null) {
			if (Vector3.Distance (player.transform.position, transform.position) >= MaxDist) {
				Idle ();
			} else {
				Attack ();
			}
		}
	
	}

	void Idle(){

		transform.gameObject.GetComponent<Renderer>().material.color = Color.green;
		enemyController.SimpleMove (transform.forward * 0);	
	
	}

	void Attack(){
		//gameObject.renderer.material.color = Color.white;
		transform.gameObject.GetComponent<Renderer>().material.color = Color.red;
		Vector3 target = player.transform.position;
		laser_gun.Fire ( new Vector3(target.x, target.y - 2.5f, target.z) );
		target.y = transform.position.y;
		//target.y = -3.57f;
		transform.LookAt (target);
		enemyController.SimpleMove (transform.forward * speed);
		//enemyController.SimpleMove (transform.forward * speed * Time.deltaTime);
		
		//transform.gameObject.GetComponent<Rigidbody> ().AddForce (transform.forward * speed, ForceMode.Force);
		
		//transform.LookAt (player.transform);
		//Vector3 newPos = Vector3.Scale (transform.position, new Vector3 (1, 0, 1));
		//transform.position += newPos * speed * Time.deltaTime;
		
		//transform.position += transform.forward * speed * Time.deltaTime;
	}
}
