using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] targetObjects;
    [SerializeField] SpawnArea[] spawnAreas;
    public List<GameObject> badTargetsObjs = new List<GameObject>();
    [SerializeField] int amountOfSpawns;
    [SerializeField] Vector2 minGapBetweenSpawns;

    public float targetTime = 300.0f;
    bool timerStarted = false;

    public static int goodTargets = 0;
    public static int badTargets = 0;

    private void Update()
    {
        if (goodTargets < 0) goodTargets = 0;
        if (badTargets < 0) badTargets = 0;

        if (timerStarted)
        {
            if (goodTargets <= 0)
            {
                ResetTargets();
                SpawnTargets();
            }

            targetTime -= Time.deltaTime;

            if (targetTime <= 0.0f)
            {
                EndTimer();
            }
        }
    }

    public void StartTimer()
    {
        timerStarted = true;
    }

    void EndTimer()
    {
        timerStarted = false;
        Debug.Log("Timer Ended");
    }

    public void SpawnTargets()
    {
        int randomSpawnAreaS = Random.Range(0, spawnAreas.Length);
        int randomTargetObjS = Random.Range(0, targetObjects.Length);

        GameObject newGoodTarget = Instantiate(targetObjects[0], Vector3.zero, targetObjects[0].transform.rotation);
        newGoodTarget.transform.parent = spawnAreas[randomSpawnAreaS].transform;

        PickRandomSpawn(newGoodTarget, randomSpawnAreaS);

        goodTargets++;

        for (int i = 0; i < amountOfSpawns - 1; i++)
        {
            int randomSpawnArea = Random.Range(0, spawnAreas.Length);
            int randomTargetObj = Random.Range(0, targetObjects.Length);

            GameObject newObj = Instantiate(targetObjects[randomTargetObj], Vector3.zero, targetObjects[randomTargetObj].transform.rotation);
            newObj.transform.parent = spawnAreas[randomSpawnArea].transform;

            if (randomTargetObj == 0)
            {
                goodTargets++;
            }
            else if (randomTargetObj == 1)
            {
                badTargetsObjs.Add(newObj);
                badTargets++;
            }

            PickRandomSpawn(newObj, randomSpawnArea);
        }
    }

    void PickRandomSpawn(GameObject obj, int randomSpawnArea)
    {
        obj.transform.localPosition = new Vector3
        (
        Random.Range(-spawnAreas[randomSpawnArea].spawnAreaX, spawnAreas[randomSpawnArea].spawnAreaX),
        -5,
        Random.Range(-spawnAreas[randomSpawnArea].spawnAreaZ, spawnAreas[randomSpawnArea].spawnAreaZ)
        );
    }

    void ResetTargets()
    {
        goodTargets = 0;
        badTargets = 0;

        foreach(GameObject obj in badTargetsObjs)
        {
            if (obj != null) obj.GetComponent<TargetPosManager>().DestroyObj();
        }

        badTargetsObjs.Clear();
    }
}
