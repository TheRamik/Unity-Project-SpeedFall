using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    public static GameObject camera;
    [SerializeField] GameObject player;
    [SerializeField] float startFallingCamera;
    [SerializeField] float stopFallingCamera;
    private bool movCamera;
    public float speed;

    private void Awake()
    {
        camera = this.gameObject;
    }

    void FixedUpdate()
    {
        if (player.transform.position.y < startFallingCamera) {
            movCamera = true;
        }
        else if (player.transform.position.y < stopFallingCamera) {
            movCamera = false;
        }

        if (movCamera) {
            MoveCamera();
        }
    }

    void MoveCamera() {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(gameObject.transform.position.x, player.transform.position.y - speed, gameObject.transform.position.z), 0.1f);
    }

    public static float yPosition() {
        return camera.gameObject.transform.position.y;
    }
}
