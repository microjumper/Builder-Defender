using UnityEngine;
using UnityEngine.InputSystem;

public enum DragStyle
{
    Natural = -1,   // Grab and drag the world.
    Standard = 1    // Move the camera with the pointer.
}

public class CameraController : MonoBehaviour
{
    [Header("Drag Settings")]
    [SerializeField, Min(0f)] private float dragSensitivity = 0.05f;
    [SerializeField] private DragStyle dragStyle = DragStyle.Natural;

    private bool isDragging = false;

    public void OnDrag(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                isDragging = true;
                break;

            case InputActionPhase.Canceled:
                isDragging = false;
                break;
        }
    }

    public void OnDragDelta(InputAction.CallbackContext context)
    {
        if (context.performed && isDragging)
        {
            Vector2 delta = context.ReadValue<Vector2>();

            transform.Translate((int)dragStyle * dragSensitivity * delta);
        }
    }
}
