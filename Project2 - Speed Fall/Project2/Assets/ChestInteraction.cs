using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChestInteraction : MonoBehaviour {

    [SerializeField] GameObject interact;
    bool hasInteracted = false;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (interact.activeSelf && Input.GetKeyDown(KeyCode.F) && !hasInteracted)
        {
            hasInteracted = true;
            interact.SetActive(false);
        }
        if (hasInteracted)
        {
            OpenChest();
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Molio"))
        {
            if (!hasInteracted)
            {
                interact.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Molio"))
        {
            interact.SetActive(false);
        }
    }

    private void OpenChest()
    {
        if (hasInteracted)
        {
            anim.SetBool("Open", true);
            StartCoroutine(DeactivateWasp(0.8f));
        }
    }

    private IEnumerator DeactivateWasp(float duration)
    {
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
