using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wasp : MonoBehaviour {

    private int Health;
    private Rigidbody2D rgbd;
    private Animator anim;
    private float moveSpeed = 3f;
    int direction = 1;
    private bool isWaiting = false;
    private Vector2 startPos;
    private float currentPos;

    [SerializeField] float maxValue = 3;
    [SerializeField] float minValue = -3;

    private void Awake()
    {
        rgbd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Health = 25;
        startPos = gameObject.transform.position;
        currentPos = startPos.x;
    }

    private void Update()
    {
        if (Health <= 0)
        {
            Death();
        }
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (!isWaiting)
        {

            currentPos += Time.deltaTime * direction * moveSpeed; // or however you are incrementing the position
            if (currentPos >= maxValue)
            {
                direction *= -1;
                gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
                StartCoroutine(waitTime());
                currentPos = maxValue;
            }
            else if (currentPos <= minValue)
            {
                direction *= -1;
                gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
                StartCoroutine(waitTime());
                currentPos = minValue;
            }
            transform.position = new Vector3(currentPos, startPos.y, 0);
        }
    }

    IEnumerator waitTime()
    {
        isWaiting = true;
        yield return new WaitForSeconds(1);
        isWaiting = false;
    }

    public void Damage(int dmg)
    {
        Health -= dmg;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("NinjaStar"))
        {
            Damage(1);
        }
    }

    private void Death()
    {
        anim.SetBool("isDead", true);
        Destroy(gameObject, 0.5f);
    }



}
