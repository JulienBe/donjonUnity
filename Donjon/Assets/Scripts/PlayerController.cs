using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

  public float speed, friction, acceleration;
	private Vector3 dir = Vector3.zero;
  private Vector3 currentMove = Vector3.zero;
	private CharacterController controller;

	void Start() {
		controller = GetComponent<CharacterController> ();
	}

	void Update() {
    // because rotated
    var xAxis = Input.GetAxis ("Horizontal");
    var yAxis = Input.GetAxis ("Vertical");

    currentMove.Set(xAxis, 0f, yAxis);
    if (currentMove.sqrMagnitude > 1f)
      currentMove.Normalize();
    currentMove *= acceleration * Time.deltaTime;

    dir += currentMove;
    dir = Vector3.ClampMagnitude(dir, speed * Time.deltaTime);
		controller.Move (dir);

    var drag = 1 / (1 + (Time.deltaTime * friction));
    dir *= drag;
	}

}
