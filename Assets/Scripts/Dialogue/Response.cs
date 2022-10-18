using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Response
{
    public enum LineType
    {
        RESPONSE,
        QUESTION,
        ANSWER
    }

    [Header("Identification")]
    public int ID;
    public string characterName;

    [Header("LineType")]
    [SerializeField] public Response.LineType lineType = Response.LineType.RESPONSE;
    
    [Header("Responce Info")]
    [TextArea(3,10)]
    public string[] responces;

    [Header("Question Class Vars")]
    public string[] choicesList;

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
