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
    public string baseNodeGuid;
    public string targetNodeGuid;
}

public class BaseNodeData
{
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
    public EventSO eventSO;
}
[System.Serializable]
public class DialogueNodePort
{
    public string InputGuid;
    public string OutputGuid;
    public Port myPort;
    public TextField textField;
}
