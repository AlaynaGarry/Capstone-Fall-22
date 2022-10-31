using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoolData", menuName = "Data/BoolData")]
[System.Serializable]
public class BoolData : ScriptableObject
{

    public string description;
    public bool data = false;

    //public int TriggeredID = -1;
}
