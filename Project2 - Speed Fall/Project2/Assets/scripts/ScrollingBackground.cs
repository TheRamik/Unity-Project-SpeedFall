using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {

    [SerializeField] public new GameObject camera;
    [SerializeField] Sprite background;
    [SerializeField] GameObject[] backgrounds;
    [SerializeField] public PlatformSpawnManager platformSpawner;
    public int currentBackground = 0;
    float startPos = 0;

	// Use this for initialization
	void Start () {
	    backgrounds[1].transform.position = new Vector3(backgrounds[1].transform.position.x, background.bounds.min.y * 2, backgrounds[1].transform.position.z);
        platformSpawner.SpawnAll();
    }
	
	// Update is called once per frame
	void Update () {
		if (camera.transform.position.y <= startPos + background.bounds.min.y * 2)
        {
            if (currentBackground == 1)
            {
                backgrounds[1].transform.position -= Vector3.down * background.bounds.min.y * 4;
                platformSpawner.SpawnAll();
                currentBackground = 0;
            }
            else
            {
                backgrounds[0].transform.position -= Vector3.down * background.bounds.min.y * 4;
                platformSpawner.SpawnAll();
                currentBackground = 1;
            }
            startPos = camera.transform.position.y;
        }
	}
}
