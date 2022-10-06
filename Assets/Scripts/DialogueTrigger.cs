using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public TextMeshProUGUI interactUI;

    bool isPlayerCloseEnough = false;

    private void Update()
    {
        if (isPlayerCloseEnough && Input.GetKeyDown(KeyCode.E))
        {
            
            TriggerDialogue();
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player");
            interactUI.text = "Press E to interact";
            isPlayerCloseEnough = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player leave");
            interactUI.text = "";
            isPlayerCloseEnough = false;
        }
    }
}
