using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Response
{
    [Header("Identification")]
    public int ID;
    public string characterName;

    [Header("Responce Info")]
    [TextArea(3,10)]
    public string[] responces;

    /*[Header("Identification")]
    public int displayOrder;

    [Header("Conditions")]
    Dictionary<string, bool> condition;

    [Header("Responce Info")]
    //temp
    public string name;

    [TextArea(3, 10)]
    public string[] conditionResponses;*/
}
