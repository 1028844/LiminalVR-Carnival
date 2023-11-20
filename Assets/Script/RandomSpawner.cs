using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] targetObjects;
    [SerializeField] SpawnArea[] spawnAreas;
    [SerializeField] List<Transform> objectPositions;
    [SerializeField] int amountOfSpawns;
    [SerializeField] Vector2 minGapBetweenSpawns;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnTargets();
        }
    }

    public void SpawnTargets()
    {
        for (int i = 0; i < amountOfSpawns; i++)
        {
            int randomSpawnArea = Random.Range(0, spawnAreas.Length);

            GameObject newObj = Instantiate(targetObjects[0], Vector3.zero, targetObjects[0].transform.rotation);
            newObj.transform.parent = spawnAreas[randomSpawnArea].transform;

            PickRandomSpawn(newObj, randomSpawnArea);
        }
    }

    void PickRandomSpawn(GameObject obj, int randomSpawnArea)
    {
        obj.transform.localPosition = new Vector3
        (
        Random.Range(-spawnAreas[randomSpawnArea].spawnAreaX, spawnAreas[randomSpawnArea].spawnAreaX),
        0,
        Random.Range(-spawnAreas[randomSpawnArea].spawnAreaZ, spawnAreas[randomSpawnArea].spawnAreaZ)
        );

        objectPositions.Add(obj.transform);
    }
}
