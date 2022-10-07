using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class DialogueTrigger : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] public Response dialogue;
    [SerializeField] public TextMeshProUGUI interactUITxt;
    [SerializeField] public GameObject interactUI;

    [Header("Animator")]
    public Animator interactionAnimator;

    [HideInInspector]
    [Header("Key Code Controls")]
    public KeyCode interactKey = KeyCode.E;

    [Header("Trigger Bools")]
    bool isPlayerCloseEnough = false;

    private void Start()
    {
        interactUI.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerCloseEnough && Input.GetKeyDown(interactKey) && interactUI)
        {
            TriggerDialogue();
        }
    }

    public void TriggerDialogue()
    {
        DeactivateInteractableTxt();
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    public void ActivateInteractableTxt()
    {
        interactUI.SetActive(true);
        interactionAnimator.SetTrigger("Start");
        interactUITxt.text = interactKey.ToString();
    }
    public void DeactivateInteractableTxt()
    {
        interactUI.SetActive(false);
        interactUITxt.text = "";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ActivateInteractableTxt();
            isPlayerCloseEnough = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DeactivateInteractableTxt();
            isPlayerCloseEnough = false;
        }
    }
}
