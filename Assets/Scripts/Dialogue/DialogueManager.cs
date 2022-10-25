using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
/// <summary>
/// Base code from Brackey's Response System Youtube Video. 
/// Link: https://www.youtube.com/watch?v=_nRzoTzeyxU&t=753s
/// Edits for functionallity and preference by yours truly Alayna Garry
/// </summary>
public class DialogueManager : MonoBehaviour
{
    [Header("Instance")]
    static DialogueManager dialogueManagerInstance;
    public static DialogueManager Instance { get { return dialogueManagerInstance; } }

    [Header("UI")]
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    [SerializeField] TMP_Text questionTxt;

    [Header("Anim")]
    public Animator dialogueAnimator;

    [Header("Player")]
    public GameObject player;
    public Animator playerAnimator;

    [HideInInspector]
    [Header("Key Code Controls")]
    public KeyCode continueKey = KeyCode.Space;

    [Header("Response")]
    private Queue<string> sentences;
    private int responseID = 0;

    [Header("Choice UI")]
    [SerializeField][Tooltip("Built to be a Panel w/ a Layout Group Component")] public GameObject buttonContainer;
    [SerializeField][Tooltip("Requires Button Component On Parent And TMP_Text on Child")] public GameObject buttonPrefab;

    /*    /// <summary>
        /// Code here was written by Draven Bowton and modified by yours truly Alayna Garry
        /// </summary>
        [Header("Choice Info")]

        [Header("Choice UI")]
        [SerializeField][Tooltip("Built to be a Panel w/ a Layout Group Component")] GameObject buttonContainer;
        [SerializeField][Tooltip("Requires Button Component On Parent And TMP_Text on Child")] GameObject buttonPrefab;

        // Used To Delete TextBoxes After Selection is Made
        List<GameObject> currentActiveButtons = new List<GameObject>();

        [System.Serializable]
        public struct TextOptions
        {
            [Header("Choice Text")]
            public string text;
            public int optionId;
        }

        [System.Serializable]
        public struct TextDialogue
        {
            [Header("Character Name")]
            public string chatName;
            [Header("All Choices")]
            public List<TextOptions> choices;
        }
        /// <summary>
        /// END OF CODE
        /// </summary>*/

    public Enum lineType;
    // Use this for initialization
    void Start()
    {
        sentences = new Queue<string>();
        lineType = Response.LineType.RESPONSE;
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

    public void StartDialogue(Response dialogue)
    {
        dialogueAnimator.SetBool("IsOpen", true);
        playerAnimator.SetFloat("MovingBlend", 0);

        Cainos.CharacterController.controlsEnabled = false;

        nameText.text = dialogue.characterName;

        sentences.Clear();
        //for (int i = 0; i < dialogue.Length; i++)
        //{
        foreach (string sentence in dialogue.responces)
        {
            //if(dialogue.ID == responseID)
            //Debug.Log(sentence);
            sentences.Enqueue(sentence);
        }
        //}

                DisplayNextSentence();

        //DisplayNextSentence(dialogue[responseID]);
    }

    public void StartChoice(Response response)
    {
        questionTxt.text = response.question;

        //Line from Draven to activate the buttons
        GenerateTextButtons(response);
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            StartCoroutine(EndDialogue());
            responseID++;
            return;
        }

       /* if (response.lineType == Response.LineType.QUESTION && response.question != "")
        {
            questionTxt.text = response.question;
            GenerateTextButtons(response);
        }*/

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    /// <summary>
    /// Code here was written by Draven Bowton and modified by yours truly Alayna Garry
    /// </summary>

    public void GenerateTextButtons(Response response)
    {
        RemoveActiveDialogue(response);
        Response.TextDialogue textInfo = response.initialDialogue;


        foreach (var info in textInfo.choices)
        {
            GameObject newButton = Instantiate(buttonPrefab, buttonContainer.transform);
            newButton.name = "Option: " + info.optionId;
            newButton.GetComponentInChildren<TMP_Text>().text = info.text;
            newButton.GetComponent<Button>().onClick.AddListener(() => OptionSelected(info.optionId));

            response.currentActiveButtons.Add(newButton);
        }
    }

    public void OptionSelected(int selection)
    {
        print(selection);
    }

    public void RemoveActiveDialogue(Response response)
    {
        for (int i = 0; i < response.currentActiveButtons.Count;)
        {
            GameObject go = response.currentActiveButtons[i];
            response.currentActiveButtons.Remove(go);
            Destroy(go);
        }
    }
    /// <summary>
    /// END OF DRAVEN'S CODE
    /// </summary>

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {

            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f); ;
        }
    }

    IEnumerator EndDialogue()
    {
        dialogueAnimator.SetBool("IsOpen", false);

        yield return new WaitForSeconds(0.5f);
        Cainos.CharacterController.controlsEnabled = true;
    }
}
