using UnityEngine;
using System.Collections;
using System;

[RequireComponent (typeof(CharacterController))]
public class PlayerInput : MonoBehaviour {

	public CharacterController character_controller;
	public float move_speed;
	public float slowMoveSpeed = 40f;
	public float fastMoveSpeed = 160f;
	public float rotate_speed;
	public AudioClip beepclip;
	public AudioSource source;
	public AudioClip micclip;
	public Camera playercam;
	public float fovTime =1;
	public float fovElapsedTime = 0;
	public bool playerIsMoving = false;


	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update ()
	{
		if (character_controller.velocity.magnitude > 0) {playerIsMoving = true;} else {playerIsMoving = false;}
		//Debug.Log (fovElapsedTime + "is elapsed");

		Vector3 forward = Input.GetAxis ("Vertical") * transform.TransformDirection (0, 0, 1) * move_speed * Time.deltaTime;
		//forward.y = 0; //clamps player to current y-axis
		Vector3 side2side = Input.GetAxis ("Horizontal") * transform.TransformDirection (Vector3.right) * move_speed * Time.deltaTime;
		//side2side.y = 0; //clamps player to current y-axis
		character_controller.Move (forward + side2side);
		//character_controller.SimpleMove(Physics.gravity); //disable for observer cam

		if (Input.GetButtonDown ("Fire1")) {
			source.clip = beepclip;
			source.Play ();
		}
		if (Input.GetButton ("Fire2")&&playerIsMoving) {
			move_speed = fastMoveSpeed;
			if (playercam == null) {
				playercam = this.GetComponentInChildren<Camera> ();
			}
			playercam.fieldOfView = Mathf.Lerp (60, 90, Mathf.Clamp (fovElapsedTime / fovTime, 0, 1));
			fovElapsedTime += Time.deltaTime;
			fovElapsedTime = Mathf.Clamp (fovElapsedTime, 0, 1);
		}
		if (Input.GetButton ("Fire2") == false || playerIsMoving == false) {
			move_speed = slowMoveSpeed;
			if (playercam == null) {
				playercam = this.GetComponentInChildren<Camera> ();
			}
			playercam.fieldOfView = Mathf.Lerp (60, 90, Mathf.Clamp (fovElapsedTime / fovTime, 0, 1));
			fovElapsedTime -= Time.deltaTime;
			fovElapsedTime = Mathf.Clamp (fovElapsedTime, 0, 1);
		}


		if (Input.GetButtonDown ("Jump")) {
			micclip = Microphone.Start (MicTest.device, false, 10, 11025);
		}

		if (Input.GetButtonUp ("Jump")) {
			Microphone.End (MicTest.device);
			source.clip = micclip;
			source.Play ();
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}
	}
}