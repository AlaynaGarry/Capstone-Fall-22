using UnityEngine;

[CreateAssetMenu(menuName ="Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField] public string characterName;
    //[SerializeField] private GameObject character;
    [SerializeField][TextArea] private string[] dialogue;
    [SerializeField] private AnswerChoice[] answerChoices;

    public string[] Dialogue => dialogue;

    public bool HasAnswerChoices => AnswerChoices != null && AnswerChoices.Length > 0;

    public AnswerChoice[] AnswerChoices => answerChoices;

}
