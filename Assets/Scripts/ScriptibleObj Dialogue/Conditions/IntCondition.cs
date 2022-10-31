using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "Data/IntCondition")]
[System.Serializable]
public class IntCondition : Condition
{
    [Header("Bool Ref")]
    [SerializeField] IntRef parameter;
    [Header("Current Condition Status")]
    [SerializeField] int condition;
	[Header("Required Predicate")]
	[SerializeField] Predicate predicate;

	public IntCondition(IntRef parameter, Predicate predicate, int condition)
	{
		this.parameter = parameter;
		this.predicate = predicate;
		this.condition = condition;
	}

	public override bool IsTrue()
	{
		bool result = false;

		switch (predicate)
		{
			case Predicate.EQUAL:
				result = ((int)parameter == condition);
				break;
			case Predicate.LESS:
				result = ((int)parameter < condition);
				break;
			case Predicate.GREATER:
				result = ((int)parameter > condition);
				break;
			default:
				break;
		}

		return result;
	}
}
