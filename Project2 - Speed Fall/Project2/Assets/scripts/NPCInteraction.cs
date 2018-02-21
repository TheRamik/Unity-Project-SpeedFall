using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour {

    [SerializeField] GameObject interact;
    [SerializeField] GameObject dialogue;
    bool hasInteracted = false;

	
	void Update () {
		if (interact.activeSelf && Input.GetKeyDown(KeyCode.F) && !hasInteracted)
        {
            hasInteracted = true;
            dialogue.SetActive(true);
            interact.SetActive(false);
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
            else
            {
                dialogue.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Molio"))
        {
            interact.SetActive(false);
            if (hasInteracted)
            {
                EndInteraction();
            }
        }
    }

    public void EndInteraction()
    {
        dialogue.SetActive(false);
    }

}
