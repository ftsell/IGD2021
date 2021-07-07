using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _meteor;
    [SerializeField] private float _cooldown = 0.5f;
    [SerializeField] private float _cooldownSlowFactor = 2;

    [SerializeField] private GameObject _arena;
    private ArenaController _arenaManager;

    private float _lastSpawn;
    private float _lastRingRadius;
    private float _rings = 3;

    // Start is called before the first frame update
    void Start()
    {
        _lastSpawn = 0;
        _arenaManager = _arena.GetComponent<ArenaController>();

        _lastRingRadius = _arenaManager.GetCurrentRingRadius();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > _lastSpawn + _cooldown && _rings > 1)
        {
            if(_arenaManager.GetCurrentRingRadius() < _lastRingRadius)
            {
                _cooldown *= 2;
                _rings--;
                _lastRingRadius = _arenaManager.GetCurrentRingRadius();
            }

            Vector2 p = GetSpawnPoint(_arenaManager.GetCurrentRingRadius());
            Vector3 spawnPoint = transform.position + new Vector3(p.x, 0, p.y);
            Instantiate(_meteor, spawnPoint, Quaternion.identity, transform);
            _lastSpawn = Time.time;
        }
    }

    public Vector2 GetSpawnPoint(float radius)
    {
        float r = radius * Mathf.Sqrt(Random.value);
        float theta = Random.value * 2 * Mathf.PI;

        float x = r * Mathf.Cos(theta);
        float y = r * Mathf.Sin(theta);

        return new Vector2(x, y);
    }
}
