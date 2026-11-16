using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstocalesSpawner : MonoBehaviour
{
    [Header("Lists")]
    [SerializeField] private List<GameObject> spawnableObjects = new List<GameObject>();

    [Header("GameObjects")]
    [SerializeField] private Transform spawnPointOne;
    [SerializeField] private Transform spawnPointTwo;

    [Header("Variables")]
    [SerializeField] private float spawnRate;

    [Header("Rotation")]
    public float maxRandomAngleX;
    public float maxRandomAngleY;
    public float maxRandomAngleZ;

    private void Start()
    {
        StartCoroutine(SpawnObject());
    }

    private IEnumerator SpawnObject()
    {
        while (true) 
        {
            yield return new WaitForSeconds(spawnRate);

            int objectIndex = Random.Range(0, spawnableObjects.Count);

            Instantiate(spawnableObjects[objectIndex], randomPosition(), randomRotation());
        }
    }

    private Quaternion randomRotation()
    {
        Quaternion rot = Quaternion.identity;

        rot.x = Random.Range(0, maxRandomAngleX);
        rot.y = Random.Range(0, maxRandomAngleY);
        rot.z = Random.Range(0, maxRandomAngleZ);

        return rot;
    }

    private Vector3 randomPosition()
    {
        Vector3 choosenPosition = new Vector3();

        Vector3 pos1 = spawnPointOne.position;
        Vector3 pos2 = spawnPointTwo.position;

        choosenPosition.x = Random.Range(Mathf.Min(pos1.y, pos2.x), Mathf.Max(pos1.x, pos2.x));
        choosenPosition.y = Random.Range(Mathf.Min(pos1.y, pos2.y), Mathf.Max(pos1.y, pos2.y));
        choosenPosition.z = Random.Range(Mathf.Min(pos1.z, pos2.z), Mathf.Max(pos1.z, pos2.z));

        return choosenPosition;
    }

    private void StopSpawning()
    {
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        StopSpawning();
    }
}
