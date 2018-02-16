using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] float moveSpeed;
    [SerializeField] Rigidbody2D rgbd;
    [SerializeField] Animator anim;
    bool isMoving, isJumping;

    private void FixedUpdate()
    {
        isMoving = false;
        isJumping = false;

        if (Input.GetKey(KeyCode.A))
        {
            
        }
    }

    void Move(Direction d)
    {
        rgbd.MovePosition((Vector2)gameObject.transform.position + (int)d * Vector2.right)
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
