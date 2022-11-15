using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DialogueUI : MonoBehaviour
{
   /* [SerializeField] private DialogueData dialogueData;

    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] public GameObject button;

    [SerializeField] private GameObject interactUI;

    [SerializeField] private TMP_Text NPCNameText;

    [SerializeField] Animator dialogueAnimator;
    [SerializeField] private Animator playerAnimator;
    private bool playerActive;

    public bool IsOpen { get; set; }

    private TypewriterEffect typewriterEffect;
    private AnswerChoiceHandler answerChoiceHanlder;

    private void Start()
    {
        IsOpen = false;

        typewriterEffect = GetComponent<TypewriterEffect>();
        answerChoiceHanlder = GetComponent<AnswerChoiceHandler>();

        CloseDialogue();
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        IsOpen = true;
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
        }

        dialogueAnimator.SetBool("IsOpen", true);
        playerAnimator.SetFloat("MovingBlend", 0);

        interactUI.SetActive(false);

        playerActive = false;
        Player.controlsEnabled = playerActive;

        //NPCNameText.text = dialogueObject.character.name;

        //dialogueBox.SetActive(true);
        //StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    public void AdvanceDialogue()
    {
        //Click ContinueButton to continue the dialogue

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
            dialogueData.TriggerDialogueCondition(dialogue);
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
        //typewriterEffect.Run(dialogue, textLabel);

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
        interactUI.SetActive(false);
        StartCoroutine(EndDialogue());
    }

    IEnumerator EndDialogue()
    {
        yield return new WaitForSeconds(0.5f);
        Player.controlsEnabled = playerActive;

    }*/
}
