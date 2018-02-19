using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawnManager : MonoBehaviour {

    [SerializeField] CameraMove cam;
    [SerializeField] Vector2 spawnPosition;
    [SerializeField] float distanceBetweenSpawns;
    [SerializeField] float stopSpawnBeforePause;
    [SerializeField] Platform[] platforms;
    [SerializeField] private Transform[] wallPoints;
    List<PlatformList> livingPlatforms = new List<PlatformList>();
    float LeftPoint, MiddlePoint, RightPoint;
    Vector2 LeftSpawnPosition, RightSpawnPosition;

    private void Awake()
    {
        LeftPoint = wallPoints[0].position.x;
        RightPoint = wallPoints[1].position.x;
        MiddlePoint = (LeftPoint + RightPoint) / 2;
        LeftSpawnPosition = spawnPosition;
        RightSpawnPosition = spawnPosition;
    }

    public void SpawnAll()
    {
        SpawnObjects(LeftPoint, ref LeftSpawnPosition);
        SpawnObjects(RightPoint, ref RightSpawnPosition);
    }

    void SpawnObjects(float point, ref Vector2 spawnPos)
    {
        for (int i = 0; i < platforms.Length; i++)
        {
            IncrementSpawnPoint(point);
            if (spawnPos.y > cam.pauseFallingCamera[0] + stopSpawnBeforePause)
            {

                livingPlatforms.Add(new PlatformList(Instantiate(platforms[Random.Range(0, platforms.Length - 1)].prefab,
                                    spawnPos, Quaternion.identity) as GameObject));
            }
        }
    }

    void IncrementSpawnPoint(float wallPoint)
    {
        if (wallPoint < 0)
        {
            LeftSpawnPosition += new Vector2(0, Random.Range(-2, -distanceBetweenSpawns));
            LeftSpawnPosition.x = Random.Range(wallPoint, MiddlePoint);
        }
        else
        {
            RightSpawnPosition += new Vector2(0, Random.Range(-2, -distanceBetweenSpawns));
            RightSpawnPosition.x = Random.Range(MiddlePoint, wallPoint);
        }
    }
}

[System.Serializable]
public class Platform {
    public GameObject prefab;
}

public class PlatformList
{
    public GameObject obj;

    public PlatformList(GameObject newObj)
    {
        obj = newObj;
    }
}