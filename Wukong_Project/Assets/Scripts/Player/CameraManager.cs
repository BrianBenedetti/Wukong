using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    private PlayerInputActions inputActions;

    //float yaw;
    //float pitch;
    //float mouseSensitivity = 0.5f;
    //public float distanceToTarget;
    //public float rotationSmoothTime = 0.12f;
    //public float movementSmoothTime = 10;

    //public Transform targetLook;
    public CinemachineFreeLook mainCamera;

    Vector2 cameraInput;
    //Vector2 pitchMinMax = new Vector2(-40, 80);

    //Vector3 rotationSmoothVelocity;
    //Vector3 currentRotation;
    //public Vector3 offset;


    private void Awake()
    {
        inputActions = new PlayerInputActions();

        inputActions.PlayerControls.Camera.performed += ctx => cameraInput = ctx.ReadValue<Vector2>();
        inputActions.PlayerControls.Camera.canceled += ctx => cameraInput = Vector2.zero;
    }

    //private void FixedUpdate()
    //{
    //    yaw += cameraInput.x * mouseSensitivity;
    //    pitch -= cameraInput.y * mouseSensitivity;
    //    pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

    //    currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);

    //    transform.eulerAngles = currentRotation;

    //    Vector3 desiredPosition = targetLook.position + offset;
    //    Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, movementSmoothTime * Time.deltaTime);
    //    transform.position = smoothedPosition;
    //}
    private void LateUpdate()
    {
        mainCamera.m_XAxis.m_InputAxisValue = cameraInput.x * mainCamera.m_XAxis.m_MaxSpeed * Time.deltaTime;
        mainCamera.m_YAxis.m_InputAxisValue = cameraInput.y * mainCamera.m_YAxis.m_MaxSpeed * Time.deltaTime;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
}
