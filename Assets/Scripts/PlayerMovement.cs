using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody rigidbody;

    [Header("Input")]
    [SerializeField] private InputActionAsset inputManager;

    private InputAction directionalKeys;
    private InputAction cameraInput;
    private InputAction jumpInput;

    [Header("Control Variables")]
    [SerializeField] private float speedCap;
    [SerializeField] private float waterDrag, airDrag;

    private bool preventMovement;

    [Header("Swimming Variables")]
    [SerializeField] private float swimSpeed;
    [SerializeField] private float maxSwimSpeed;

    [HideInInspector] public bool isInWater;

    [Header("Jumping Variables")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCoolDownTime;

    private bool jumpCoolingDown;

    [Header("Camera Variables")]
    [SerializeField] private float cameraSmoothing;
    [SerializeField] private float sensX, sensY;

    private float currentCamXRotation, currentCamYRotation;
    private float wantedCamXRotation, wantedCamYRotation;
    private float camXVelocity, camYVelocity;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //Grab input
        directionalKeys = inputManager.FindAction("Move");
        cameraInput = inputManager.FindAction("Look");
        jumpInput = inputManager.FindAction("Jump");

    }

    private void Update()
    {
        if (preventMovement) return;

        CameraMovement();

        SpeedControl();
        DragControl();

        if (isInWater)
        {
            MovePlayer();

            if (jumpInput.IsPressed() && !jumpCoolingDown)
                Jump();
        }
    }

    private void DragControl()
    {
        if(!isInWater || jumpCoolingDown)
            rigidbody.linearDamping = airDrag;
        else
            rigidbody.linearDamping = waterDrag;
    }

    private void MovePlayer()
    {
        Vector2 inputVector = directionalKeys.ReadValue<Vector2>();

        Vector3 movementDirection = transform.forward * inputVector.y + transform.right * inputVector.x;

        rigidbody.AddForce(swimSpeed * movementDirection);
    }
    
    private void SpeedControl()
    {
        Vector3 flatVel = rigidbody.linearVelocity;

        if (flatVel.magnitude > speedCap)
        {
            flatVel = flatVel.normalized * speedCap;
        }

        if (isInWater && !jumpCoolingDown)
        {
            if (flatVel.magnitude > maxSwimSpeed)
            {
                flatVel = flatVel.normalized * maxSwimSpeed;
            }
        }

        rigidbody.linearVelocity = flatVel;
    }

    private void CameraMovement()
    {
        //Grab the mouse input
        float inputX = Mouse.current.delta.x.ReadValue() * sensX;
        float inputY = Mouse.current.delta.y.ReadValue() * sensY;

        wantedCamYRotation += inputX;
        wantedCamXRotation -= inputY;

        //Prevent the player from looking up past neck breaking point
        wantedCamXRotation = Mathf.Clamp(wantedCamXRotation, -89.9f, 89.9f);

        currentCamXRotation = Mathf.SmoothDamp(currentCamXRotation, wantedCamXRotation, ref camXVelocity, cameraSmoothing);
        currentCamYRotation = Mathf.SmoothDamp(currentCamYRotation, wantedCamYRotation, ref camYVelocity, cameraSmoothing);

        transform.rotation = Quaternion.Euler(wantedCamXRotation, wantedCamYRotation, 0);
    }

    private void Jump()
    {
        rigidbody.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);

        StartCoroutine(JumpCoolDown());
    }

    private IEnumerator JumpCoolDown()
    {
        jumpCoolingDown = true;

        yield return new WaitForSeconds(jumpCoolDownTime);

        jumpCoolingDown = false;
    }

    public void PreventMovement(bool toggle)
    {
        preventMovement = toggle;
    }
}
