using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// <summary>
/// Base code from Brackey's Dialogue System Youtube Video. 
/// Link: https://www.youtube.com/watch?v=_nRzoTzeyxU&t=753s
/// Edits for functionallity and preference by yours truly Alayna Garry
/// </summary>
public class DialogueManager : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    [Header("Anim")]
    public Animator dialogueAnimator;
    [Header("Player")]
    public GameObject player;

    [HideInInspector]
    [Header("Key Code Controls")]
    public KeyCode continueKey = KeyCode.Space;

    [Header("Dialogue")]
    private Queue<string> sentences;

    // Use this for initialization
    void Start()
    {
        sentences = new Queue<string>();
    }

    void Update()
    {
        if (dialogueAnimator)
        {
            if (Input.GetKeyDown(continueKey))
            {
                DisplayNextSentence();
            }
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueAnimator.SetBool("IsOpen", true);
        Cainos.CharacterController.controlsEnabled = false;
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        Cainos.CharacterController.controlsEnabled = true;
        dialogueAnimator.SetBool("IsOpen", false);
    }
}
