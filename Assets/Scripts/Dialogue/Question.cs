using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[System.Serializable]
public class Question
{
    [Header("Question Class Vars")]
    public string nameText;
    public string questionText;

    public string[] choicesList;
}