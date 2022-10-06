using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public TextMeshProUGUI interactUITxt;
    public GameObject interactUI;

    bool isPlayerCloseEnough = false;

    private void Start()
    {
        interactUI.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerCloseEnough && Input.GetKeyDown(KeyCode.E) && interactUI)
        {
            TriggerDialogue();
        }
    }

    public void TriggerDialogue()
    {
        interactUITxt.text = "";
        interactUI.SetActive(false);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Debug.Log("Player");
            interactUI.SetActive(true);
            interactUITxt.text = "E";
            isPlayerCloseEnough = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Debug.Log("Player leave");
            interactUI.SetActive(false);
            interactUITxt.text = "";
            isPlayerCloseEnough = false;
        }
    }
}
