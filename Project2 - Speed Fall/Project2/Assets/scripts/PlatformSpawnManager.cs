using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawnManager : MonoBehaviour {

    public static PlatformSpawnManager instance;
    //public List<GameObject> NPCRefs = new List<GameObject>();
    [SerializeField] ScrollingBackground sb;
    [SerializeField] Vector2 spawnPosition;
    [SerializeField] float distanceBetweenSpawns;
    [SerializeField] Platform[] platforms;
    [SerializeField] private Transform[] wallPoints;
    List<PlatformList> livingPlatforms = new List<PlatformList>();


    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start ()
    {
        SpawnAll();
	}

    // Update is called once per frame
    void Update()
    {
        if (sb.currentBackground == 1)
        {
            SpawnAll(sb.backgrounds[0].transform.);
        }
        else if (sb.currentBackground == 0)
        {
            SpawnAll(sb.backgrounds[1].transform.position);
        }
    }

    void SpawnAll(Vector3 backgroundPos)
    {
        for (int i = 0; i < platforms.Length; i++)
        {
            IncrementSpawnPoint(backgroundPos);
            livingPlatforms.Add(new PlatformList(Instantiate (platforms[i].prefab, spawnPosition, Quaternion.identity) as GameObject, platforms[i].PlatformNumber));
            //NPCRefs.Add(livingNPCs[i].obj);
        }
    }



    void IncrementSpawnPoint(Vector3 backgroundPos)
    {
        spawnPosition = new Vector2(Random.Range(wallPoints[0].position.x, wallPoints[1].position.x),
                                    Random.Range(backgroundPos.y +, distanceBetweenSpawns));
        //spawnPosition.x = Random.Range(wallPoints[0].position.x, wallPoints[1].position.x);
    }
}

[System.Serializable]
public class Platform {
    public GameObject prefab;
    public int PlatformNumber;
}

public class PlatformList
{
    public GameObject obj;
    public int PlatformNumber;

    public PlatformList(GameObject newObj, int newPlatformNumber/*, EventType newNPCEvent*/)
    {
        obj = newObj;
        PlatformNumber = newPlatformNumber;
    }
}