using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueConditions : MonoBehaviour 
{ 
    [Header("BoolData")]
    [SerializeField] BoolData[] boolDatas;
    [Header("Transition")]
    [SerializeReference] Transition[] transitions;

    private void Update()
    {
        Test();
        foreach(Transition t in transitions)
        //StartCoroutine(Test());
        if (t.ToTransition())
        {
            Debug.Log("ToTransition == true");
        }
    }
    private void Test()
    {
/*      
        yield return new WaitForSeconds(2);
        if (boolDatas != null)
        {
            Debug.Log("DialogueConditions..boolDatas[0].data = true");
            boolDatas[0].data = !boolDatas[0].data;
            yield break;
        }
        yield return null;
*/
    }
}
