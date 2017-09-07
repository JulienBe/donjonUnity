using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMover : MonoBehaviour {

  public float speed;
  private Rigidbody rb;
  private Vector3 dir = new Vector3(0, 0, 0);
  private Transform transform;
  private Vector3 previousPos = new Vector3();

	void Start () {
    rb = GetComponent<Rigidbody>();
    transform = GetComponent<Transform>();
    dir.Set(Random.value, Random.value, Random.value);
	}
	
	void Update () {
    dir.Normalize();
    dir.Set(dir.x * speed * Time.deltaTime, 0f, dir.z * speed * Time.deltaTime);
    // what a crappy api. Can't you just accept another vec3 ?
    previousPos.Set(transform.position.x, transform.position.y, transform.position.z);
    rb.MovePosition(rb.position + dir);
	}

  void OnTriggerEnter(Collider other) {
    RaycastHit tokitobashi;
   
    if(Physics.Raycast(previousPos, dir, out tokitobashi)) {
      Bounce(tokitobashi.normal);
    } else {
      Debug.LogError("NOP lol");
    }
  }

  void Bounce(Vector3 normal) {
    dir.Normalize();
    var velocityDotProduct = Vector3.Dot(normal, dir);
    Debug.Log("=---=---=---=---=---=---=---=---=");
    Debug.Log("normal : " + normal);
    Debug.Log("before : " + dir);
    Debug.Log("dot : " + velocityDotProduct);
    dir.Set(dir.x - 2 * velocityDotProduct * normal.x, dir.y, dir.z - 2 * velocityDotProduct * normal.z);
    Debug.Log("after : " + dir);
    //rb.MoveRotation(Vector3.Angle(Vector3.zero, dir));
  }

  void OnCollisionEnter(Collision collision) {
    Debug.Log("Collision : " + collision);
  }
}
