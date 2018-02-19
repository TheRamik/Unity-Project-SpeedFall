using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class NinjaStar : MonoBehaviour {

    [SerializeField] private float speed;
    private Rigidbody2D rgbd;
    private Vector2 direction;

	// Use this for initialization
	void Start ()
    {
        rgbd = GetComponent<Rigidbody2D>();
	}

    public void Initialize(Vector2 direction)
    {
        this.direction = direction;
    }

    private void FixedUpdate()
    {
        rgbd.velocity = direction * speed;
        Destroy(gameObject, 1.0f);
    }
}
