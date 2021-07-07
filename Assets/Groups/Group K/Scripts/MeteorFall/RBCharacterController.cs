using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RBCharacterController : MonoBehaviour
{
    #region Fields
    [SerializeField] private bool inputEnabled = true;
    [SerializeField] private float _moveForce = 10.0f;
    [SerializeField] private float _seperationForce = 5.0f;
    [SerializeField] private float _jumpForce = 10.0f;
    [SerializeField] private float _hitForce = 20.0f;
    public int _mashLimit = 10;
    [SerializeField] private float GRAVITY = 1.0f;
    [SerializeField] private float _meteorKnockbackMultiplier = 1.5f;
    [SerializeField] private float _moveThreshold = 0.05f;
    [SerializeField] private float _kickRange = 0.5f;
    [SerializeField] private float _kickCooldown = 0.2f;
    [SerializeField] private float _distToGround = 0.01f;
    public float _stepHeight = 0.3f;
    [SerializeField] private float _smoothStep = 0.15f;

    [Range(0, 500)] public float maxRotateSpeed = 150f;
    public GameObject Minifig;

    [SerializeField] private GameObject _stunnedParticles;
    [SerializeField] private GameObject _outParticles;

    [Header("Audio")]

    public List<AudioClip> stepAudioClips = new List<AudioClip>();
    public AudioClip jumpAudioClip;
    public AudioClip doubleJumpAudioClip;
    public AudioClip landAudioClip;
    public AudioClip explodeAudioClip;

    private Rigidbody _rb;
    private Animator animator;
    private AudioSource audioSource;
    private Collider m_Collider;
    private BoxCollider _colly;
    private RaycastHit m_Hit;
    public Vector2 _moveDirection;

    bool isJumping;
    bool isGrounded;
    public bool isStunned;
    bool isClimbing;
    bool climbed;
    int mashCounter;
    bool wasGrounded;
    float airborneTime;
    float moveDelta;
    Vector3 lastPosition;
    bool hasMoved;
    float kickTime;
    float speed;
    float rotateSpeed;
    bool stopSpecial;
    bool cancelSpecial;
    bool m_HitDetect;
    bool stepped;

    float externalRotation;
    Vector3 externalMotion;
    Transform groundedTransform;
    Vector3 groundedLocalPosition;
    Vector3 oldGroundedPosition;
    Quaternion oldGroundedRotation;

    int speedHash = Animator.StringToHash("Speed");
    int rotateSpeedHash = Animator.StringToHash("Rotate Speed");
    int groundedHash = Animator.StringToHash("Grounded");
    int jumpHash = Animator.StringToHash("Jump");
    int playSpecialHash = Animator.StringToHash("Play Special");
    int cancelSpecialHash = Animator.StringToHash("Cancel Special");
    int specialIdHash = Animator.StringToHash("Special Id");

    Action<bool> onSpecialComplete;
    public enum SpecialAnimation
    {
        AirGuitar = 0,
        Ballerina = 1,
        Crawl = 2,
        CrawlOnWallLeft = 3,
        CrawlOnWallRight = 4,
        Dance = 5,
        Flexing = 6,
        Flip_No_Y_Axis = 7,
        HatSwap = 8,
        HatSwap2 = 9,
        Idle_Light = 10,
        // Unused 11
        IdleHeavy = 12,
        IdleImpatient = 13,
        Jump = 14,
        Jump_GoingDown = 15,
        Jump_GoingUp = 16,
        Jump_LandingBounce = 17,
        Jump_Midair = 18,
        Jump_NoLanding_No_Y_Axis = 19,
        Jump_wBounce = 20,
        KickRightFoot = 21,
        Laughing = 22,
        LeftHandReaction = 23,
        LegReactive = 24,
        LookAbove = 25,
        LookingAround = 26,
        LookingDown = 27,
        LookingUp = 28,
        MoonWalk = 29,
        PantsSwap = 30,
        RightHandReactive = 31,
        Run = 32,
        // Unused 33 
        Skipping = 34,
        Sleeping = 35,
        Slide = 36,
        Sneezing = 37,
        Snoozing = 38,
        Spiderman = 39,
        Spin = 40,
        Stretching = 41,
        Superman_Flying = 42,
        Superman_Setoff = 43,
        Torso_Reactive = 44,
        Turn_90_Left = 45,
        Turn_90_Right = 46,
        Walk = 47,
        WalkBackwards = 48,
        Wave = 49
    }
    #endregion

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        animator = Minifig.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        m_Collider = GetComponent<Collider>();
        _colly = transform.GetComponent<BoxCollider>();
    }

    private void Start()
    {
        string controlScheme = GetComponent<PlayerInput>().defaultControlScheme;
        GetComponent<PlayerInput>().SwitchCurrentControlScheme(controlScheme, Keyboard.current);
    }

    private void FixedUpdate()
    {
        if(isStunned && mashCounter <= 0)
        {
            isStunned = false;
            inputEnabled = true;
            stopSpecial = true;
            _stunnedParticles.SetActive(false);
        }
        else if (isStunned)
        {
            PlaySpecialAnimation(SpecialAnimation.Crawl);
        }

        if (!isStunned && Time.time > kickTime)
        {
            stopSpecial = true;
        }
        
        if(inputEnabled)
        {
            if(isJumping)
            {
                _rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
                isJumping = false;
                stopSpecial = false;
            }
            if(!hasMoved || moveDelta > _moveThreshold)
            {
                _rb.AddForce(new Vector3(_moveDirection.x, 0, _moveDirection.y) * _moveForce);
                if(_moveDirection.magnitude > 0)
                {
                    hasMoved = true;
                }
                else
                {
                    hasMoved = false;
                }
            }
            else
            {
                hasMoved = false;
            }
            
        }

        #region Rotation
        var right = Vector3.right;
        var forward = Vector3.forward;
        if (Camera.main)
        {
            right = Camera.main.transform.right;
            right.y = 0.0f;
            right.Normalize();
            forward = Camera.main.transform.forward;
            forward.y = 0.0f;
            forward.Normalize();
        }

        var targetSpeed = right * _rb.velocity.x;
        targetSpeed += forward * _rb.velocity.z;
        rotateSpeed = 0.0f;
        if (targetSpeed.sqrMagnitude > 0.0f)
        {
            var localTargetSpeed = transform.InverseTransformDirection(targetSpeed);
            var angleDiff = Vector3.SignedAngle(Vector3.forward, localTargetSpeed.normalized, Vector3.up);

            if (angleDiff > 0.0f)
            {
                rotateSpeed = maxRotateSpeed;
            }
            else if (angleDiff < 0.0f)
            {
                rotateSpeed = -maxRotateSpeed;
            }

            // Assumes that x > NaN is false - otherwise we need to guard against Time.deltaTime being zero.
            if (Mathf.Abs(rotateSpeed) > Mathf.Abs(angleDiff) / Time.deltaTime)
            {
                rotateSpeed = angleDiff / Time.deltaTime;
            }
        }
        if(isGrounded)
        {
            // Handle external motion.
            externalMotion = Vector3.zero;
            externalRotation = 0.0f;
            // Apply external motion and rotation.
            if (groundedTransform && Time.deltaTime > 0.0f)
            {
                var newGroundedPosition = groundedTransform.TransformPoint(groundedLocalPosition);
                externalMotion = (newGroundedPosition - oldGroundedPosition) / Time.deltaTime;
                oldGroundedPosition = newGroundedPosition;

                var newGroundedRotation = groundedTransform.rotation;
                // FIXME Breaks down if rotating more than 180 degrees per frame.
                var diffRotation = newGroundedRotation * Quaternion.Inverse(oldGroundedRotation);
                var rotatedRight = diffRotation * Vector3.right;
                rotatedRight.y = 0.0f;
                if (rotatedRight.magnitude > 0.0f)
                {
                    rotatedRight.Normalize();
                    externalRotation = Vector3.SignedAngle(Vector3.right, rotatedRight, Vector3.up) / Time.deltaTime;
                }
                oldGroundedRotation = newGroundedRotation;
            }
        }
        #endregion

        isGrounded = GroundCheck();

        if (!isGrounded)
        {
            _rb.AddForce(new Vector3(0, GRAVITY, 0));
            groundedTransform = null;
            stopSpecial = false;
        }

        // If becoming grounded by this Move
        if (!wasGrounded && isGrounded && !climbed)
        {
            if (landAudioClip)
            {
                audioSource.PlayOneShot(landAudioClip);
            }
            _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
        }

        moveDelta = Vector3.Distance(lastPosition, transform.position);
        lastPosition = transform.position;
        isClimbing = climbed;
        climbed = false;
        wasGrounded = isGrounded;
        speed = _rb.velocity.magnitude;
        transform.Rotate(0, (rotateSpeed + externalRotation) * Time.deltaTime, 0);

        // Stop special if requested.
        cancelSpecial = stopSpecial;
        stopSpecial = false;

        // Update animation - delay airborne animation slightly to avoid flailing arms when falling a short distance.
        animator.SetBool(cancelSpecialHash, cancelSpecial);
        animator.SetFloat(speedHash, speed);
        animator.SetFloat(rotateSpeedHash, rotateSpeed);
        animator.SetBool(groundedHash, isGrounded);
    }

    public void Kick()
    {
        m_Collider = GetComponent<Collider>();
        //Test to see if there is a hit using a BoxCast
        //Calculate using the center of the GameObject's Collider(could also just use the GameObject's position), half the GameObject's size, the direction, the GameObject's rotation, and the maximum distance as variables.
        //Also fetch the hit data
        m_HitDetect = Physics.BoxCast(transform.position, transform.lossyScale/2, 
            transform.forward, out m_Hit, transform.rotation, _kickRange);
        if (m_HitDetect)
        {
            if(m_Hit.collider.CompareTag("Player"))
            {
                m_Hit.collider.GetComponent<RBCharacterController>().GetHit(-m_Hit.normal.normalized);
            }
        }
    }

    public void TryKick()
    {
        if (isGrounded && !isStunned && Time.time > kickTime)
        {
            PlaySpecialAnimation(SpecialAnimation.KickRightFoot, explodeAudioClip);

            Kick();
            kickTime = Time.time + _kickCooldown;
        }
    }

    public void TryJump()
    {
        if (!isStunned)
        {
            // Check if player is jumping.
            if (isGrounded)
            {
                isJumping = true;
                PlaySpecialAnimation(SpecialAnimation.Flip_No_Y_Axis, jumpAudioClip);
            }
        }
    }

    public void Explode(Vector3 source)
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.z);
        Vector2 posS = new Vector2(source.x, source.z);
        Vector2 dir = pos - posS;
        dir.Normalize();
        _rb.AddForce(new Vector3(dir.x, 0, dir.y) * _hitForce * _meteorKnockbackMultiplier, ForceMode.Impulse);
    }

    public void Die()
    {
        inputEnabled = false;
        GameObject part = Instantiate(_outParticles, transform.position, Quaternion.identity, null);
        Destroy(part, 4.0f);
        gameObject.SetActive(false);
    }

    public void GetHit(Vector3 direction)
    {
        _rb.AddForce(direction * _hitForce, ForceMode.Impulse);
    }

    public void GetStunned()
    {
        if(GroundCheck() && !isStunned)
        {
            isStunned = true;
            inputEnabled = false;
            mashCounter = _mashLimit;
            PlaySpecialAnimation(SpecialAnimation.Crawl);
            _stunnedParticles.SetActive(true);
        }
    }

    public void UnstunInput()
    {
        if (isStunned)
        {
            mashCounter--;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("K_Ground") && (GroundCheck() || isClimbing))
        {
            Vector3 lowerCoord = new Vector3(_colly.bounds.center.x, _colly.bounds.min.y + 0.01f, _colly.bounds.center.z);
            Vector3 higherCoord = new Vector3(_colly.bounds.center.x, _colly.bounds.min.y + _stepHeight, _colly.bounds.center.z);
            ContactPoint[] cs = new ContactPoint[collision.contactCount];
            collision.GetContacts(cs);
            foreach (ContactPoint c in cs)
            {
                Vector3 norm = new Vector3(c.normal.x, 0, c.normal.z);
                Helper_StepRaycast(lowerCoord, higherCoord, -norm, 0.1f);
            }
        }
        if(collision.collider.CompareTag("Player") || collision.collider.CompareTag("K_Prop"))
        {
            Vector3 dir = transform.position - collision.collider.transform.position;
            _rb.AddForce(dir * _seperationForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionStay(Collision collision)
    {

        if (collision.collider.CompareTag("K_Ground") && (GroundCheck() || isClimbing))
        {
            Vector3 lowerCoord = new Vector3(_colly.bounds.center.x, _colly.bounds.min.y + 0.01f, _colly.bounds.center.z);
            Vector3 higherCoord = new Vector3(_colly.bounds.center.x, _colly.bounds.min.y + _stepHeight, _colly.bounds.center.z);
            ContactPoint[] cs = new ContactPoint[collision.contactCount];
            collision.GetContacts(cs);
            foreach (ContactPoint c in cs)
            {
                Vector3 norm = new Vector3(c.normal.x, 0, c.normal.z);
                Helper_StepRaycast(lowerCoord, higherCoord, -norm, 0.1f);
            }
        }
    }

    private void Helper_StepRaycast(Vector3 lCoord, Vector3 hCoord, Vector3 dir, float dist)
    {
        if(!climbed)
        {
            RaycastHit lowerHit;
            if (Physics.Raycast(lCoord, dir, out lowerHit, _colly.bounds.extents.x + dist))
            {
                RaycastHit upperHit;
                if (!Physics.Raycast(hCoord, dir, out upperHit, _colly.bounds.extents.x + dist))
                {
                    _rb.position -= new Vector3(0f, -_smoothStep, 0f);
                    climbed = true;
                }
            }
        }
        //Debug.DrawRay(hCoord, dir.normalized * (_colly.bounds.extents.x + dist), Color.red);
        //Debug.DrawRay(lCoord, dir.normalized * (_colly.bounds.extents.x + dist), Color.blue);
    }

    public bool GroundCheck()
    {
        Vector3 size = _colly.size;
        Vector3 center = new Vector3(_colly.center.x, _colly.center.y - size.y / 2.1f, _colly.center.z);
        float factor = 0.05f;

        Vector3 vertex1 = new Vector3(center.x + size.x / 2 - factor, center.y, center.z + size.z / 2 - factor);
        Vector3 vertex2 = new Vector3(center.x - size.x / 2 + factor, center.y, center.z - size.z / 2 + factor);
        Vector3 vertex3 = new Vector3(center.x + size.x / 2 - factor, center.y, center.z - size.z / 2 + factor);
        Vector3 vertex4 = new Vector3(center.x - size.x / 2 + factor, center.y, center.z + size.z / 2 - factor);
        Vector3 vertex5 = new Vector3(center.x + size.x / 2 - factor, center.y, center.z);
        Vector3 vertex6 = new Vector3(center.x - size.x / 2 + factor, center.y, center.z);
        Vector3 vertex7 = new Vector3(center.x, center.y, center.z - size.z / 2 + factor);
        Vector3 vertex8 = new Vector3(center.x, center.y, center.z + size.z / 2 - factor);

        bool centerGround = Physics.Raycast(transform.TransformPoint(center), -transform.up, _distToGround);
        bool c1Ground = Physics.Raycast(transform.TransformPoint(vertex1), -transform.up, _distToGround);
        bool c2Ground = Physics.Raycast(transform.TransformPoint(vertex2), -transform.up, _distToGround);
        bool c3Ground = Physics.Raycast(transform.TransformPoint(vertex3), -transform.up, _distToGround);
        bool c4Ground = Physics.Raycast(transform.TransformPoint(vertex4), -transform.up, _distToGround);
        bool c5Ground = Physics.Raycast(transform.TransformPoint(vertex5), -transform.up, _distToGround);
        bool c6Ground = Physics.Raycast(transform.TransformPoint(vertex6), -transform.up, _distToGround);
        bool c7Ground = Physics.Raycast(transform.TransformPoint(vertex7), -transform.up, _distToGround);
        bool c8Ground = Physics.Raycast(transform.TransformPoint(vertex8), -transform.up, _distToGround);

        return (centerGround || c1Ground || c2Ground || c3Ground || c4Ground || c5Ground || c6Ground || c7Ground || c8Ground);
    }

    #region Input Handling
    // Input ------------------------------------------------------------------------------------------


    private void OnMove(InputValue value)
    {
        _moveDirection = value.Get<Vector2>();
    }

    private void OnMoveDpad(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        input.Normalize();
        _moveDirection = input;
    }

    private void OnMenu()
    {
        print("OnMenu");
    }

    private void OnNorthPress()
    {
        UnstunInput();
    }

    private void OnNorthRelease()
    {

    }

    private void OnEastPress()
    {
        TryKick();
    }

    private void OnEastRelease()
    {

    }

    private void OnSouthPress()
    {
        TryJump();
    }

    private void OnSouthRelease()
    {

    }

    private void OnWestPress()
    {

    }

    private void OnWestRelease()
    {

    }

    #endregion

    // Animation event.
    public void StepFoot()
    {
        if (!stepped)
        {
            if (stepAudioClips.Count > 0)
            {
                var stepAudioClip = stepAudioClips[UnityEngine.Random.Range(0, stepAudioClips.Count)];
                if (stepAudioClip)
                {
                    audioSource.PlayOneShot(stepAudioClip);
                }
            }
        }
        stepped = true;
    }

    // Animation event.
    public void LiftFoot()
    {
        stepped = false;
    }

    public void PlaySpecialAnimation(SpecialAnimation animation, AudioClip specialAudioClip = null, Action<bool> onSpecialComplete = null)
    {
        animator.SetBool(playSpecialHash, true);
        animator.SetInteger(specialIdHash, (int)animation);

        if (specialAudioClip)
        {
            audioSource.PlayOneShot(specialAudioClip);
        }

        this.onSpecialComplete = onSpecialComplete;
    }

    /// <summary>
    /// Do not call this directly.
    /// Called from SpecialAnimationBehaviour to signal that a special animation finished.
    /// </summary>
    public void SpecialAnimationFinished()
    {
        // Do callback.
        onSpecialComplete?.Invoke(animator.GetBool(cancelSpecialHash));
    }
}
