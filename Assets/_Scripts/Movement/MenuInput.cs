using UnityEngine;
using UnityEngine.InputSystem;

public class MenuInput : MonoBehaviour
{
    [SerializeField] private InputActionReference directional, accept, cancel, slideLeft, slideRight;

    public Vector2 DirectionalAxis => directional.action.ReadValue<Vector2>();

    public bool AcceptPressed => accept.action.triggered;

    public bool CancelPressed => cancel.action.triggered;

    private void OnEnable()
    {
        MenuInputSwitch(true);
    }

    private void OnDisable()
    {
        MenuInputSwitch(false);    
    }

    private void MenuInputSwitch(bool enabling)
    {
        if(enabling)
        {
            directional.action.Enable();
            accept.action.Enable();
            cancel.action.Enable();
        }

        else if (!enabling)
        {
            directional.action.Disable();
            accept.action.Disable();
            cancel.action.Disable();
        }
    }
}
