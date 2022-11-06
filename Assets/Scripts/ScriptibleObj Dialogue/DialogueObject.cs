using System.ComponentModel;
using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(menuName ="Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField] public GameObject character;
    //[ReadOnly] public string characterName;
    [SerializeField][TextArea] private string[] dialogue;
    [SerializeField] private AnswerChoice[] answerChoices;

    [SerializeField] public BoolCondition[] conditions;
    public string[] Dialogue => dialogue;

    public bool HasAnswerChoices => AnswerChoices != null && AnswerChoices.Length > 0;

    public AnswerChoice[] AnswerChoices => answerChoices;

    public void SetSpeaker(GameObject speaker) {
        character = speaker;
    }
}
