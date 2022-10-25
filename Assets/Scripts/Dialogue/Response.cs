using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;

[System.Serializable]
public class Response
{
    public enum LineType
    {
        RESPONSE,
        QUESTION,
        ANSWER
    }

    [Header("Identification")]
    public int ID;
    public string characterName;

    [Header("LineType")]
    [SerializeField] public Response.LineType lineType = Response.LineType.RESPONSE;
    
    [Header("Responce Info")]
    [TextArea(3,10)]
    public string[] responces;

    [Header("Question Class Vars")]
    public string question;
    public Answer[] answers;

    /// <summary>
    /// Code here was written by Draven Bowton and modified by yours truly Alayna Garry
    /// </summary>
    [Header("Choice Info")]
    [SerializeField] public TextDialogue initialDialogue;

    /*[Header("Choice UI")]
    [SerializeField][Tooltip("Built to be a Panel w/ a Layout Group Component")] public GameObject buttonContainer;
    [SerializeField][Tooltip("Requires Button Component On Parent And TMP_Text on Child")] public GameObject buttonPrefab;*/

    // Used To Delete TextBoxes After Selection is Made
    public List<GameObject> currentActiveButtons = new List<GameObject>();

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
/*        [Header("Character Name")]
        public string chatName;*/
        [Header("All Choices")]
        public List<TextOptions> choices;
    }
    /// <summary>
    /// END OF CODE
    /// </summary>
    /// 
    /// <summary>
    /// Code here was written by Draven Bowton and modified by yours truly Alayna Garry
    /// </summary>

   /* public void GenerateTextButtons(TextDialogue textInfo)
    {
        RemoveActiveButtons();

        foreach (var info in textInfo.choices)
        {
            GameObject newButton = Instantiate(buttonPrefab, buttonContainer.transform);
            newButton.name = textInfo.chatName + "Option: " + info.optionId;
            newButton.GetComponentInChildren<TMP_Text>().text = info.text;
            newButton.GetComponent<Button>().onClick.AddListener(() => OptionSelected(info.optionId));

            currentActiveButtons.Add(newButton);
        }
    }

    public void OptionSelected(int selection)
    {
        print(selection);
    }

    public void RemoveActiveButtons()
    {
        for (int i = 0; i < currentActiveButtons.Count;)
        {
            GameObject go = currentActiveButtons[i];
            currentActiveButtons.Remove(go);
            Destroy(go);
        }
    }*/
    /// <summary>
    /// END OF DRAVEN'S CODE
    /// </summary>
}
