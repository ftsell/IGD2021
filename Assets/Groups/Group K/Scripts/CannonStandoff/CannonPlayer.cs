﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CannonPlayer : MonoBehaviour {
	
	private GameObject cannon;
	
	public GameObject marker = null;
	
	private void SwitchInput() {
		string controlScheme = GetComponent<PlayerInput>().defaultControlScheme;
		
		GetComponent<PlayerInput>().SwitchCurrentControlScheme(controlScheme, Keyboard.current);
	}
	
	void Start() {
		SwitchInput();
		
		cannon = transform.Find("Cannon").gameObject;
	}
	
	void Update() {
		if (marker != null) {
			cannon.transform.LookAt(marker.transform);
		}
	}
	
	private void OnMove(InputValue value) {
		;
	}
	
	private void OnMoveDpad(InputValue value) {
		;
	}
	
}
