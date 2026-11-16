using Unity.VisualScripting;
using UnityEngine;

public class WaterCheckHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PlayerMovement playerMovement;
    private Rigidbody rigidbody;

    [Header("Variables")]
    [SerializeField] private int waterLayer = 4;

    private void Awake()
    {
        rigidbody = playerMovement.rigidbody;

        rigidbody.useGravity = true;
        playerMovement.isInWater = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == waterLayer)
        {
            playerMovement.isInWater = true;

            rigidbody.useGravity = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.name);

        if (other.gameObject.layer == waterLayer)
        {
            playerMovement.isInWater = false;

            rigidbody.useGravity = true;
        }
    }
}
