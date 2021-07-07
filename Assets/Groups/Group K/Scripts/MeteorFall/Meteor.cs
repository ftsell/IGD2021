using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] private float _fallSpeed = 1.0f;
    [SerializeField] private float _maxRotationSpeed = 0.5f;
    [SerializeField] private Transform _decal;
    [SerializeField] private Transform _body;
    [SerializeField] private GameObject _Shockwave;
    [SerializeField] private GameObject _particleSystem;
    [SerializeField] private GameObject _explosionDecal;
    [SerializeField] private AudioClip _sound;

    private Vector3 _rotation;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        RaycastHit[] hits = Physics.RaycastAll(transform.position, -transform.up, 20.0f);
        // filter player out
        _decal.position = hits[0].collider.transform.position + new Vector3(0, 0.01f, 0);
        _rotation = new Vector3(Random.Range(0, _maxRotationSpeed), Random.Range(0, _maxRotationSpeed), Random.Range(0, _maxRotationSpeed));
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position - transform.up * Time.deltaTime * _fallSpeed;
        _body.Rotate(_rotation * Time.deltaTime);

        SetDecalPosition();

        // for now
        if(transform.position.y < -0.5f)
        {
            Destroy(gameObject);
        }
    }

    public void SetDecalPosition()
    {
        RaycastHit[] hits = Physics.RaycastAll(transform.position, -transform.up, 20.0f);
        Vector3 highGround = new Vector3(0, 0, 0);
        float lastHitDistance = 1000;
        bool foundGround = false;
        foreach (RaycastHit hit in hits)
        {
            if(hit.collider.CompareTag("K_Ground"))
            {
                if(hit.distance < lastHitDistance)
                {
                    highGround = hit.point;
                    lastHitDistance = hit.distance;
                    foundGround = true;
                }
            }
        }
        if(foundGround)
        {
            _decal.position = highGround + new Vector3(0, 0.01f, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<RBCharacterController>().Explode(transform.position);
            Explosion();
        }
        else if(collision.collider.CompareTag("K_Ground"))
        {
            Instantiate(_Shockwave, transform.position, Quaternion.identity, null);
            GameObject explosionDecal = Instantiate(_explosionDecal, collision.GetContact(0).point, Quaternion.Euler(-90.0f, 0.0f, 0.0f), null);
            Destroy(explosionDecal, 5.0f);
            Explosion();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Explosion()
    {
        _audioSource.PlayOneShot(_sound);
        GameObject part = Instantiate(_particleSystem, transform.position, Quaternion.identity, null);
        Destroy(part, 3.0f);
        GetComponent<Collider>().enabled = false;
        _body.GetComponent<Renderer>().enabled = false;
        Destroy(gameObject, 2.0f);
    }
}
