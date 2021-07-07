using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBlock : MonoBehaviour {
	
	private CannonStandoff game;
	private bool scored;
	private AudioSource audio;
	
	public float timeToLiveAfterScore = 3.0f;
	public Material[] materials;
	public AudioClip[] collisionSounds;
	
	private void changeMaterial() {
		if (materials == null || materials.Length == 0) {
			return;
		}
		
		Renderer[] children = GetComponentsInChildren<Renderer>();
		int i = Random.Range(0, materials.Length);
		Material mat = materials[i];
		
		foreach (Renderer c in children) {
			c.material = mat;
		}
	}
	
	void Start() {
		GameObject manager = GameObject.Find("GameManager");
		
		if (manager != null) {
			game = manager.GetComponent<CannonStandoff>();
		}
		
		changeMaterial();
		
		audio = GetComponent<AudioSource>();
	}
	
	void Update() {
		;
	}
	
	void OnCollisionEnter(Collision collision) {
		GameObject collider = collision.gameObject;
		
		if (!scored && collider != null && collider.tag == "Floor") {
			float posX = transform.position.x;
			
			scored = true;
			
			if (game != null) {
				if (posX < 0) {
					game.ScoreForSolo();
				} else if (posX > 0) {
					game.ScoreForTeam();
				} else {
					//Neither side gets a point I guess?
				}
			}
			
			Destroy(gameObject, timeToLiveAfterScore);
		}
		
		if (audio != null && collisionSounds != null && collisionSounds.Length > 0) {
			int i = Random.Range(0, collisionSounds.Length);
			AudioClip sound = collisionSounds[i];
			float volume = Mathf.Min(collision.relativeVelocity.magnitude / 100.0f, 0.5f);
			
			audio.PlayOneShot(sound, volume);
		}
	}
	
	public bool HasScored() {
		return scored;
	}
	
}
