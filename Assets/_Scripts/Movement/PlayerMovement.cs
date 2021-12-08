using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Input References")]
    [SerializeField] private InputActionReference run;
    [SerializeField] private InputActionReference jump;
    [SerializeField] private InputActionReference normalAttack;
    [SerializeField] private InputActionReference specialAttack;   

    private CharacterController control;
    private Animator animator;
    [SerializeField] private GameObject playerPivot;
    private PlayerAnimationManager animationManager;

    #region Movement Property
    [Header("Horizontal Properties")]
    [SerializeField] private float groundSpeed = 15.0f;
    [SerializeField] private float airSpeed = 10.0f;
    [SerializeField] private float turningSpeed = 700f;
    [Range(0, 100)]
    [SerializeField] private float accelTime = 15;
    [Range(0, 100)]
    [SerializeField] private float deccelTime = 5f;
    [Range(0, 100)]
    [SerializeField] private float driftingTime = 5f;
    private float smoothSpeed = 15;
    private float runningAxis;
    private float tempAxis;
    private float runSpeed; //modifiy accordingly

    float currentSpeed = 0;
    float accelRate = 0.7f;

    private float smoothMovement = 0;
    private float smoothInputVelocity; //used for ref only

    private bool isTurning = false;
    private bool faceRight = true;

    [Header("Vertical Properties")]
    [Range(0,20)]
    [SerializeField] private float jumpHeight;
    [SerializeField] private int jumpCredit = 2;
    [SerializeField] private float gravityValue;
    [SerializeField] private float gravityMultiplier = 1.8f;
    private float jumpConst = -1.0f;

    private Vector3 playerVelocity = Vector3.zero;
    
    Vector3 moveDirection;
    #endregion

    #region GroundCheck Related
    public Transform groundChecker;
    public float checkerRadius;
    public LayerMask checkerMask;
    private bool isGrounded;
    #endregion

    public enum PlayerState
    {
        idle,
        onAir,
        run,
        dead,
        onMenu
    }

    private PlayerState playerState = PlayerState.idle;

    private enum MovespdState
    {
        stop,
        turning,
        acceleration,
        topSpeed,
        decceleration,
        drifting //changing the runningAxis during decceleration
    }
    private MovespdState movespdState = MovespdState.stop;

    private void OnEnable()
    {
        InputActionSwitch(true);
    }

    private void OnDisable()
    {
        InputActionSwitch(false);
    }


    // Start is called before the first frame update
    void Start()
    {
        control = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        animationManager = GetComponentInChildren<PlayerAnimationManager>();
    }

    private void InputActionSwitch(bool isEnabling)
    {
        if (isEnabling)
        {
            run.action.Enable();
            jump.action.Enable();
            normalAttack.action.Enable();
            specialAttack.action.Enable();

            run.action.performed += Move;
            run.action.canceled += StopMove;
            jump.action.performed += Jump;
        }
        else if (!isEnabling)
        {
            run.action.Disable();
            jump.action.Disable();
            normalAttack.action.Disable();
            specialAttack.action.Disable();

            run.action.performed -= Move;
            run.action.canceled -= StopMove;
            jump.action.performed -= Jump;
        }
    }

    private void Move(InputAction.CallbackContext context)
    {
        //things to add when move starts
        
    }

    private void StopMove(InputAction.CallbackContext context)
    {
        //Things to add when move stops

    }

    private void Jump(InputAction.CallbackContext context)
    {
        
        if (isGrounded || (!isGrounded && jumpCredit > 0))
        {
            playerVelocity.y = -1; //reset the down velocity for air jump
            jumpCredit -= 1;
            isGrounded = false;
            playerVelocity.y += Mathf.Sqrt(jumpHeight * jumpConst * gravityValue);

            animator.SetTrigger("jump");
        }

        if (playerState != PlayerState.onAir)
            playerState = PlayerState.onAir;
    }

    private void Fall()
    {        
        if (isGrounded)
        {           
            isGrounded = false;
            jumpCredit -= 1;
        }

        if (playerState != PlayerState.onAir)
            playerState = PlayerState.onAir;

        playerVelocity.y += gravityValue * Time.deltaTime;
        //Debug.Log("OLD " + playerVelocity.y);
    }

    private void JumpRefresh()
    {
        jumpCredit = 2;
    }

    private bool GroundCheck()
    {
        return Physics.CheckSphere(groundChecker.position, checkerRadius, checkerMask);
    }

    // Update is called once per frame
    void Update()
    {
        runningAxis = run.action.ReadValue<float>();

        switch (playerState)
        {
            case PlayerState.idle:
                //something
                if (GroundCheck() && playerVelocity.y < 0 && !isGrounded)
                {
                    if (playerState == PlayerState.onAir)
                        playerState = PlayerState.idle;

                    playerVelocity.y = -1;
                    JumpRefresh();
                    isGrounded = true;
                }

                if (!GroundCheck())
                {
                    Fall(); //Exit to onAir in this function and Jump()
                }

                //Exit to move
                if (runningAxis > 0 || runningAxis < 0)
                {
                    playerState = PlayerState.run;

                    //if(movespdState == MovespdState.stop)
                    //    FaceCheck(runningAxis);                    
                }
                break;

            case PlayerState.run:
                //something
                if (runSpeed != groundSpeed)
                {
                    runSpeed = groundSpeed;                    
                }
                    

                if (GroundCheck())
                {
                    animator.SetBool("isRunning", true);
                }


                else if (!GroundCheck())
                {
                    animator.SetBool("isRunning", false);
                    Fall(); //Exit to onAir in this function and Jump()
                }

                
                //Exit to Idle
                if (runningAxis == 0)
                {
                    animator.SetBool("isRunning", false);
                    playerState = PlayerState.idle;
                }
                break;

            case PlayerState.onAir:
                //something
                if (runSpeed != airSpeed)
                    runSpeed = airSpeed;

                
                if(!GroundCheck())
                {
                    Fall();
                }
                
                //Exit to Idle
                if (GroundCheck() && playerVelocity.y < 0 && !isGrounded)
                {
                    playerVelocity.y = -1;
                    JumpRefresh();
                    isGrounded = true;
                    animator.SetBool("isRunning", false);
                    playerState = PlayerState.idle;
                }
                break;

            case PlayerState.onMenu:
                //something
                break;
        }

        if (isTurning)
        {
            Turning();     
        }

        RunningState();

    }

    private void FaceCheck(float axis)
    {
        if (axis > 0 && !faceRight)
        {
            //Modify face direction so the turning will be clockwise
            Vector3 faceDirection = playerPivot.transform.localEulerAngles;
            faceDirection.y += 1;
            playerPivot.transform.localEulerAngles = faceDirection;

            moveDirection = new Vector3(0, 0, axis);
            moveDirection.Normalize();
            animator.SetTrigger("turn");
            faceRight = true;
            isTurning = true;            
        }
        else if (axis < 0 && faceRight)
        {
            moveDirection = new Vector3(0, 0, axis);
            moveDirection.Normalize();
            animator.SetTrigger("turn");
            faceRight = false;
            isTurning = true;            
        } 
    }

    private void Turning()
    {
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
        playerPivot.transform.rotation = Quaternion.RotateTowards(playerPivot.transform.rotation, targetRotation, turningSpeed * Time.deltaTime);

        if (playerPivot.transform.rotation == targetRotation)
        {
            isTurning = false;            
        }            
    }

    private void RunningState()
    {
        #region movespd State

        switch (movespdState)
        {            
            case MovespdState.stop:
                tempAxis = 0;
                smoothMovement = 0;

                if (runningAxis > 0 || runningAxis < 0)
                {
                    Debug.Log("ACCELERATING");
                    tempAxis = runningAxis;

                    if(GroundCheck())
                        FaceCheck(runningAxis);

                    movespdState = MovespdState.acceleration;
                }
                break;

            case MovespdState.acceleration:
                smoothMovement = Mathf.SmoothDamp(smoothMovement, tempAxis, ref smoothInputVelocity, (accelTime / 100));

                if (GroundCheck())
                    FaceCheck(runningAxis);

                if ((tempAxis == 1 && smoothMovement > 0.98f) || (tempAxis == -1 && smoothMovement < -0.98f))
                {
                    //Debug.Log("TOP SPEED");
                    
                    movespdState = MovespdState.topSpeed;
                }
                else if (runningAxis == 0f)
                {

                    //Debug.Log("DECCELERATING" + tempAxis);
                    movespdState = MovespdState.decceleration;
                }
                break;

            case MovespdState.topSpeed:
                smoothMovement = tempAxis;

                if (GroundCheck())
                    FaceCheck(runningAxis);

                if ((tempAxis == 1 && runningAxis <= 0) || (tempAxis == -1 && runningAxis >=0))
                {
                    //Debug.Log("DECCELERATING" + tempAxis);
                    movespdState = MovespdState.decceleration;
                }
                break;

            case MovespdState.decceleration:
                smoothMovement = Mathf.SmoothDamp(smoothMovement, 0, ref smoothInputVelocity, (deccelTime / 100));

                if(runningAxis != 0)
                {
                    if (GroundCheck())
                        FaceCheck(runningAxis);

                    //Debug.Log("DRIFTING");
                    movespdState = MovespdState.drifting;
                }

                if ((tempAxis == 1 && smoothMovement < 0.01f) || (tempAxis == -1 && smoothMovement > -0.01f))
                {
                    //Debug.Log("STOP");
                    
                    movespdState = MovespdState.stop;
                }
                break;
            case MovespdState.drifting:
                smoothMovement = Mathf.SmoothDamp(smoothMovement, 0, ref smoothInputVelocity, (driftingTime / 100));
                
                if ((tempAxis == 1 && smoothMovement < 0.01f) || (tempAxis == -1 && smoothMovement > -0.01f))
                {
                    //Debug.Log("STOP");
                    
                    movespdState = MovespdState.stop;
                }
                break;
        }

        #endregion      
        
        playerVelocity.x = runSpeed * smoothMovement;
        //control.Move(playerVelocity * Time.deltaTime);
        
    }

    private void LateUpdate()
    {
        control.Move(playerVelocity * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundChecker.position, checkerRadius);
    }
}
