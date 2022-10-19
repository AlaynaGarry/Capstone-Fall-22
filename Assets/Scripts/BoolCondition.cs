using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolCondition : MonoBehaviour
{
    public BoolData data;

    bool IsTrue()
    {
        return data.data;
    }
}
