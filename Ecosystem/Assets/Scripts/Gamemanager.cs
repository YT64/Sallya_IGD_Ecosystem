using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public GameObject fish;
    public GameObject star;
    public GameObject cthulhu;
    private float Timestamp;

    private void SpawnObjects(GameObject prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            float spawnY = Random.Range(0.6f, 0.8f);
            float spawnX = Random.Range(0.3f, 0.8f);
            Vector2 spawnPosition = Camera.main.ViewportToWorldPoint(new Vector2(spawnX, spawnY));

            Instantiate(prefab, spawnPosition, Quaternion.identity);
        }
    }

    private void CheckAndSpawn(GameObject prefab, int maxCount)
    {
        GameObject[] existingObjects = GameObject.FindGameObjectsWithTag(prefab.tag);

        if (existingObjects.Length < maxCount)
        {
            if (Time.time - Timestamp > 3.0f)
            {
                SpawnObjects(prefab, 1);
            }
           
        }
    }

    private void CheckAndDespawn(GameObject prefab, int minCount)
    {
        GameObject[] existingObjects = GameObject.FindGameObjectsWithTag(prefab.tag);

        if (existingObjects.Length > minCount)
        {
            if (Time.time - Timestamp > 2.0f)
            {
                Destroy(prefab, 1);
            }
        }
    }


    void Start()
    {
        SpawnObjects(fish, 1);
        SpawnObjects(star, 2);
        SpawnObjects(cthulhu, 1);

    }

    void Update()
    {
        CheckAndSpawn(fish, 1);
        CheckAndSpawn(star, 1);
        CheckAndSpawn(cthulhu, 1);
        CheckAndDespawn(star, 3);
    }
}
