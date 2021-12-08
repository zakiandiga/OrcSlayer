using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private InputActionReference run;
    [SerializeField] private InputActionReference jump;
    [SerializeField] private InputActionReference sideStep;
    [SerializeField] private InputActionReference normalAttack;
    [SerializeField] private InputActionReference specialAttack;

    public float MoveAxisX
    {
        get
        {
            return run.action.ReadValue<float>();
        }
        private set
        {
            run.action.ReadValue<float>();
        }
    }

    public float MoveAxisZ
    {
        get
        {
            return sideStep.action.ReadValue<float>();
        }
        private set
        {
            sideStep.action.ReadValue<float>();
        }
    }

    public bool IsJumping { get; set; }
    public bool NormalAttack
    {
        get
        {
            return normalAttack.action.triggered;
        }
        private set { }

    }

    public bool SpecialAttack
    {
        get
        {
            return specialAttack.action.triggered;
        }
        private set { }
    }



    private void OnEnable()
    {
        InputActionSwitch(true);
    }
    private void OnDisable()
    {
        InputActionSwitch(false);
    }

    public void InputActionSwitch(bool enabling)
    {
        if (enabling)
        {
            run.action.Enable();
            sideStep.action.Enable();
            jump.action.Enable();
            normalAttack.action.Enable();
            specialAttack.action.Enable();

            jump.action.started += Jump;
            jump.action.canceled += Jump;

        }
        else if (!enabling)
        {
            run.action.Disable();
            sideStep.action.Disable();
            jump.action.Disable();
            normalAttack.action.Disable();
            specialAttack.action.Disable();

            jump.action.started -= Jump;
            jump.action.canceled -= Jump;
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsJumping = true;
        }
        if (context.canceled)
        {
            IsJumping = false;
        }
    }

    public void JumpStop() => IsJumping = false;

}
/*
    private float timer;
    private float delayTime = 1.2f;
    private bool isCounting = false;
    private void Update()
    {
        if (isCounting == true)
        {

            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                isCounting = false;
                Debug.Log("Ready for combo!");
            }
        }
            

        if(normalAttack.action.triggered)
        {
            if(!isCounting)
            {
                Debug.Log("Attack!");
                timer = delayTime;
                isCounting = true;
            }
            else if (isCounting)
            {
                Debug.Log("Attack on delay!");
            }
        }
    }
*/
