using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBlockFloating : MonoBehaviour {
	
	private Rigidbody rigidbody;
	
	void Start() {
		rigidbody = GetComponent<Rigidbody>();
	}
	
	void Update() {
		;
	}
	
	void OnCollisionEnter(Collision collision) {
		if (rigidbody != null) {
			rigidbody.useGravity = true;
		}
	}
	
}
