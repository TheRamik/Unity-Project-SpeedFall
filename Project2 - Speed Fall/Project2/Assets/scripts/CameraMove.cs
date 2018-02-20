using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    public float speed;
    public bool playerOffScreen;
    public float startFallingCamera;
    public float pauseFallingCamera;
    [SerializeField] GameObject player;
    [SerializeField] Transform[] screenPoints;
    private new GameObject camera;
    private bool movCamera;
    

    public GameObject Camera
    {
        get
        {
            return camera;
        }

        set
        {
            camera = value;
        }
    }

    private void Awake()
    {
        Camera = this.gameObject;
    }

    void FixedUpdate()
    {
        if (player.transform.position.y < startFallingCamera)
        {
            movCamera = true;
        }

        if (movCamera && playerOutOfRange())
        {
            movCamera = false;
            playerOffScreen = true; //implement game over. If player goes off screen.
        }

        if (gameObject.transform.position.y < pauseFallingCamera)
        {
            movCamera = false;
        }

        if (movCamera)
        {
            MoveCamera();
        }


    }

    void MoveCamera() {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(gameObject.transform.position.x, yPosition() + speed, gameObject.transform.position.z), 0.1f);
    }

    private float yPosition()
    {
        return gameObject.transform.position.y;
    }

    public bool playerOutOfRange()
    {
        return ((player.transform.position.y - 1f) > screenPoints[0].position.y) || ((player.transform.position.y + 1f)  < screenPoints[1].position.y);
    }
}
