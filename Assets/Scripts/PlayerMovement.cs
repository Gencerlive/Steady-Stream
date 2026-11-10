using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody rigidbody;

    [Header("Input")]
    [SerializeField] private InputActionAsset inputManager;

    private InputAction directionalKeys;

    [Header("Swimming Variables")]
    [SerializeField] private float swimSpeed;

    private void Start()
    {
        //Grab input
        directionalKeys = inputManager.FindAction("Move");

        //Grab Components
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector2 inputVector = directionalKeys.ReadValue<Vector2>();

        rigidbody.AddForce(swimSpeed * inputVector);
    }
}
