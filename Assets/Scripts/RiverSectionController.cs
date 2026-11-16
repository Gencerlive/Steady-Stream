using System.Collections.Generic;
using UnityEngine;

public class RiverSectionController : MonoBehaviour
{
    [Header("Lists")]
    private List<GameObject> moveableObjectsInRiver = new List<GameObject>();

    [Header("Variables")]
    [SerializeField] private Vector3 riverDirection;

    [SerializeField] private string moveableObjectTag;

    [SerializeField] private float riverForce;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == moveableObjectTag)
        {
            if (moveableObjectsInRiver.Contains(other.gameObject)) return;

            moveableObjectsInRiver.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == moveableObjectTag)
        {
            if (moveableObjectsInRiver.Contains(other.gameObject)) return;

            moveableObjectsInRiver.Remove(other.gameObject);
        }
    }

    private void Update()
    {
        foreach (GameObject obj in moveableObjectsInRiver)
        {
            obj.GetComponent<Rigidbody>().AddForce(riverForce * riverDirection, ForceMode.Force);
        }
    }

    private void OnDisable()
    {
        moveableObjectsInRiver.Clear();
    }
}
