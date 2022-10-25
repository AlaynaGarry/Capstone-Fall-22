using UnityEngine;

[System.Serializable]
public class AnswerChoice
{
    [SerializeField] private string answerText;
    [SerializeField] private DialogueObject dialogueObject;

    public string AnswerText=> answerText;
    public DialogueObject DialogueObject => dialogueObject; 
}
