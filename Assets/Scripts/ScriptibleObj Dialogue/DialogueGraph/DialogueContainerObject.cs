using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName ="Dialogue/New Dialogue"), System.Serializable]
public class DialogueContainerObject : ScriptableObject
{
    public List<NodeLinkData> nodeLinkDatas = new List<NodeLinkData>();
    public List<DialogueNodeData> dialogueNodeDatas = new List<DialogueNodeData>();
    public List<EndNodeData> endNodeDatas = new List<EndNodeData>();
    public List<StartNodeData> startNodeDatas = new List<StartNodeData>();
    public List<EventNodeData> eventNodeDatas = new List<EventNodeData>();
    
    public List<BaseNodeData> AllNodes
    {
        get
        {
            List<BaseNodeData> temp = new List<BaseNodeData>();
            temp.AddRange(dialogueNodeDatas);
            temp.AddRange(endNodeDatas);
            temp.AddRange(startNodeDatas);
            temp.AddRange(eventNodeDatas);

            return temp;
        }
    }

}

[System.Serializable]
public class NodeLinkData
{
#if UNITY_EDITOR
    public string NodeLinkName;
#endif
    public string baseNodeGuid;
    public string targetNodeGuid;
}

[System.Serializable]
public class BaseNodeData
{
#if UNITY_EDITOR
    public string BaseNodeName;
#endif
    public string nodeGuid;
    public Vector2 position;
}

[System.Serializable]
public class DialogueNodeData: BaseNodeData
{
    public List<DialogueNodePort> dialogueNodePorts;
    public Sprite sprite;
    public CharacterImageLocation characterImageLocation;
    public string name;
    public List<LanguageGeneric<string>> TextLanguages;
}

[System.Serializable]
public class EndNodeData: BaseNodeData
{
    public EndNodeType endNodeType;
}
[System.Serializable]
public class StartNodeData: BaseNodeData
{
    
}
[System.Serializable]
public class EventNodeData: BaseNodeData
{
    public DialogueEventSO eventSO;
}
[System.Serializable]
public class DialogueNodePort
{
#if UNITY_EDITOR
    public string DialoguePortName;
#endif
    public string PortGuid;
    public string InputGuid;
    public string OutputGuid;
    public Port MyPort;
    public TextField TextField;
    public List<LanguageGeneric<string>> TextLanguages = new List<LanguageGeneric<string>>();
}

[System.Serializable]
public class LanguageGeneric<T>
{
    public LanguageType LanguageType;
    public T LanguageGenericType;
}

public enum LanguageType
{
    English
}