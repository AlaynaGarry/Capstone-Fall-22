using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System;

public class ChoiceController : MonoBehaviour
{
    /*[SerializeField] public GameObject choicePanel;
    [SerializeField] public TMP_Text choiceQuestionTxt;

    [Header("Choice")]
    [SerializeField] bool isChoice = false;
    [SerializeField] Button[] choiceButtons;
    private int choiceIndex = 0;

    [SerializeField] public Question[] questions;
    private int currentQuestionIndex;

    public Question GetCurrentQuestion()
    {
        var question = questions[currentQuestionIndex];
        return question;
    }

    private void Start()
    {
        InitializeButtons();
    }

    private void CurrentChoice()
    {
        var question = GetCurrentQuestion();
        choiceQuestionTxt = question.questionText;

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i >= question.choicesList.Length)
            {
                choiceButtons[i].gameObject.SetActive(false);
                continue;
            }

            string choiceTxt = question.choicesList[i];

            choiceButtons[i].gameObject.SetActive(true);
            choiceButtons[i].GetComponentInChildren<Text>().text = choiceTxt;
        }
    }

    private void InitializeButtons()
    {
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            Button button = choiceButtons[i];
            int buttonIndex = i;
            button.onClick.AddListener(() => ShowResponse(buttonIndex));
        }
    }

    private void ShowResponse(int buttonIndex)
    {
        Debug.Log("Choice Chosen");
        var question = GetCurrentQuestion();
        choiceQuestionTxt = question.questionText;

        //StartCoroutine(MoveToNextQuestionAfterDelay());
    }

    private IEnumerator MoveToNextQuestionAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        currentQuestionIndex++;
        CurrentChoice();
    }*/
}

