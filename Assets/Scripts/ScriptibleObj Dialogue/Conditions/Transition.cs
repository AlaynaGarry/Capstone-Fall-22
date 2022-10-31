using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Transition
{
    [SerializeReference] Condition[] conditions;

    public Transition(Condition[] conditions) {
        this.conditions = conditions;
    }

    public bool ToTransition() {
        foreach (var condition in conditions) {
            if (!condition.IsTrue()) return false;
        }

        return true;
    }
}
