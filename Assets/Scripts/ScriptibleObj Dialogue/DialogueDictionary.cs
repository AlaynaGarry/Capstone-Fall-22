using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct TransitionToDialogue
{
    [Header("Transition")]
    public Transition key;
    [Header("Destination Dialogue Object")] 
    public DialogueObject value;
}

[System.Serializable]
public struct DialogueToKVP
{
    [Header("Origin Dialogue Object")]
    public DialogueObject key;
    public TransitionToDialogue[] value;
}
