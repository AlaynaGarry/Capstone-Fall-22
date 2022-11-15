using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
//using UnityEngine.UIElements;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private GameObject dialogueUI;
    [Header("Dialogue box")]
    [SerializeField] private TMP_Text textName;
    [SerializeField] private TMP_Text textBox;
    /*  
        [Header("Image")]
        [SerializeField] private Image leftImage;
        [SerializeField] private GameObject leftImageGO;
        [SerializeField] private Image rightImage;
        [SerializeField] private GameObject rightImageGO;
    */

    //temp (replace with button code)
    /* 
     private List<Button> buttons = new List<Button>();
     private List<Text> buttonsTexts = new List<Text>();
    */

    [SerializeField] Animator dialogueAnimator;
    [SerializeField] private Animator playerAnimator;
    private bool playerActive;

    public bool IsOpen { get; set; }

    private TypewriterEffect typewriterEffect;
    private AnswerChoiceHandler answerChoiceHanlder;

    public static DialogueController Instance { get; private set; }

    private void Awake()
    {
        ShowDialogue(false);

        IsOpen = false;

        typewriterEffect = GetComponent<TypewriterEffect>();
        answerChoiceHanlder = GetComponent<AnswerChoiceHandler>();

        CloseDialogue();
    }

    public void ShowDialogue(bool show)
    {
        IsOpen = true;
        /*
         * temp remove - needs restructure
        var dict = dialogueData.transitionDictionary;
        var transitions = dict.FirstOrDefault(d => d.key.Equals(dialogueObject)).value;
        if (transitions != null)
        {
            foreach (var transition in transitions)
            {
                if (transition.key.ToTransition())
                {
                    ShowDialogue(transition.value);
                    return;
                }
            }
        }*/

        dialogueAnimator.SetBool("IsOpen", true);
        playerAnimator.SetFloat("MovingBlend", 0);

        playerActive = false;
        Player.controlsEnabled = playerActive;
        dialogueUI.SetActive(show);
    }

    public void SetText(string name, string text)
    {
        textName.text = name;
        RunTypingEffect(text);
    }
    public void SetImage(Sprite _image, CharacterImageLocation _dialogueFaceImageType)
    {
        /* leftImageGO.SetActive(false);
         rightImageGO.SetActive(false);

         if (_image != null)
         {
             if (_dialogueFaceImageType == CharacterImageLocation.Left)
             {
                 leftImage.sprite = _image;
                 leftImageGO.SetActive(true);
             }
             else
             {
                 rightImage.sprite = _image;
                 rightImageGO.SetActive(true);
             }
         }*/
    }

    public IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];

            yield return RunTypingEffect(dialogue);
            //textLabel.text = dialogue;

            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasAnswerChoices) break;

            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }

        foreach (var dialogue in dialogueObject.conditions)
        {
            //dialogueData.TriggerDialogueCondition(dialogue);
            Debug.Log("Dialogue Conditions: ");
        }

        if (dialogueObject.HasAnswerChoices)
        {
            answerChoiceHanlder.ShowChoices(dialogueObject.AnswerChoices);
        }
        else
        {
            //set dialogueObject conditions.conditions !conditions.conditions
            CloseDialogue();
        }
    }

    private IEnumerator RunTypingEffect(string dialogue)
    {
        typewriterEffect.Run(dialogue, textBox);

        while (typewriterEffect.IsRunning)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                typewriterEffect.Stop();
            }
        }
    }

    private void CloseDialogue()
    {
        IsOpen = false;
        playerActive = true;
        StartCoroutine(EndDialogue());
    }

    IEnumerator EndDialogue()
    {
        yield return new WaitForSeconds(0.5f);
        Player.controlsEnabled = playerActive;

    }
}
