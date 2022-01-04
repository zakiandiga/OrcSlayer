using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM.Controllers;

public class CustomCharacterMovement : BaseCharacterController
{
    [SerializeField] private InputHandler input;
    protected override void HandleInput()
    {
        /*
        if (Input.GetKeyDown(KeyCode.P))
            pause = !pause;
        */

        // Handle user input

        moveDirection = new Vector3
        {
            x = input.MoveAxisX,
            y = 0.0f,
            z = 0.0f
        };

        jump = input.IsJumping;

        crouch = input.SpecialAttack;
    }
}
