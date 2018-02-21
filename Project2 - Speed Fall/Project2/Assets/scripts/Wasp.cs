using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wasp : MonoBehaviour {

    private int Health;
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
            if (currentPos >= (startPos.x + maxValue))
            {
                direction *= -1;
                gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
                StartCoroutine(waitTime(1));
                currentPos = startPos.x + maxValue;
            }
            else if (currentPos <= (startPos.x + minValue))
            {
                direction *= -1;
                gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
                StartCoroutine(waitTime(1));
                currentPos = startPos.x + minValue;
            }
            transform.position = new Vector3(currentPos, startPos.y, 0);
        }
    }

    public void Damage(int dmg)
    {
        Health -= dmg;
    }

    private void Death()
    {
        anim.SetBool("isDead", true);
        StartCoroutine(DeactivateWasp(0.8f));
    }

    IEnumerator waitTime(float seconds)
    {
        isWaiting = true;
        yield return new WaitForSeconds(seconds);
        isWaiting = false;
    }

    private IEnumerator DeactivateWasp(float duration)
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("NinjaStar"))
        {
            Damage(1);
        }
    }
}
