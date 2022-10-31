using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "Data/BoolCondition")]
[System.Serializable]
public class BoolCondition : Condition  
{
	[SerializeField] BoolRef parameter;
	[SerializeField] bool condition;

    public BoolCondition(BoolRef parameter, bool condition)
	{
		this.parameter = parameter;
		this.condition = condition;
	}

	public override bool IsTrue()
	{
		return (parameter == condition);
	}
}
