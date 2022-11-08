using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DialogueSaveAndLoad
{
    private List<Edge> edges => graphView.edges.ToList();
    private List<BaseNode> nodes => graphView.nodes.ToList().Where(node => node is BaseNode).Cast<BaseNode>().ToList();

    private DialogueGraphView graphView;

    public DialogueSaveAndLoad(DialogueGraphView _graphView)
    {
        graphView = _graphView;
    }

    public void Save(DialogueContainerObject dialogueContainerObject)
    {

    }
    public void Load(DialogueContainerObject dialogueContainerObject)
    {

    }

    private void SaveEdges(DialogueContainerObject dialogueContainerObject)
    {
        dialogueContainerObject.nodeLinkDatas.Clear();

        Edge[] connectedEdges = edges.Where(edge => edge.input.node != null).ToArray();
        for (int i = 0; i < connectedEdges.Count(); i++)
        {
            BaseNode outputNode = connectedEdges[i].output.node as BaseNode;
            BaseNode inputNode = connectedEdges[i].input.node as BaseNode;

            dialogueContainerObject.nodeLinkDatas.Add(new NodeLinkData
            {
                baseNodeGuid = outputNode.NodeGUID,
                targetNodeGuid = inputNode.NodeGUID
            });
        }
    }

    private void SaveNode(DialogueContainerObject dialogueContainerObject)
    {
        dialogueContainerObject.dialogueNodeDatas.Clear();
        dialogueContainerObject.eventNodeDatas.Clear();
        dialogueContainerObject.startNodeDatas.Clear();
        dialogueContainerObject.endNodeDatas.Clear();

        nodes.ForEach(node =>
        {
            switch (node)
            {
                case DialogueNode dialogueNode:
                    break;
                default:
                    break;
            }
        });
    }
     private DialogueNodeData SaveNodeData (DialogueNode dialogueNode)
    {
        DialogueNodeData dialogueNodeData = new DialogueNodeData
        {

        };
        return dialogueNodeData;
    }

}
