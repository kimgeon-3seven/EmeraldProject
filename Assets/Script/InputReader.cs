using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    [Header("Input Action References")]
    public InputActionReference moveAction;
    public InputActionReference jumpAction;
    public InputActionReference attackAction;

    public Vector2 MoveInput => moveAction != null ? moveAction.action.ReadValue<Vector2>() : Vector2.zero;
    public bool JumpPressed => jumpAction != null && jumpAction.action.WasPressedThisFrame();
    public bool AttackPressed => attackAction != null && attackAction.action.WasPressedThisFrame();

    private void OnEnable()
    {
        if (moveAction != null) moveAction.action.Enable();
        if (jumpAction != null) jumpAction.action.Enable();
        if (attackAction != null) attackAction.action.Enable();
    }

    private void OnDisable()
    {
        if (moveAction != null) moveAction.action.Disable();
        if (jumpAction != null) jumpAction.action.Disable();
        if (attackAction != null) attackAction.action.Disable();
    }
}
