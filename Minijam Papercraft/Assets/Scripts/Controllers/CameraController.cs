using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private Transform _cameraHoldderTransform;

    [Header("Moving Parameters")]
    [SerializeField] private float _moveSpeed = 50.0f;
    [SerializeField] private float _dragMoveSpeed = 15.0f;

    [Header("Zoom Parameters")]
    [SerializeField] private float _minCameraSize = 10.0f;
    [SerializeField] private float _maxCameraSize = 50.0f;
    private float _currentCameraSize;
    [SerializeField, Range(0.01f, 1.0f)] private float _zoomSensitivity = 1.0f;

    [Header("Helper Variables")]
    private bool _isClicking = false;
    private bool _isDragMoving = false;
    private Vector2 _moveInput;

    [SerializeField] private LayerMask _upgradabledLayerMask;

    private Vector3 _startDrag;

    private void Start()
    {
        _cameraHoldderTransform = this.transform;
        _currentCameraSize = Camera.main.orthographicSize;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 forward = _cameraHoldderTransform.forward;
        Vector3 right = _cameraHoldderTransform.right;
        forward.y = 0;
        right.y = 0;

        float zoomFactor = (_currentCameraSize - _minCameraSize) / (_maxCameraSize - _minCameraSize);
        float speedMultiplier = Mathf.Lerp(0.5f, 1.0f, zoomFactor);

        float speed = _isDragMoving ? _dragMoveSpeed * 3.0f : _moveSpeed;

        Vector2 adjustedMoveInput = _isDragMoving ? _moveInput * 0.1f : _moveInput;

        Vector3 move = Time.deltaTime * speedMultiplier * speed * (forward * adjustedMoveInput.y + right * adjustedMoveInput.x).normalized;
        _cameraHoldderTransform.Translate(move, Space.World);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _moveInput = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            _moveInput = Vector2.zero;
        }
    }

    public void OnDragMove(InputAction.CallbackContext context)
    {
        // if (context.started)
        // {
        //     if (_isClicking)
        //     {
        //         _isDragMoving = true;
        //     }
        // }

        // if (_isDragMoving && context.performed)
        // {
        //     Plane plane = new Plane(Vector3.up, Vector3.zero);
        //     Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        //     if (plane.Raycast(ray, out float distance))
        //     {
        //         if (Mouse.current.rightButton.wasPressedThisFrame)
        //         {
        //             _startDrag = ray.GetPoint(distance);
        //         }
        //         else if (Mouse.current.rightButton.isPressed)
        //         {
        //             Vector3 dragDelta = _startDrag - ray.GetPoint(distance);
        //             _moveInput = new Vector2(dragDelta.x, dragDelta.z);
        //             _startDrag = ray.GetPoint(distance); // Update start drag point for smooth dragging
        //         }
        //     }
        // }
        // else if (context.canceled)
        // {
        //     _moveInput = Vector2.zero;
        // }
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _isClicking = true;
        }
        else if (context.canceled)
        {
            if (!_isDragMoving)
            {
                Debug.Log("Click registered");
                SelectTarget();
            }

            _isClicking = false;
            _isDragMoving = false; // Set to false only when the click is released
        }
    }

    public void OnZoom(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _currentCameraSize = Mathf.Clamp(_currentCameraSize + (-context.ReadValue<Vector2>().y * _zoomSensitivity), _minCameraSize, _maxCameraSize);
            Camera.main.orthographicSize = _currentCameraSize;
        }
    }

    public void SelectTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * 100.0f, Color.red, 0.5f);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _upgradabledLayerMask))
        {
            // Get the upgrade script from the object that was clicked
            hit.collider.GetComponent<UpgradeObjects>().ShowUpgradePanel();
        }
    }
}
