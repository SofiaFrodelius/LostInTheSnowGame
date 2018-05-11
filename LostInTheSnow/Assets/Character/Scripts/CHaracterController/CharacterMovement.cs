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

    // Use this for initialization
    void Start () {
        cc = GetComponent<CharacterController>();
        accTajmer = new Timer(0f);
        forcedPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!forcedMove)
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

        if (!cc.isGrounded)
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
        if (inputJump && cc.isGrounded)
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

    public void ForceMovement(Vector3 targetPosition, Vector2 targetLook)
    {
        forcedPosition = targetPosition;
        forcedLook = targetLook;
        forcedMove = true;
        cutsceneLock = true;
        CameraController camCon = Camera.main.GetComponent<CameraController>();
        camCon.CutsceneLock = true;
        float hAngle = targetLook.x - camCon.getLook().x;
        if (hAngle >= 0)
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
        Debug.Log("ForceMovement");
    }

    private void Forcing()
    {
        transform.position = Vector3.MoveTowards(transform.position, forcedPosition, 1.5f);
        CameraController camCon = Camera.main.GetComponent<CameraController>();
        Vector2 newLook = camCon.getLook();
        newLook.y = Mathf.MoveTowards(newLook.y, forcedLook.y, 2);
        int turnSpeed = 4;
        newLook.x += forcedTurn * turnSpeed;
        if (forcedTurn == 1)
        {
            if (newLook.x > forcedLook.x)
                if (newLook.x - forcedLook.x <= turnSpeed)
                    newLook.x = forcedLook.x;
        }
        else
        {
            if (newLook.x < forcedLook.x)
                if (forcedLook.x - newLook.x <= turnSpeed)
                    newLook.x = forcedLook.x;
        }
        camCon.setLook(newLook);
        if (transform.position == forcedPosition && camCon.getLook() == forcedLook)
        {
            forcedMove = false;
            cutsceneLock = false;
            camCon.CutsceneLock = false;
        }
    }

    public bool GetForcedMove()
    {
        return forcedMove;
    }
}
