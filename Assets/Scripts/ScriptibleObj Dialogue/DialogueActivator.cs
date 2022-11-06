using TMPro;
using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject dialogueObject;

    [Header("UI")]
    [SerializeField] public TMP_Text textLabel;
    [SerializeField] public GameObject interactUI;

    [Header("Animator")]
    [SerializeField] private Animator interactionAnimator;

    [SerializeField] private GameObject dialogueUI;
    private string playerInteract;

    private bool isPlayerCloseEnough = false;

    private void Start()
    {
        interactUI.SetActive(false);
        playerInteract = FindObjectOfType<Player>().interactKey.ToString();
    }

    private void Update()
    {
        if (isPlayerCloseEnough && dialogueUI.activeSelf)
        {
            //Debug.Log(dialogueUI.activeSelf);
            interactUI.SetActive(true);
        }   
    }

    private void ActivateInteractableText()
    {
        interactUI.SetActive(true);
        interactionAnimator.SetTrigger("Start");
        Debug.Log(playerInteract);
        textLabel.text = playerInteract;
    }
    public void DeactivateInteractableText()
    {
        interactUI.SetActive(false);
        textLabel.text = "";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        isPlayerCloseEnough = true;
        if (other.CompareTag("Player") && other.TryGetComponent(out Player player))
        {
            ActivateInteractableText();
            player.Interactable = this;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out Player player))
        {
            if (player.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                DeactivateInteractableText();
                isPlayerCloseEnough = false;
                player.Interactable = null;
            }
        }
    }

    public void Interact(Player player)
    {
        player.DialogueUI.ShowDialogue(dialogueObject);
    }
}
