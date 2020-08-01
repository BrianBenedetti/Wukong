using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    private PlayerInputActions inputActions;

    public CinemachineFreeLook mainCamera;

    Vector2 cameraInput;

    private void Awake()
    {
        inputActions = new PlayerInputActions();

        inputActions.PlayerControls.Camera.performed += ctx => cameraInput = ctx.ReadValue<Vector2>();
        inputActions.PlayerControls.Camera.canceled += ctx => cameraInput = Vector2.zero;
    }

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
