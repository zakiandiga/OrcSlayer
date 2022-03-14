using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private InputActionReference run;
    [SerializeField] private InputActionReference jump;
    [SerializeField] private InputActionReference sideStep;
    [SerializeField] private InputActionReference normalAttack;
    [SerializeField] private InputActionReference specialAttack;

    public float MoveAxisX => run.action.ReadValue<float>();

    public float MoveAxisZ => sideStep.action.ReadValue<float>();

    public bool IsJumping { get; private set; }

    public bool NormalAttack => normalAttack.action.triggered;

    public bool SpecialAttack => specialAttack.action.triggered;

    private void OnEnable()
    {
        PlayerInputSwitch(true);
    }
    private void OnDisable()
    {
        PlayerInputSwitch(false);
    }

    public void PlayerInputSwitch(bool enabling)
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
