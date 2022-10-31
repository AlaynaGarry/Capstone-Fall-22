using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IntData", menuName = "Data/IntData")]
public class IntData : ScriptableObject
{
    public string description;
    public int data = -1;

    public int TriggeredID = -1;
}
