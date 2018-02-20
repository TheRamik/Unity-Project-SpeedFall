using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToNextScene : MonoBehaviour {


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Molio"))
        {
            Debug.Log("Collision Made");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}
