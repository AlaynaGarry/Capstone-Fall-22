using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "Item/baseItem")]
public class Item : ScriptableObject
{
    new public string name = "Default Item";
    public Sprite icon = null;
    public virtual void Use() {
        Debug.Log("Using " + name);
    }
}

/*Previous Code
 * 
 * public abstract class Item : MonoBehaviour
{
    public enum Type
    {
        USEABLE,
        QUEST
    }

    public GameObject visual;
    public string input;
    public Type type;

    public abstract void Activate();
    public abstract void Decctivate();
    public abstract void UpdateItem();
}*/
