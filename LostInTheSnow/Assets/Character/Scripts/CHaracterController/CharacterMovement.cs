using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class CharacterMovement : MonoBehaviour {
    [SerializeField] private AnimationCurve moveAccelaration;
    [Range(0f, 10f)]
    [SerializeField] private float movementSpeed;
    [Range(1f, 3f)][Tooltip("1 is equal to movementSpeed")]
    [SerializeField] private float sprintMultiplier;
    [Range(0f, 10f)]
    [SerializeField] private float jumpStartSpeed;
    [Range(0f, 3f)]
    [SerializeField] private float fallMultiplier;

    [SerializeField] private bool canJump;
    [SerializeField] private StudioEventEmitter jumpEmitter;

    private float inputH, inputV;
    private bool inputSprint, inputJump, allowedToJump;
    private CharacterController cc;
    private Vector3 moveDirection;
    private Vector3 lastMoveDirection;

    private bool moving = false;
    private bool toggledMoving = false;
    private Timer accTajmer;

    private bool cutsceneLock = false;
    private Vector3 forcedPosition;
    private Vector2 forcedLook;
    private bool forcedMove = false;
    private float t = 0f;
    private int forcedTurn;
    private bool forceRelease;


    private Vector3 hitNormal;
    private bool isSliding;
    [SerializeField] private float slopeLimit = 45;
    [SerializeField] private float friction = 0.7f;


    public float MovementSpeed
    {
        get
        {
            return movementSpeed;
        }
        set
        {
            movementSpeed = value;
        }
    }
    public float SprintMultiplier
    {
        get
        {
            return movementSpeed;
        }
        set
        {
            sprintMultiplier = value;
        }
    }


    // Use this for initialization
    void Start () {
        cc = GetComponent<CharacterController>();
        accTajmer = new Timer(0f);
        forcedPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        isSliding = !((Vector3.Angle(Vector3.up, hitNormal) <= slopeLimit));
        if (!forcedMove && !cutsceneLock)
        {
            CheckInputs();
            CalculateMovement();
            CalculateCurrentSpeed();
            CalculateJump();
            CalculateAirTime();
            ApplyMovement();
        }
        if (forcedMove)
            Forcing();
    }

    public bool CutsceneLock
    {
        get { return cutsceneLock; }
        set { cutsceneLock = value; }
    }

    void CalculateAirTime()
    {
        float dt = Time.deltaTime;

        if (!cc.isGrounded && !isSliding)
        {
            moveDirection.x = lastMoveDirection.x;
            moveDirection.z = lastMoveDirection.z;
        }
        if (cc.velocity.y < 0)
        {

            moveDirection.y += dt * fallMultiplier * Physics.gravity.y;
        }
        else if (cc.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            moveDirection.y += dt * fallMultiplier * Physics.gravity.y;
        }
        else 
        {
            moveDirection.y += dt *Physics.gravity.y;
        }
    }
    
    void CalculateJump()
    {
        if (inputJump && cc.isGrounded && !isSliding)
        {
            //Play jumpSound
            jumpEmitter.Play();
            moveDirection.y = jumpStartSpeed;
        }
    }
    
    void CalculateMovement()
    {
        
        Vector3 lookDir = Camera.main.transform.forward;
        Vector2 dir = new Vector2(inputH, inputV).normalized;
        moveDirection = new Vector3(dir.x, lastMoveDirection.y, dir.y);

    }
    void ApplyMovement()
    {
        moveDirection = transform.TransformDirection(moveDirection);
        ApplySliding();
        cc.Move(moveDirection * Time.deltaTime);
        lastMoveDirection = transform.InverseTransformDirection(moveDirection);
        
        
    }


    private void CalculateCurrentSpeed()
    {
        moveDirection.x *= movementSpeed * getAcceleration();
        moveDirection.z *= movementSpeed * getAcceleration();
        moveDirection.x *= inputSprint ? sprintMultiplier : 1.0f;
        moveDirection.z *= inputSprint ? sprintMultiplier : 1.0f;
    }
    private void CheckInputs()
    {
        if (!cutsceneLock)
        {
            moving = Input.GetButton("Horizontal") == true || Input.GetButton("Vertical") == true ? true : false;
            inputH = Input.GetAxisRaw("Horizontal");
            inputV = Input.GetAxisRaw("Vertical");
            inputSprint = Input.GetButton("Sprint") ? true : false;
            if (canJump) inputJump = Input.GetButtonDown("Jump") ? true : false;
        }
        else
        {
            moving = false;
            inputH = 0f;
            inputV = 0f;
            inputSprint = false;
            inputJump = false;
        }
    }
    public bool getSprint()
    {
        return inputSprint;
    }
    private float getAcceleration()
    {
        float tmp = 0;
        AddTakeTame();
        tmp =  moveAccelaration.Evaluate(accTajmer.Time);
        return tmp > 1 ? 1 : tmp;
    }

    void AddTakeTame()
    {
        if (moving) accTajmer.AddTime(Time.deltaTime);
        else accTajmer.ResetTimer();
    }

    private void ApplySliding()
    {
        if (isSliding && cc.isGrounded)
        {
            moveDirection.x += (1f - hitNormal.y) * hitNormal.x * friction;
            moveDirection.z += (1f - hitNormal.y) * hitNormal.z * friction;
        }
    }


    public void ForceMovement(Vector3 targetPosition, Vector2 targetLook, bool release)
    {
        forcedPosition = targetPosition;
        forcedLook = targetLook;
        while (forcedLook.x < 0)
            forcedLook.x += 360;
        while (forcedLook.x >= 360)
            forcedLook.x -= 360;
        forcedMove = true;
        cutsceneLock = true;
        CameraController camCon = Camera.main.GetComponent<CameraController>();
        if (camCon != null) camCon.CutsceneLock = true;
        ChracterInteract charInteract = GetComponent<ChracterInteract>();
        if (charInteract != null) charInteract.CutsceneLock = true;
        float hAngle = forcedLook.x - camCon.getLook().x;
        if (hAngle == 0)
            forcedTurn = 0;
        else if (hAngle > 0)
        {
            if (hAngle >= 180)
                forcedTurn = -1;
            else
                forcedTurn = 1;
        }
        else
        {
            if (hAngle <= -180)
                forcedTurn = 1;
            else
                forcedTurn = -1;
        }
        forceRelease = release;
    }

    private void Forcing()
    {
        transform.position = Vector3.MoveTowards(transform.position, forcedPosition, 0.2f);
        CameraController camCon = Camera.main.GetComponent<CameraController>();
        ChracterInteract charInteract = GetComponent<ChracterInteract>();
        Vector2 newLook = camCon.getLook();
        float turnSpeed = 4 * 60 * Time.deltaTime;
        newLook.y = Mathf.MoveTowards(newLook.y, forcedLook.y, turnSpeed);
        if (newLook.x == forcedLook.x)
            forcedTurn = 0;
        newLook.x += forcedTurn * turnSpeed;
        if (newLook.x >= 360) newLook.x -= 360;
        else if (newLook.x < 0) newLook.x += 360;
        if (forcedTurn == 1)
        {
            if (forcedLook.x >= 360 - turnSpeed)
            {
                if (newLook.x < turnSpeed)
                    newLook.x = forcedLook.x;
            }
            if (newLook.x > forcedLook.x)
                if (newLook.x - forcedLook.x <= turnSpeed)
                    newLook.x = forcedLook.x;
        }
        else if (forcedTurn == -1)
        {
            if (forcedLook.x <= turnSpeed)
            {
                if (newLook.x > 360 - turnSpeed)
                    newLook.x = forcedLook.x;
            }
            if (newLook.x < forcedLook.x)
                if (forcedLook.x - newLook.x <= turnSpeed)
                    newLook.x = forcedLook.x;
        }
        camCon.setLook(newLook);
        if (transform.position == forcedPosition && camCon.getLook() == forcedLook)
        {
            forcedMove = false;
            if (forceRelease)
            {
                cutsceneLock = false;
                camCon.CutsceneLock = false;
                charInteract.CutsceneLock = false;
            }
        }
    }

    public bool GetForcedMove()
    {
        return forcedMove;
    }

    public void CutsceneRelease()
    {
        CameraController camCon = Camera.main.GetComponent<CameraController>();
        ChracterInteract charInteract = GetComponent<ChracterInteract>();
        cutsceneLock = false;
        if (camCon != null) camCon.CutsceneLock = false;
        if (charInteract != null) charInteract.CutsceneLock = false;
    }


    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hitNormal = hit.normal;
    }

    public void setForceRelease(bool v)
    {
        forceRelease = v;
    }

}
