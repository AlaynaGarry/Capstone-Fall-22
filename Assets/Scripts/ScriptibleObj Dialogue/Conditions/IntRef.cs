using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "Data/IntConditionRef")]
[System.Serializable]
public class IntRef : ScriptableObject
{
    [Header("Condition Value"), Tooltip("Value the Dialogue Object needs to be")]
    [SerializeField] public int value = -1;

    public static implicit operator int(IntRef r) { return r.value; }
}
