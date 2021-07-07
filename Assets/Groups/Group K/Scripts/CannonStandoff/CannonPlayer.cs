using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.UI;

public class CannonPlayer : MonoBehaviour {
	
	private GameObject cannon;
	private bool shooting;
	private float lastShotTimer;
	private Material color;
	private AudioSource audio;
	
	/* AI */
	private bool aiControlled;
	private GameObject wall;
	private WallBlock currentTarget;
	private int attemptedShots;
	
	public CannonMarker marker = null;
	public Camera camera = null;
	public GameObject bullet = null;
	public float bulletSpeed = 15.0f;
	public float cooldown = 1.0f;
	
	/* AI */
	public int maxAttemptedShots = 5;
	
	public GameObject Cannon {
		get { return transform.Find("Cannon").gameObject; }
	}
	
	public Material Color {
		get { return color; }
		set {
			Renderer[] parts = Cannon.GetComponentsInChildren<Renderer>();
			Image crosshair = marker.GetComponent<Image>();
			
			foreach (Renderer c in parts) {
				c.material = value;
			}
			
			crosshair.color = value.color;
			
			color = value;
		}
	}
	
	private void SwitchInput() {
		string controlScheme = GetComponent<PlayerInput>().defaultControlScheme;
		
		GetComponent<PlayerInput>().SwitchCurrentControlScheme(controlScheme, Keyboard.current);
	}
	
	private void ExecuteAi() {
		if (!aiControlled || wall == null || camera == null) {
			return;
		}
		
		if (currentTarget == null || currentTarget.HasScored() || attemptedShots > maxAttemptedShots) {
			List<WallBlock> blocks = wall.GetComponentsInChildren<Transform>()
				.Select(c => c.GetComponent<WallBlock>())
				.Where(c => c != null)
				.Where(c => !c.HasScored())
				.ToList();
			int blockCount = blocks.Count;
			
			if (blockCount == 0) {
				marker.Move(Vector2.zero);
				
				return;
			}
			
			int i = Random.Range(0, blockCount);
			
			currentTarget = blocks[i];
			attemptedShots = 0;
			shooting = false;
		}
		
		Vector3 targetScreenPos = camera.WorldToScreenPoint(currentTarget.transform.position)
			+ 3.0f * new Vector3(0.0f, currentTarget.transform.position.y, 0.0f)
			+ 1.0f * new Vector3(0.0f, Mathf.Pow(currentTarget.transform.position.y, 2.0f), 0.0f);
		Vector3 markerScreenPos = camera.WorldToScreenPoint(marker.transform.position);
		Vector3 distance = targetScreenPos - markerScreenPos;
		Vector2 distance2d = new Vector2(distance.x, distance.y);
		
		if (distance2d.magnitude > marker.speed + 1.0f) {
			marker.Move(distance2d);
			
			shooting = false;
		} else {
			marker.Move(Vector2.zero);
			
			shooting = true;
		}
	}
	
	private void LookAtTarget() {
		if (marker != null && camera != null) {
			Vector3 markerPos = camera.WorldToScreenPoint(marker.transform.position);
			
			markerPos.z = Mathf.Abs(camera.transform.position.x);
			
			Vector3 target = camera.ScreenToWorldPoint(markerPos);
			
			cannon.transform.LookAt(target);
		}
	}
	
	private void CheckShooting() {
		if (shooting && bullet != null && lastShotTimer >= cooldown) {
			Vector3 spawnPoint = cannon.transform.position + cannon.transform.forward;
			GameObject instance = Instantiate(bullet, spawnPoint, Quaternion.identity);
			Rigidbody rb = instance.GetComponent<Rigidbody>();
			Renderer renderer = instance.GetComponent<Renderer>();
			
			rb.velocity = cannon.transform.forward * bulletSpeed;
			renderer.material = color;
			shooting = false;
			lastShotTimer = 0.0f;
			
			if (aiControlled) {
				attemptedShots++;
			}
			
			audio.Play();
		}
	}
	
	void Start() {
		if (!aiControlled) {
			SwitchInput();
		}
		
		cannon = Cannon;
		lastShotTimer = cooldown;
		audio = GetComponent<AudioSource>();
	}
	
	void Update() {
		ExecuteAi();
		LookAtTarget();
		CheckShooting();
		
		lastShotTimer += Time.deltaTime;
	}
	
	void OnSouthPress() {
		if (aiControlled) {
			return;
		}
		
		shooting = true;
	}
	
	void OnSouthRelease() {
		if (aiControlled) {
			return;
		}
		
		shooting = false;
	}
	
	void OnMove(InputValue value) {
		if (aiControlled) {
			return;
		}
		
		Vector2 movement = value.Get<Vector2>();
		
		marker.Move(movement);
	}
	
	void OnMoveDpad(InputValue value) {
		if (aiControlled) {
			return;
		}
		
		Vector2 movement = value.Get<Vector2>();
		
		marker.Move(movement);
	}
	
	public void SetAiControlled(bool aiControlled) {
		this.aiControlled = aiControlled;
	}
	
	public void SetWall(GameObject wall) {
		this.wall = wall;
	}
	
}
