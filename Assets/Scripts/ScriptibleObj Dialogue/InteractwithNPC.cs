using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractwithNPC : MonoBehaviour
{
    [SerializeField] private GameObject speechBubble;
    [SerializeField] private KeyCode talkKey = KeyCode.E;
    [SerializeField] private TMP_Text keyInputText;

    private DialogueInteraction dialogueTalk;

    private void Awake()
    {
        speechBubble.SetActive(false);
        keyInputText.text = talkKey.ToString();
        dialogueTalk = GetComponent<DialogueInteraction>();
    }

    void Update()
    {
        if (Input.GetKeyDown(talkKey)) 

        if (Input.GetKeyDown(talkKey) && speechBubble.activeSelf)
        {
            dialogueTalk.StartDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            speechBubble.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            speechBubble.SetActive(false);
        }
    }
}
