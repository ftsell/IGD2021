using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeteorFallAI : MonoBehaviour
{
    private RBCharacterController _controller;
    private MiniGameManager _manager;

    private RBCharacterController[] _players;

    private enum State
    {
        Idle,
        Wandering,
        Hunting,
        Dodging,
        Sinking,
        Stunned
    }

    State _currentState;

    float groundHeight = 100;

    float timing = 0;
    float unstunSegment;
    float unStunTimer = 0;

    float jumpDisabledTimer;
    float jumpTimer = 0;
    float jumpDelay = 0.2f;
    float jumpDistance = 2.0f;

    float directionTimer = 0;
    float directionChangeSeg = 0.25f;

    float kickTimer = 0;
    float kickDelay = 0.2f;
    float huntTime = 0;

    Vector2 currentWaypoint;
    Vector2 dodgeDirection;
    float meteorStressLevel;
    float errorMargin = 0.05f;

    private void Start()
    {
        _controller = transform.GetComponent<RBCharacterController>();
        _manager = GameObject.FindObjectOfType<MiniGameManager>().GetComponent<MiniGameManager>();
        _currentState = State.Idle;
        unstunSegment = _manager.AI_StunDuration / _controller._mashLimit;
        _players = FindPlayers();
        currentWaypoint = Vector2.zero;
        dodgeDirection = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // Set initial Ground Position
        if(groundHeight == 100 && _controller.GroundCheck())
        {
            groundHeight = transform.position.y - 0.01f;
        }
        
        // Dodge Wave
        if(IsWaveIncoming() && Time.time > jumpDisabledTimer && !_controller.isStunned)
        {
            if(Random.value < _manager.AI_WaveDodgePercentage)
            {
                if(Time.time > jumpTimer + jumpDelay)
                {
                    _controller.TryJump();
                    jumpTimer = Time.time;
                }
            }
            else
            {
                jumpDisabledTimer = Time.time + _manager.AI_JumpPenalty;
            }
        }

        if(IsMeteorAbove())
        {
            meteorStressLevel += 1.0f * Time.deltaTime;
        }

        switch(_currentState)
        {
            case State.Idle:
                if(CheckForStateChange())
                {
                    break;
                }
                if (Time.time > timing + _manager.AI_idleTime)
                {
                    timing = Time.time;
                    _currentState = State.Wandering;
                }
                break;
            case State.Wandering:
                if (CheckForStateChange())
                {
                    break;
                }

                Wander();
                break;
            case State.Hunting:
                if (CheckForStateChange())
                {
                    CancelMovement();
                    break;
                }
                Hunt();
                break;
            case State.Dodging:
                if(meteorStressLevel <= 0)
                {
                    meteorStressLevel = 0;
                    dodgeDirection = Vector2.zero;
                    _currentState = State.Wandering;
                }
                DodgeMeteor();
                break;
            case State.Sinking:
                if (CheckForStateChange())
                {
                    CancelMovement();
                    break;
                }
                GetToHighGround();
                break;
            case State.Stunned:
                if (_controller.isStunned && Time.time > unStunTimer + unstunSegment)
                {
                    _controller.UnstunInput();
                    unStunTimer = Time.time;
                }
                else if(!_controller.isStunned)
                {
                    _currentState = State.Wandering;
                }
                break;
        }
    }

    private bool CheckForStateChange()
    {
        if (_controller.isStunned && _currentState != State.Stunned)
        {
            CancelMovement();
            _currentState = State.Stunned;
            return true;
        }
        if (transform.position.y < groundHeight - _manager.AI_SinkingTolerance && groundHeight != 100)
        {
            _currentState = State.Sinking;
            return true;
        }
        if (IsMeteorAbove() && meteorStressLevel > _manager.AI_MeteorTolerance && _currentState != State.Dodging)
        {
            CancelMovement();
            _currentState = State.Dodging;
            return true;
        }
        RBCharacterController closest = FindBestTarget();
        if(closest != null && _currentState != State.Hunting && Time.time > huntTime)
        {
            _currentState = State.Hunting;
            return true;
        }
        return false;
    }

    private void CancelMovement()
    {
        dodgeDirection = Vector2.zero;
        currentWaypoint = Vector2.zero;
        _controller._moveDirection = Vector2.zero;
    }

    private void GetToHighGround()
    {
        Vector2 direction = Vector2.zero - GetFlatVector(transform.position);
        direction.Normalize();
        _controller._moveDirection += direction;
        _controller._moveDirection.Normalize();

        if(transform.position.y <= groundHeight - _controller._stepHeight)
        {
            float currentRingRadius = _manager._arena.GetComponent<ArenaController>().GetCurrentRingRadius();
            if(Time.time > jumpDisabledTimer)
            {
                if (currentRingRadius + jumpDistance >= Vector2.Distance(Vector2.zero, GetFlatVector(transform.position))  + (Random.value - 0.5f))
                {
                    if (Time.time > jumpTimer + jumpDelay)
                    {
                        _controller.TryJump();
                        jumpTimer = Time.time;
                    }
                }
                else
                {
                    jumpDisabledTimer = Time.time + _manager.AI_JumpPenalty;
                }
            }
        }
    }

    private void DodgeMeteor()
    {
        if(dodgeDirection == Vector2.zero)
        {
            currentWaypoint = GetWayPoint(_manager._arena.GetComponent<ArenaController>().GetCurrentRingRadius());
            Vector2 direction = currentWaypoint - GetFlatVector(transform.position);
            direction.Normalize();
            _controller._moveDirection = direction;
            //_controller._moveDirection.Normalize();
        }
        meteorStressLevel -= 1.0f * Time.deltaTime;
    }

    private void Wander()
    {
        float radius = _manager._arena.GetComponent<ArenaController>().GetCurrentRingRadius();

        if(currentWaypoint == Vector2.zero || Vector2.Distance(GetFlatVector(transform.position), currentWaypoint) <= errorMargin 
            || Vector2.Distance(currentWaypoint, Vector2.zero) > radius)
        {
            currentWaypoint = GetWayPoint(radius);
        }

        Vector2 direction = currentWaypoint - GetFlatVector(transform.position);
        direction.Normalize();
        _controller._moveDirection += direction;
        _controller._moveDirection.Normalize();
    }

    public Vector2 GetWayPoint(float radius)
    {
        float r = radius * Mathf.Sqrt(Random.value);
        float theta = Random.value * 2 * Mathf.PI;

        float x = r * Mathf.Cos(theta);
        float y = r * Mathf.Sin(theta);

        return new Vector2(x, y);
    }

    private void Hunt()
    {
        RBCharacterController closest = FindBestTarget();
        if(closest != null)
        {
            Vector2 selfPos = GetFlatVector(transform.position);
            Vector2 closestPos = GetFlatVector(closest.transform.position);
            float distance = Vector2.Distance(selfPos, closestPos);
            if (distance >= _manager.AI_KickDistance)
            {
                Vector3 dir3 = FindBestTarget().transform.position - transform.position;
                Vector2 dir2 = GetFlatVector(dir3, true);
                dir2.Normalize();
                _controller._moveDirection += dir2;
                _controller._moveDirection.Normalize();
            }
            else
            {
                if (Time.time > kickTimer + kickDelay)
                {
                    _controller.TryKick();
                    kickTimer = Time.time;

                    huntTime = Time.time + _manager.AI_HuntDelay;
                    _currentState = State.Wandering;
                }
            }
        }
    }

    private RBCharacterController FindBestTarget()
    {
        RBCharacterController closest = null;
        RBCharacterController stunnedTarget = null;
        float minDistance = 100;
        Vector2 selfPos = GetFlatVector(transform.position);
        foreach(RBCharacterController player in _players)
        {
            if(player.gameObject.activeSelf)
            {
                Vector2 playerPos = GetFlatVector(player.transform.position);
                float distance = Vector2.Distance(playerPos, selfPos);
                if(distance < minDistance && distance < _manager.AI_PlayerDetectRadius)
                {
                    minDistance = distance;
                    closest = player;
                }
                if(player.isStunned && distance < _manager.AI_PlayerDetectRadius * _manager.AI_StunnedPreferationMultiplier)
                {
                    stunnedTarget = player;
                }
            }
        }
        if(stunnedTarget != null)
        {
            return stunnedTarget;
        }
        return closest;
    }

    private bool IsMeteorAbove()
    {
        DecalType[] meteors = GameObject.FindObjectsOfType<DecalType>();
        foreach (DecalType meteor in meteors)
        {
            Vector2 meteorPos = GetFlatVector(meteor.transform.position);
            Vector2 selfPos = GetFlatVector(transform.position);
            float distance = Vector2.Distance(meteorPos, selfPos);
            if (distance <= _manager.AI_MeteorFleeDistance)
            {
                return true;
            }
        }
        return false;
    }

    private bool IsWaveIncoming()
    {
        Shockwave[] waves = GameObject.FindObjectsOfType<Shockwave>();
        foreach(Shockwave wave in waves)
        {
            Vector2 wavePos = GetFlatVector(wave.transform.position);
            Vector2 selfPos = GetFlatVector(transform.position);
            float distance = Vector2.Distance(wavePos, selfPos) - wave._currRadius;
            if (distance <= _manager.AI_WaveDetectionRadius && distance >= 0)
            {
                return true;
            }
        }
        return false;
    }

    private RBCharacterController[] FindPlayers()
    {
        RBCharacterController[] allControllers = FindObjectsOfType<RBCharacterController>();
        allControllers = allControllers.Where(val => val != _controller).ToArray();
        return allControllers;
    }

    #region HelperFunctions
    private Vector2 GetFlatVector(Vector3 vec, bool normalize=false)
    {
        Vector2 vec2 = new Vector2(vec.x, vec.z);
        if(normalize)
        {
            vec2.Normalize();
        }
        return vec2;
    }
    #endregion

    private void OnDrawGizmos()
    {
        if(currentWaypoint != Vector2.zero)
        {
            Gizmos.DrawWireCube(new Vector3(currentWaypoint.x, 0, currentWaypoint.y), Vector3.one);
        }
    }
}
