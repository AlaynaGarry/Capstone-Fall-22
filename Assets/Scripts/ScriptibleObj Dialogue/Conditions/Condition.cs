using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "Data/Condition")]
[System.Serializable]
public abstract class Condition : ScriptableObject
{
	public enum Predicate
	{
		EQUAL,
		LESS,
		LESS_EQUAL,
		GREATER
	}

	public abstract bool IsTrue();
}
