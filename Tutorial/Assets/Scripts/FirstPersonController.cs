using UnityEngine;
using UnityEngine.VR;
using System.Collections;

public class FirstPersonController : MonoBehaviour {

	public float movementSpeed = 10.0f;
	public float mouseSensitivity = 5.0f;
	float verticalRotation = 0;
	//float rotLeftRight
	public float upDownRange = 30.0f;
	public float sprint = 2.0f;
	public float jumpSpeed = 20.0f;

	public float verticalVelocity = 0.0f;
	CharacterController characterController;
	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;;
		Cursor.visible = false;
		characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {

        // Only use this for debugging. Don't put this is final release
        if ( Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

		//transform.Rotate

		// If using Oculus Rift, movement will not work because movement is look input from mouse or right stick
		// When using VR, the camera becomes decoupled from the game object, so moving the camera doesn't rotate the game object
		if (VRDevice.isPresent) {
			// Use the VR input to determin rotation

			//Quaternion vrRotation = InputTracking.GetLocalRotation(VRNode.CenterEye);
			//transform.rotation = Quaternion.Euler( 0, vrRotation.eulerAngles.y * 2, 0);
			//transform.Rotate (0, vrRotation.eulerAngles.y * 2, 0);

		} 
		else {
			//Rotation
			float rotLeftRight = Input.GetAxis ("Mouse X") * mouseSensitivity;
			transform.Rotate (0, rotLeftRight, 0);

			verticalRotation -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
			verticalRotation = Mathf.Clamp (verticalRotation, -upDownRange, upDownRange);
			Camera.main.transform.localRotation = Quaternion.Euler (verticalRotation, 0, 0);
		}


		//Movement
		float forwardSpeed = Input.GetAxis ("Vertical") * movementSpeed;
		float sideSpeed = Input.GetAxis ("Horizontal") * movementSpeed;

		verticalVelocity += Physics.gravity.y * Time.deltaTime;
		if (characterController.isGrounded) {
			verticalVelocity = 0;
		}
		if (characterController.isGrounded && Input.GetButtonDown ("Jump")) {
			verticalVelocity = jumpSpeed;
		}

		Vector3 speed = new Vector3 (sideSpeed, verticalVelocity, forwardSpeed);

		//if (Input.GetKeyDown ("space")) {
		//				print ("space key was pressed");
		//				speed = transform.rotation * speed * sprint;
		//		}
		//else

		if (VRDevice.isPresent) {
			//Debug.Log("Oculus Quaternion: " + InputTracking.GetLocalRotation (VRNode.CenterEye) );
			//Debug.Log("Oculus Eular Angles: " + InputTracking.GetLocalRotation(VRNode.CenterEye).eulerAngles);
			//speed = InputTracking.GetLocalRotation (VRNode.CenterEye) * speed;
			speed = Quaternion.Euler( 0, InputTracking.GetLocalRotation(VRNode.Head).eulerAngles.y, 0 ) * speed;
			//speed = transform.rotation * speed;
			//speed = Camera.main.transform.forward + speed;
		} 
		else {
			speed = transform.rotation * speed;
		}


		characterController.Move (speed * Time.deltaTime);

	}
}
