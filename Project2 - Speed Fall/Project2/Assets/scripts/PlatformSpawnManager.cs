using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawnManager : MonoBehaviour {

    public static PlatformSpawnManager instance;

    [SerializeField] Vector2 spawnPosition;
    [SerializeField] float distanceBetweenSpawns;
    [SerializeField] GameObject[] platforms;
    [SerializeField] private Transform[] wallPoints;


    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
    void SpawnAll()
    {
        for (int i = 0; i < platforms; i++)
        {

        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
