using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour {

    [SerializeField] GameObject interact;
    [SerializeField] GameObject dialogue;

	
	void Update () {
		if (interact.activeSelf && Input.GetKeyDown(KeyCode.F))
        {
            dialogue.SetActive(true);
            interact.SetActive(false);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Molio"))
        {
            interact.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Molio"))
        {
            interact.SetActive(false);
        }
    }

    public void EndInteraction()
    {
        dialogue.SetActive(false);
    }

}
