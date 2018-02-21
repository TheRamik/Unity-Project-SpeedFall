using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] float xMax;
    [SerializeField] float yMax;
    [SerializeField] float xMin;
    [SerializeField] float yMin;

    private Transform target;

    // Use this for initialization
    void Start()
    {

        target = GameObject.Find("Molio").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(target.position.x + 2.5f, xMin, xMax), Mathf.Clamp(target.position.y, yMin, yMax), transform.position.z);
    }
}
