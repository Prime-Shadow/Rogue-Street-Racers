using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;





public class CameraController : MonoBehaviour
{
    [Header("Cinemachine Cameras (3.x)")]
    public CinemachineCamera followCam;
    public CinemachineCamera freeCam;

    [Header("UI Elements")]
    public GameObject selectionCanvas;


    [Header("Cinemachine Brain")]
    public CinemachineBrain cinemachineBrain;

    [Header("Animator")]
    public Animator animator;

    [Header("Intro Animation")]
    public float animationLength = 3f;



    [Header("Settings")]
    public float dragThreshold = 0.1f;
    public int followPriority = 20;
    public int freePriority = 10;

    private bool draggingRightSide;
    private Vector2 lastTouchPos;

    

    void Start()
    {
        ActivateFollowCamera();
        StartCoroutine(EnableCinemachineBrain());
    }

    private IEnumerator EnableCinemachineBrain()
    {
        cinemachineBrain.enabled = false;
        animator.Play("CameraAnim");

        yield return new WaitForSeconds(animationLength);

        cinemachineBrain.enabled = true;
        selectionCanvas.SetActive(true); //  show UI after animation
        ResetView(); // switch to FollowCam
    }

    void Update()
    {
        HandleTouchInput();
    }
    private void HandleTouchInput()
    {
        if (Touchscreen.current == null)
            return;

        var touch = Touchscreen.current.primaryTouch;

        if (!touch.press.isPressed)
            return;

        Vector2 pos = touch.position.ReadValue();

        if (touch.phase.ReadValue() == TouchPhase.Began)
        {
            lastTouchPos = pos;
            draggingRightSide = pos.x > Screen.width * 0.5f;
        }

        if (touch.phase.ReadValue() == TouchPhase.Moved && draggingRightSide)
        {
            Vector2 delta = pos - lastTouchPos;

            if (delta.magnitude > dragThreshold)
            {
                ActivateFreeCamera();
            }

            lastTouchPos = pos;
        }

        if (touch.phase.ReadValue() == TouchPhase.Ended || touch.phase.ReadValue() == TouchPhase.Canceled)
        {
            draggingRightSide = false;
        }
    }

    public void ResetView()
    {
        ActivateFollowCamera();
    }

    private void ActivateFollowCamera()
    {
        followCam.Priority = followPriority;
        freeCam.Priority = 0;
    }

    private void ActivateFreeCamera()
    {
        freeCam.Priority = freePriority;
        followCam.Priority = 0;
    }
}