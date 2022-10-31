using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "Data/BoolConditionRef")]
[System.Serializable]
public class BoolRef: ScriptableObject
{
	public bool value;

	public static implicit operator bool(BoolRef r) { return r.value; }
}
