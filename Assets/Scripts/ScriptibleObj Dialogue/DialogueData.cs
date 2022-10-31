using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue/DialogueData")]
public class DialogueData : ScriptableObject
{
    [Tooltip("Custom Dictionary of Dialogue Object Types")]
    [SerializeField] public DialogueToKVP[] transitionDictionary;
}
