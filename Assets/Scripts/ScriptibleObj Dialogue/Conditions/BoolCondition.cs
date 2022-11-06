using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "Data/BoolCondition")]
[System.Serializable]
public class BoolCondition : Condition  
{
	[Header("Bool Ref")]
	[SerializeField] BoolRef parameter;
	[Header("Current Condition Status")]
	[SerializeField] public bool condition;

	public bool Condition { get => condition; set => condition = value; }
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
