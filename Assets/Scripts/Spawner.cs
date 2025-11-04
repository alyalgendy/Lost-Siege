using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public float spawnRate = 1f;
    public float minHight = -1f;
    public float maxHight = 1f;
    public float minWidth = -1f; 
    public float maxWidth = 1f;

    private int totalSpawned;
    private const int MAX_GHOSTS = 200;

    private void OnEnable()
    {
        totalSpawned = 0;
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
        if (totalSpawned >= MAX_GHOSTS)
        {
            CancelInvoke(nameof(Spawn));
            return;
        }

        GameObject Ghost = Instantiate(prefab, transform.position, Quaternion.identity);
        Ghost.transform.position += Vector3.up * Random.Range(minHight, maxHight);
        Ghost.transform.position += Vector3.right * Random.Range(minWidth, maxWidth);
        totalSpawned++;
    }
}
