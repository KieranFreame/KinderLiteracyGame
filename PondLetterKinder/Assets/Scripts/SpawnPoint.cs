using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private List<GameObject> pool = new();

    [Header("Spawning Parameters")]
    [SerializeField] private GameObject objToSpawn;
    [SerializeField] private Transform endDestination;
    [SerializeField] private float timeBetweenSpawn = 1.0f;

    public void StartSpawning()
    {
        StartCoroutine(SpawnObject());
    }

    private IEnumerator SpawnObject()
    {
        float timeElapsed;

        while (true)
        {
            Pooler.Spawn(objToSpawn, transform.position, transform.rotation);
            timeElapsed = 0;

            while (timeElapsed <= timeBetweenSpawn)
            {
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }
    }
}
