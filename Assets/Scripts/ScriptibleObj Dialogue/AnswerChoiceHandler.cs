using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Globalization;
using System.Collections.Generic;

public class AnswerChoiceHandler : MonoBehaviour
{
    [SerializeField] private RectTransform answerChoiceBox;
    [SerializeField] private RectTransform answerChoiceButtonTemplate;
    [SerializeField] private RectTransform answerChoiceContainer;

    List<GameObject> currentActiveButtons = new List<GameObject>();

    private DialogueController dialogueController;

    private void Start()
    {
        dialogueController = GetComponent<DialogueController>();
    }

    public void ShowChoices(AnswerChoice[] answerChoices)
    {
        float choiceBoxHeight = 0;
        //dialogueController.button.SetActive(false);
        foreach (AnswerChoice answerChoice in answerChoices)
        {
            GameObject choiceButton = Instantiate(answerChoiceButtonTemplate.gameObject, answerChoiceContainer);
            choiceButton.gameObject.SetActive(true);
            choiceButton.GetComponentInChildren<TMP_Text>().text = answerChoice.AnswerText;
            choiceButton.GetComponent<Button>().onClick.AddListener(() => OnAnswerPicked(answerChoice));
            currentActiveButtons.Add(choiceButton);

            choiceBoxHeight += answerChoiceButtonTemplate.sizeDelta.y;
        }

        answerChoiceBox.sizeDelta = new Vector2(answerChoiceBox.sizeDelta.x, choiceBoxHeight + (180 - choiceBoxHeight));
        answerChoiceBox.gameObject.SetActive(true);
    }

    private void OnAnswerPicked(AnswerChoice answerChoice)
    {
        answerChoiceBox.gameObject.SetActive(false);

        RemoveActiveButtons();
        //dialogueController.button.SetActive(true);
        dialogueController.ShowDialogue(answerChoice.DialogueObject);
    }

    public void RemoveActiveButtons()
    {
        foreach (GameObject button in currentActiveButtons )
        {
            Destroy(button);
        }
        currentActiveButtons.Clear();
    }
}
