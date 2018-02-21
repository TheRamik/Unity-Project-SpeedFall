using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public bool isGrounded, isWall;
    public int Health;
    public GameObject starPrefab;
    public Transform wallPoint;
    public LayerMask wallLayerMask;
    public GameObject starSpawner;
    private Rigidbody2D rgbd;
    private Animator anim;

    bool isMoving, isJumping, isWallSliding, facingRight;

    [SerializeField] private float jumpForce = 300f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float groundRadius;
    [SerializeField] private Transform[] groundPoints;


    private void Awake()
    {
        rgbd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        facingRight = true;
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        isWall = false;
        isMoving = false;
        isJumping = false;
        isGrounded = IsGrounded();
        anim.SetBool("isGrounded", isGrounded);

        HandleInput();
        HandleMovement(horizontal);
        HandleWallSliding(horizontal);
        HandleLayers();
    }

    private void HandleInput()
    {

        if (Input.GetKey(KeyCode.Space) && !isWallSliding)
        {
            isJumping = true;
            HandleJump();
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
            facingRight = false;
            isMoving = !isMoving;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        { 
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            facingRight = true;
            isMoving = !isMoving;
        }
        if (Input.GetKey(KeyCode.K))
        {
            if (isGrounded)
            {
                ShootingStar(0);
            }
            else
            {
                ShootingStar(1);
            }
        }
    }

    private void HandleJump()
    {
        if (isGrounded && isJumping)
        {
            isJumping = false;
            rgbd.AddForce(new Vector2(0f, jumpForce));
            anim.SetTrigger("jump");
        }
    }

    private void HandleLayers()
    {
        if(!isGrounded)
        {
            anim.SetLayerWeight(1, 1);
        }
        else
        {
            anim.SetLayerWeight(1, 0);
        }
    }

    private void HandleWallSliding(float horizontal)
    {
        if (rgbd.velocity.y < -0.5f)
        {
            anim.SetBool("land", true);
        }
        if (!isGrounded)
        {
            isWall = Physics2D.OverlapCircle(wallPoint.position, 0.1f, wallLayerMask);
            
            if ((facingRight && (horizontal >= 0f)) || (!facingRight && (horizontal < 0.1f)))
            {
                WallSliding();
            }
        }
        if (isWall = false || isGrounded)
        {
            isWallSliding = false;
        }
    }

    private void HandleMovement(float horizontal)
    {

        rgbd.velocity = new Vector2(horizontal * moveSpeed, rgbd.velocity.y);
        anim.SetFloat("speed", Mathf.Abs(horizontal));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy") || collision.transform.CompareTag("spikes"))
        {
            Damage(1);
        }
    }

    private bool IsGrounded()
    {
        if (rgbd.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        anim.ResetTrigger("jump");
                        anim.SetBool("land", false);
                        anim.SetBool("isWallSliding", false);
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void WallSliding()
    {
        if (isWall)
        {
            rgbd.velocity = new Vector2(0, -0.9f);
            isWallSliding = true;
            anim.ResetTrigger("jump");
            anim.SetBool("land", false);
            anim.SetBool("isWallSliding", true);
            if (Input.GetKey(KeyCode.Space))
            {
                if (facingRight)
                {
                    rgbd.AddForce(new Vector2(-jumpForce * 3, jumpForce));
                }
                else
                {
                    rgbd.AddForce(new Vector2(jumpForce * 3, jumpForce));
                }
            }
        }
    }

    public void ShootingStar(int value)
    {
        if (!isGrounded && value == 1 || isGrounded && value == 0)
        {
            if (facingRight)
            {
                GameObject star = (GameObject)Instantiate(starPrefab, transform.position, Quaternion.identity);
                star.transform.position = starSpawner.transform.position;
                star.GetComponent<NinjaStar>().Initialize(Vector2.right);

            }
            else
            {
                GameObject star = (GameObject)Instantiate(starPrefab, transform.position, Quaternion.identity);
                star.transform.position = starSpawner.transform.position;
                star.GetComponent<NinjaStar>().Initialize(Vector2.left);
            }
        }
    }

    private void Death()
    {

    }

    public void Damage(int dmg)
    {
        Health -= dmg;
    }

}
