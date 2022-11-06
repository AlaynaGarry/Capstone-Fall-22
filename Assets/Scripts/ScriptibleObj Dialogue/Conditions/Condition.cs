using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
