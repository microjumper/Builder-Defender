using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public enum DragStyle
{
    Natural = -1,   // Grab and drag the world.
    Standard = 1    // Move the camera with the pointer.
}

public class CameraTarget : MonoBehaviour
{
    private const float MinOrthographicSize = 2.5f;
    private const float MaxOrthographicSize = 25f;
    private const float DefaultOrthographicSize = 10f;

    [SerializeField] private CinemachineCamera camera;

    [Header("Zoom Settings")]
    [SerializeField, Range(MinOrthographicSize, MaxOrthographicSize)] private float orthographicSize = DefaultOrthographicSize;
    [SerializeField, Range(0.1f, 4.99f)] private float zoomSensitivity = 1f;

    [Header("Drag Settings")]
    [Range(0.1f, 4.99f), SerializeField] private float dragSensitivity = 1f;
    [SerializeField] private DragStyle dragStyle = DragStyle.Natural;

    private Vector2 delta = Vector2.zero;

    public void OnCameraDrag(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                delta = context.ReadValue<Vector2>();
                break;

            case InputActionPhase.Canceled:
                delta = Vector2.zero;
                break;
        }
    }

    public void OnCameraZoom(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            float scroll = context.ReadValue<Vector2>().y;

            float newSize = camera.Lens.OrthographicSize - scroll * zoomSensitivity;

            camera.Lens.OrthographicSize = Mathf.Clamp(newSize, MinOrthographicSize, MaxOrthographicSize);
        }
    }

    public void OnCameraZoomReset(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            camera.Lens.OrthographicSize = DefaultOrthographicSize;
        }
    }

    private void Update()
    {
        transform.Translate((int)dragStyle * dragSensitivity * Time.deltaTime * delta);
    }
}
