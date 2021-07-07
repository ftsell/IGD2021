using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class playerDetection : MonoBehaviour {

    public float decaySpeed = 0.2f;
    public float spawnProtection = 0.0f;
    public AudioSource dyingSound;

    public float decay = 0.0f; // [0.0, 1.0]

    private Rigidbody rigidBody;
    private BoxCollider boxCollider;
    private MeshRenderer meshRenderer;
    private List<MeshRenderer> meshRendererOfKnobs;

    private float platformTouchedTime;
    private bool triedKill = false;

    private void Start() {

    }

    void Awake() {
        spawnProtection = Time.time + 5.0f;
        this.boxCollider = this.GetComponent<BoxCollider>();

        var meshRenderers = FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
        this.meshRendererOfKnobs = new List<MeshRenderer>();
        foreach (var renderer in meshRenderers) {
            if (renderer.name == "Shell" && this.meshRenderer == null)
                this.meshRenderer = renderer;
            if (renderer.name.Contains("knob") && this.meshRendererOfKnobs.Count < 16)
                this.meshRendererOfKnobs.Add(renderer);
            if (this.meshRenderer != null && this.meshRendererOfKnobs.Count >= 16)
                break;
        }
        this.rigidBody = this.GetComponent<Rigidbody>();




        var col = new Color(0.33f, 0.33f, 0.33f);
        this.meshRenderer.material.SetColor("_Color", col);
        this.meshRenderer.material.SetColor("_BaseColor", col);
        foreach (var mr in this.meshRendererOfKnobs) {
            mr.material.SetColor("_Color", col);
            mr.material.SetColor("_BaseColor", col);

        }

    }
    bool IsSpawnProtected() {
        return spawnProtection == 0.0f || spawnProtection >= Time.time;
    }

    void OnCollisionStay(Collision col) {
        if (IsSpawnProtected()) {
            return;
        }

        if (!col.collider.CompareTag("Player")) return;
        if (platformTouchedTime > 0) return;
        platformTouchedTime = Time.time;

        if (dyingSound != null) {
            dyingSound.Play(0);
        }
    }

    void Update() {
        if (IsSpawnProtected()) {
            return;
        }

        if (platformTouchedTime <= 0) {
            return;
        }

        if (!triedKill) {
            triedKill = true;
            Destroy(gameObject, 3);
        }


        if (rigidBody.IsSleeping()) {
            rigidBody.WakeUp();
        }

        var sinceTouched = Time.time - platformTouchedTime;

        var col = new Color(sinceTouched * 1.0f, 0.0f, 0.0f);

        this.meshRenderer.material.SetColor("_Color", col);
        this.meshRenderer.material.SetColor("_BaseColor", col);
        foreach (var mr in this.meshRendererOfKnobs) {
            mr.material.SetColor("_Color", col);
            mr.material.SetColor("_BaseColor", col);
        }

        var myDelta = Time.deltaTime * decaySpeed;
        decay += myDelta;

        var fallingDistance = myDelta * 2.0f * (float)Mathf.Pow(decay, 3.0f);
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - fallingDistance, this.transform.position.z);
    }
}

