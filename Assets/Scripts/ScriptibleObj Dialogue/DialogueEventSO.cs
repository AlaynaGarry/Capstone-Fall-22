using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[CreateAssetMenu(menuName ="Dialogue/New Dialogue Event")]
[System.Serializable]
public class DialogueEventSO : ScriptableObject
{
    public virtual void RunEvent()
    {
        Debug.Log("Event Was Called");
    }


}
