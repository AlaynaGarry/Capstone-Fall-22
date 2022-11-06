using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TransitionToDialogue
{
#if UNITY_EDITOR
    public string Name;
#endif
    [Header("Transition")]
    public Transition key;
    [Header("Destination Dialogue Object")] 
    public DialogueObject value;
}

[System.Serializable]
public struct DialogueToKVP
{
#if UNITY_EDITOR
    public string Name;
#endif
    [Header("Origin Dialogue Object")]
    public DialogueObject key;
    [Header("Transition to Dialogue")]
    public TransitionToDialogue[] value;
}
