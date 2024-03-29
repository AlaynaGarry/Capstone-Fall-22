using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "Data/BoolConditionRef")]
[System.Serializable]
public class BoolRef: ScriptableObject
{
	[Header("Condition Value"), Tooltip("Value the Dialogue Object needs to be")]
	[SerializeField] public bool value = true;

	public static implicit operator bool(BoolRef r) { return r.value; }
}
