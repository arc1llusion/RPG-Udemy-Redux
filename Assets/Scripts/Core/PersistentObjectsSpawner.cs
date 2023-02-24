using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentObjectsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject persistentObjectPrefab;

    private static bool hasSpawned = false;

    private void Awake()
    {
        if(!hasSpawned)
        {
            hasSpawned = true;

            SpawnPersistentObjects();
        }
    }

    private void SpawnPersistentObjects()
    {
        GameObject persistentObject = Instantiate(persistentObjectPrefab);
        DontDestroyOnLoad(persistentObject);
    }
}
