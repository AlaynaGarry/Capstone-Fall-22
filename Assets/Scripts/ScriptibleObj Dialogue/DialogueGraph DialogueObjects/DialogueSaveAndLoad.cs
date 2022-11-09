using ClipperLib;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueSaveAndLoad
{
    private List<Edge> edges => graphView.edges.ToList();
    private List<BaseNode> nodes => graphView.nodes.ToList().Where(node => node is BaseNode).Cast<BaseNode>().ToList();

    private DialogueGraphView graphView;

    public DialogueSaveAndLoad(DialogueGraphView _graphView)
    {
        graphView = _graphView;
    }

    //Save
    public void Save(DialogueContainerObject dialogueContainerObject)
    {
        SaveEdges(dialogueContainerObject);
        SaveNodes(dialogueContainerObject);

        EditorUtility.SetDirty(dialogueContainerObject);
        AssetDatabase.SaveAssets();
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
    private void SaveNodes(DialogueContainerObject dialogueContainerObject)
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
                    dialogueContainerObject.dialogueNodeDatas.Add(SaveNodeData(dialogueNode));
                    break;
                case StartNode startNode:
                    dialogueContainerObject.startNodeDatas.Add(SaveNodeData(startNode));
                    break;
                case EndNode endNode:
                    dialogueContainerObject.endNodeDatas.Add(SaveNodeData(endNode));
                    break;
                case EventNode eventNode:
                    dialogueContainerObject.eventNodeDatas.Add(SaveNodeData(eventNode));
                    break;
                default:
                    break;
            }
        });
    }
    private DialogueNodeData SaveNodeData(DialogueNode dialogueNode)
    {
        DialogueNodeData dialogueNodeData = new DialogueNodeData
        {
            nodeGuid = dialogueNode.NodeGUID,
            position = dialogueNode.GetPosition().position,
            name = dialogueNode.Name,
            characterImageLocation = dialogueNode.CharacterImageLocation,
            sprite = dialogueNode.CharacterImage,
            dialogueNodePorts = dialogueNode.dialogueNodePorts
        };

        foreach (var nodePort in dialogueNodeData.dialogueNodePorts)
        {
            nodePort.OutputGuid = string.Empty;
            nodePort.InputGuid = string.Empty;
            foreach (Edge edge in edges)
            {
                if (edge.output == nodePort.myPort)
                {
                    nodePort.OutputGuid = (edge.output.node as BaseNode).NodeGUID;
                    nodePort.InputGuid = (edge.input.node as BaseNode).NodeGUID;
                }
            }
        }

        return dialogueNodeData;
    }
    private StartNodeData SaveNodeData(StartNode startNode)
    {
        StartNodeData nodeData = new StartNodeData()
        {
            nodeGuid = startNode.NodeGUID,
            position = startNode.GetPosition().position
        };

        return nodeData;
    }
    private EventNodeData SaveNodeData(EventNode eventNode)
    {
        EventNodeData nodeData = new EventNodeData
        {
            nodeGuid = eventNode.NodeGUID,
            position = eventNode.GetPosition().position,
            eventSO = eventNode.EventSO
        };
        return nodeData;

    }
    private EndNodeData SaveNodeData(EndNode endNode)
    {
        EndNodeData nodeData = new EndNodeData()
        {
            nodeGuid = endNode.NodeGUID,
            position = endNode.GetPosition().position,
            endNodeType = endNode.EndNodeType
        };

        return nodeData;
    }

    //Load
    public void Load(DialogueContainerObject dialogueContainerObject)
    {
        ClearGraph();
        GenerateNodes(dialogueContainerObject);
        ConnectNodes(dialogueContainerObject);
    }

    private void GenerateNodes(DialogueContainerObject dialogueContainerObject)
    {
        //start
        foreach (StartNodeData node in dialogueContainerObject.startNodeDatas)
        {
            StartNode tempNode = graphView.CreateStartNode(node.position);
            tempNode.NodeGUID = node.nodeGuid;

            graphView.AddElement(tempNode);
        }

        //End Node
        foreach (EndNodeData node in dialogueContainerObject.endNodeDatas)
        {
            EndNode tempNode = graphView.CreateEndNode(node.position);
            tempNode.NodeGUID = node.nodeGuid;
            tempNode.EndNodeType = node.endNodeType;

            tempNode.LoadValueInToField();
            graphView.AddElement(tempNode);
        }

        //Event Node
        foreach (EventNodeData node in dialogueContainerObject.eventNodeDatas)
        {
            EventNode tempNode = graphView.CreateEventNode(node.position);
            tempNode.NodeGUID = node.nodeGuid;
            tempNode.EventSO = node.eventSO;

            tempNode.LoadValueInToField();
            graphView.AddElement(tempNode);
        }

        //Dialogue Node
        foreach (DialogueNodeData node in dialogueContainerObject.dialogueNodeDatas)
        {
            DialogueNode tempNode = graphView.CreateDialogueNode(node.position);
            tempNode.NodeGUID = node.nodeGuid;
            tempNode.Name = node.name;
            tempNode.CharacterImage = node.sprite;
            tempNode.CharacterImageLocation = node.characterImageLocation;

            //ports
            foreach (DialogueNodePort nodePort in node.dialogueNodePorts)
            {
                tempNode.AddChoicePort(tempNode, nodePort);
            }

            tempNode.LoadValueInToField();
            graphView.AddElement(tempNode);
        }

    }

    private void ConnectNodes(DialogueContainerObject dialogueContainerObject)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            List<NodeLinkData> connections = dialogueContainerObject.nodeLinkDatas.Where(edge => edge.baseNodeGuid == nodes[i].NodeGUID).ToList();
            for (int j = 0; j < connections.Count; j++)
            {
                string targetNodeGuid = connections[j].targetNodeGuid;
                BaseNode targetNode = nodes.First(node => node.NodeGUID == targetNodeGuid);

                if ((nodes[i] is DialogueNode) == false)
                {
                    LinkNodes(nodes[i].outputContainer[j].Q<Port>(), (Port)targetNode.inputContainer[0]);
                }
            }
        }

        List<DialogueNode> dialogueNodes = nodes.FindAll(node => node is DialogueNode).Cast<DialogueNode>().ToList();

        foreach(DialogueNode dialogueNode in dialogueNodes)
        {
            foreach(DialogueNodePort nodePort in dialogueNode.dialogueNodePorts)
            {
                if(nodePort.InputGuid != string.Empty)
                {
                    BaseNode targetNode = nodes.First(_node => _node.NodeGUID == nodePort.InputGuid);
                    LinkNodes(nodePort.myPort, (Port)targetNode.inputContainer[0]);

                }
            }
        }
    }

    private void LinkNodes(Port outputPort, Port inputPort)
    {
        Edge tempEdge = new Edge
        {
            output = outputPort,
            input = inputPort
        };
        tempEdge.input.Connect(tempEdge);
        tempEdge.output.Connect(tempEdge);

        graphView.AddElement(tempEdge);
    }

    private void ClearGraph()
    {
        edges.ForEach(edge => graphView.RemoveElement(edge));
        nodes.ForEach(node => graphView.RemoveElement(node));
    }
}
