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
    [SerializeField, Range(0.1f, 4.99f)] private float zoomSensitivity = 1f;
    [SerializeField, Min(0.01f)] private float zoomSmoothTime = 0.15f;

    [Header("Drag Settings")]
    [Range(0.1f, 4.99f), SerializeField] private float dragSensitivity = 1f;
    [SerializeField] private DragStyle dragStyle = DragStyle.Natural;

    private Vector2 delta = Vector2.zero;

    private float targetOrthographicSize;
    private float zoomVelocity = 0f;

    private void Awake()
    {
        targetOrthographicSize = camera.Lens.OrthographicSize;
    }

    public void OnCameraDrag(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
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

            targetOrthographicSize = Mathf.Clamp(
                targetOrthographicSize - scroll * zoomSensitivity, 
                MinOrthographicSize,
                MaxOrthographicSize
            );
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

        SmoothZoom();
    }

    private void SmoothZoom()
    {
        float currentSize = camera.Lens.OrthographicSize;

        float newSize = Mathf.SmoothDamp(
            currentSize,
            targetOrthographicSize,
            ref zoomVelocity,
            zoomSmoothTime
        );

        camera.Lens.OrthographicSize = newSize;
    }
}
