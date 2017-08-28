using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBullets : MonoBehaviour {

	public GameObject bullet;
	public Transform spawn;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			var b = (GameObject)Instantiate (bullet, spawn.position, spawn.rotation);
			bullet.GetComponent<Rigidbody> ().velocity = bullet.transform.forward * 6f;
		}
	}
}
