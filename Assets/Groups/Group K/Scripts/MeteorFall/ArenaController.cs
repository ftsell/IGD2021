using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaController : MonoBehaviour
{
    [SerializeField] private float _gameLength = 60.0f;
    [SerializeField] private float _outerTime = 0.3f;
    [SerializeField] private float _middleTime = 0.6f;
    [SerializeField] private float _innerTime = 0.8f;
    [SerializeField] private float _sinkSpeed = 0.5f;

    [Header("Transforms")]
    [SerializeField] private Transform _outerLayer;
    [SerializeField] private Transform _middleLayer;
    [SerializeField] private Transform _innerLayer;

    private bool isRunning = false;
    private float _startTime;
    private Vector3 _initialPosition;

    private float _outerFall;
    private float _middleFall;
    private float _innerFall;

    // Start is called before the first frame update
    void Start()
    {
        _outerFall = _gameLength * _outerTime;
        _middleFall = _gameLength * _middleTime;
        _innerFall = _gameLength * _innerTime;

        _initialPosition = transform.position;

        StartArena();
    }

    public void ResetArena()
    {
        _outerLayer.transform.position = new Vector3(transform.position.x, 0.626f, transform.position.z);
        _middleLayer.transform.position = new Vector3(transform.position.x, 0.623f, transform.position.z); ;
        _innerLayer.transform.position = new Vector3(transform.position.x, 0.62f, transform.position.z); ;
        isRunning = false;
    }

    public void StartArena()
    {
        ResetArena();
        isRunning = true;
        _startTime = Time.time;
    }

    private void SinkRing(Transform ring)
    {
        if(ring.position.y >= -1.5f)
        {
            Vector3 direction = new Vector3(ring.position.x, -1.1f, ring.position.z);
            ring.Translate(direction * _sinkSpeed * Time.deltaTime);
        }
    }

    public float GetCurrentRingRadius() 
    {
        // I am deeply sorry for the magic numbers, but the old method doesn't work with the new platform model
        float currTime = Time.time;
        if (currTime > _startTime + _middleFall)
        {
            return 3 / 2 - 0.5f;
        }
        else if (currTime > _startTime + _outerFall)
        {
            return 10 / 2 - 0.5f;
        }
        else
        {
            return 15 / 2 - 0.5f;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isRunning)
        {
            float currTime = Time.time;
            if(currTime > _startTime + _innerFall)
            {
                SinkRing(_innerLayer);
            }
            else if(currTime > _startTime + _middleFall)
            {
                SinkRing(_middleLayer);
            }
            else if(currTime > _startTime + _outerFall)
            {
                SinkRing(_outerLayer);
            }
        }
    }
}
