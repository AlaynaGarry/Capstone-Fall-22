using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueGetData : MonoBehaviour
{
    [SerializeField] protected DialogueContainerObject dialogueContainerObject;
    protected BaseNodeData GetNodeByGUID(string targetNodeGUID)
    {
        return dialogueContainerObject.AllNodes.Find(node => node.nodeGuid == targetNodeGUID);
    }

    protected BaseNodeData GetNodeByNodePort(DialogueNodePort nodePort)
    {
        return dialogueContainerObject.AllNodes.Find(node => node.nodeGuid == nodePort.InputGuid);
    }

    protected BaseNodeData GetNextNode(BaseNodeData baseNodeData)
    {
        NodeLinkData nodeLinkData = dialogueContainerObject.nodeLinkDatas.Find(edge => edge.baseNodeGuid == baseNodeData.nodeGuid);

        return GetNodeByGUID(nodeLinkData.targetNodeGuid);
    }
}
