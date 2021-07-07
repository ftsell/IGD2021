using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections;
using UnityEngine.AI;

public enum Axle
{
    Front,
    Rear
}

[Serializable]
public struct Wheel
{
    public GameObject model;
    public WheelCollider collider;
    public Axle axle;
}

public class CarController : MonoBehaviour
{

    private Vector2 movement;
    private Rigidbody rb;
    private bool backwards = false;
    private bool handbrake = false;
    private bool controlEnabled = false;

    private AudioSource engineSound;
    public float maxAcceleration = 70.0f;
    public float turnSensitivity = 0.9f;
    public float maxSteerAngle = 30.0f;
    public float maxVelocity = 55.0f;
    public List<Wheel> wheels;
    public Vector3 centerOfMass;
    public Vector3 wheelRotationOffset;
    public float downForce = 10.0f;
    public List<GameObject> fastGrounds;
    public Boolean steeringReversed = false;
    private Boolean stopped = false;
    public float GroundPercent { get; private set; }
    public float AirPercent { get; private set; }
    public float AddedGravity { get; private set; } = 1.0f;
    public float CoastingDrag { get; private set; } = 7.0f;

    public void DisableControl()
    {
        controlEnabled = false;
    }

    public void EnableControl()
    {
        controlEnabled = true;
    }

    public void StopCar()
    {
        stopped = true;
        foreach(Wheel wheel in wheels)
        {
            wheel.collider.brakeTorque = Mathf.Infinity;
            wheel.collider.steerAngle = 0.0f;
        }
        StartCoroutine(RestartCar(0.5f));
    }

    void FixedUpdate()
    {
        if(!stopped)
        {
            PlayEngineSound();
            CheckDrivingDirection(rb);
            ChangeGroundDependentSpeed();
            CheckGroundContact();
            GroundAirbourne();
            Move();
            Turn();
        }
        ApplyDownForce();
    }


    void Start()
    {
        engineSound = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass;
    }

    void Update()
    {
        AnimateWheels();
    }

    IEnumerator RestartCar(float time)
    {
        yield return new WaitForSeconds(time);

        foreach (Wheel wheel in wheels)
        {
            wheel.collider.brakeTorque = 0.0f;
            wheel.collider.motorTorque = 0.0f;
        }
        stopped = false;
    }

    private void ChangeGroundDependentSpeed()
    {

        RaycastHit raycastHit;
        Vector3 raycastStart = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        if (Physics.Raycast(raycastStart, Vector3.down, out raycastHit, 40.0f))
        {
            if(fastGrounds.Contains(raycastHit.collider.gameObject))
            {
                SetWheelsSidewaysStiffnessTo(1f);
                SetWheelsForwardStiffnessTo(2f);
                maxAcceleration = 70.0f;
                maxVelocity = 55.0f;
            } else
            {
                SetWheelsSidewaysStiffnessTo(0.5f);
                SetWheelsForwardStiffnessTo(0.2f);
                maxAcceleration = 10.0f;
                maxVelocity = 25.0f;
            }
        }
    }


    private void CheckGroundContact()
    {
        int groundedCount = 0;

        foreach (Wheel wheel in wheels)
        {
            if (wheel.collider.isGrounded && wheel.collider.GetGroundHit(out WheelHit hit))
                groundedCount++;
        }

        GroundPercent = (float)groundedCount / 4.0f;
        AirPercent = 1 - GroundPercent;
    }

    void GroundAirbourne()
    {
        if (AirPercent >= 1)
        {
            rb.velocity += Physics.gravity * Time.fixedDeltaTime * AddedGravity;
        }
    }

    private void SetWheelsForwardStiffnessTo(float stiffness)
    {
        foreach (Wheel wheel in wheels)
        {
            SetWheelForwardStiffnessTo(wheel, stiffness);
        }
    }

    private void SetWheelsSidewaysStiffnessTo(float stiffness)
    {
        foreach (Wheel wheel in wheels)
        {
            setWheelSidewaysStiffnessTo(wheel, stiffness);
        }
    }

    private void SetWheelForwardStiffnessTo(Wheel wheel, float stiffness)
    {
        WheelFrictionCurve forwardFriction = wheel.collider.forwardFriction;
        forwardFriction.stiffness = stiffness;
        wheel.collider.forwardFriction = forwardFriction;
    }

    private void setWheelSidewaysStiffnessTo(Wheel wheel, float stiffness)
    {
        WheelFrictionCurve sidewaysFriction = wheel.collider.sidewaysFriction;
        sidewaysFriction.stiffness = stiffness;
        wheel.collider.sidewaysFriction = sidewaysFriction;
    }

    private void Move()
    {
        foreach (Wheel wheel in wheels)
        {
            //Debug.Log("Speed: " + (rb.velocity.magnitude * 3.6f) + " km/h");
            if (controlEnabled)
            {
                // driving forwards --> accelerate
                if (movement.y > 0 && !backwards && GroundPercent > 0.0f)
                {
                    wheel.collider.brakeTorque = 0;
                    ApplyMotorTorque(wheel);
                    //Debug.Log("forward-accelerate");

                }
                // driving forwards --> brake
                else if (movement.y < 0 && !backwards && GroundPercent > 0.0f)
                {
                    wheel.collider.motorTorque = 0;
                    ApplyBrakeTorque(wheel, -(movement.y * 0.001f));
                    //Debug.Log("forward-brake");
                    if(rb.velocity.magnitude <= 0.1)
                    {
                        wheel.collider.brakeTorque = Mathf.Infinity;
                    }
                }
                // driving backwards --> brake
                else if (movement.y > 0 && backwards && GroundPercent > 0.0f)
                {
                    wheel.collider.motorTorque = 0;
                    ApplyBrakeTorque(wheel, movement.y);
                    //Debug.Log("backward-brake");
                }
                // driving backwards --> accelerate
                else if (movement.y < 0 && backwards && GroundPercent > 0.0f)
                {
                    wheel.collider.brakeTorque = 0;
                    ApplyMotorTorque(wheel);
                    //Debug.Log("backward-accelerate");
                }
                // No Input (Coasting drag - DE:Motorbremswirkung)
                else if(movement.y <= 0.01f && !backwards && GroundPercent > 0.0f)
                {
                    //Debug.Log("no input!");
                    wheel.collider.motorTorque = 1;
                    wheel.collider.brakeTorque = CoastingDrag * 100;
                    if (rb.velocity.magnitude <= 0.1)
                    {
                        wheel.collider.brakeTorque = Mathf.Infinity;
                    }
                }
                else
                {
                    wheel.collider.motorTorque = 0;
                }

                // handbrake
                if (handbrake && wheel.axle == Axle.Rear)
                {
                    wheel.collider.brakeTorque = maxAcceleration * 2000 * Time.deltaTime;
                }
            } else
            {
                wheel.collider.motorTorque = 0;
                wheel.collider.brakeTorque = maxAcceleration * 35 * Time.deltaTime;
            }
        }
        //Debug.Log("01" + " mt: " + wheels[0].collider.motorTorque + " bt: " + wheels[0].collider.brakeTorque + '\n' 
        //    + "02" + " mt: " + wheels[1].collider.motorTorque + " bt: " + wheels[1].collider.brakeTorque + '\n'
        //    + "03" + " mt: " + wheels[2].collider.motorTorque + " bt: " + wheels[2].collider.brakeTorque + '\n'
        //    + "04" + " mt: " + wheels[3].collider.motorTorque + " bt: " + wheels[3].collider.brakeTorque + '\n');
    }

    private void CheckDrivingDirection(Rigidbody rigidbody)
    {
        if (rigidbody.velocity.magnitude <= 0.01)
        {
            if (movement.y > 0)
            {
                backwards = false;
            }
            else if (movement.y < 0)
            {
                backwards = true;
            }
        }
    }

    private void ApplyMotorTorque(Wheel wheel)
    {
        if (wheel.collider.attachedRigidbody.velocity.magnitude < maxVelocity)
        {
            wheel.collider.motorTorque = movement.y * maxAcceleration * 500 * Time.deltaTime;
        }
        else
        {
            wheel.collider.motorTorque = 0;
        }
    }

    private void ApplyBrakeTorque(Wheel wheel, float direction)
    {
        wheel.collider.brakeTorque = direction * maxAcceleration * 1000000  * Time.deltaTime;
    }

    private void Turn()
    {
        if(controlEnabled)
        {
            foreach (Wheel wheel in wheels)
            {
                if (wheel.axle == Axle.Front)
                {
                    float steerAngle = movement.x * turnSensitivity * maxSteerAngle;
                    if(steeringReversed)
                    {
                        steerAngle = -steerAngle;
                    }
                    wheel.collider.steerAngle = Mathf.Lerp(wheel.collider.steerAngle, steerAngle, 0.5f);
                }
            }
        }
    }

    private void ApplyDownForce()
    {
        if (rb.velocity.magnitude > 30)
        {
            rb.AddForce(Vector3.down * Time.deltaTime * rb.velocity.magnitude * downForce, ForceMode.Force);
        }
    }

    private void AnimateWheels()
    {
        foreach (Wheel wheel in wheels)
        {
            Quaternion wheelRotation;
            Vector3 wheelPosition;
            wheel.collider.GetWorldPose(out wheelPosition, out wheelRotation);
            wheel.model.transform.position = wheelPosition;
            wheelRotation = wheelRotation * Quaternion.Euler(wheelRotationOffset);
            wheel.model.transform.rotation = wheelRotation;
        }
    }

private void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    private void OnMoveDpad(InputValue value)
    {
        Debug.Log("onmovedpad: " + movement);
        movement = value.Get<Vector2>();
    }

    private void OnMenu()
    {
        print("OnMenu");
    }

    private void OnNorthPress()
    {
        print("OnNorthPress");
    }

    private void OnNorthRelease()
    {
        print("OnNorthRelease");
    }

    private void OnEastPress()
    {
        //print("Powerup");
        PlayerStats ps = gameObject.GetComponent<PlayerStats>();
        if(ps.hasPowerup)
        {
            StartCoroutine(ps.power.UsePowerup(ps.gameObject));
        }
    }

    private void PlayEngineSound()
    {
        if (TryGetComponent(out NavMeshAgent agent))
        {
            float newPitch = agent.velocity.magnitude / agent.speed * +1;
            if (newPitch >= 2.5f)
            {
                newPitch = 2.5f;
            }
            engineSound.pitch = newPitch;
        }
        else
        {
            float newPitch = engineSound.pitch = rb.velocity.magnitude / maxVelocity + 1;
            if (newPitch >= 2.5f)
            {
                newPitch = 2.5f;
            }
            engineSound.pitch = newPitch;
        }


    }

    private void OnEastRelease()
    {
        //print("OnEastRelease");
    }

    private void OnSouthPress()
    {
        handbrake = true;
    }

    private void OnSouthRelease()
    {
        handbrake = false;
    }

    private void OnWestPress()
    {
        print("OnWestPress");
    }

    private void OnWestRelease()
    {
        print("OnWestRelease");
    }
}
