using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 0.6f;
	private Vector3 dir = Vector3.zero;
	private CharacterController controller;

	void Start() {
		controller = GetComponent<CharacterController> ();
	}

	void Update() {
		dir.Set (Input.GetAxis ("Horizontal"), 0f, Input.GetAxis ("Vertical"));
		dir.Normalize ();
		dir *= speed * Time.deltaTime;
		dir = transform.TransformDirection (dir);
		//controller.Move (dir); 
		//controller.SimpleMove(dir);
    Debug.Log(dir);
		controller.Move (dir);
	}

}
