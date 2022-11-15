using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueInteraction : DialogueGetData
{
    [SerializeField] private DialogueController dialogueController;

    private DialogueNodeData currentDialogueNodeData;
    private DialogueNodeData previousDialogueNodeData;

    private void Awake()
    {
        dialogueController =  FindObjectOfType<DialogueController>();
    }

    public void StartDialogue()
    {
        CheckNodeType(GetNextNode(dialogueContainerObject.startNodeDatas[0]));
        dialogueController.ShowDialogue(true);
    }

    private void CheckNodeType(BaseNodeData baseNodeData)
    {
        switch (baseNodeData)
        {
            case StartNodeData nodeData:
                RunNode(nodeData);
                break;
            case EndNodeData nodeData:
                RunNode(nodeData);
                break;
            case EventNodeData nodeData:
                RunNode(nodeData);
                break;
            case DialogueNodeData nodeData:
                RunNode(nodeData);
                break;
            default:
                break;
        }
    }

    private void RunNode(StartNodeData nodeData)
    {
        CheckNodeType(GetNextNode(dialogueContainerObject.startNodeDatas[0]));
    }

    private void RunNode(DialogueNodeData nodeData)
    {
        previousDialogueNodeData = currentDialogueNodeData;
        currentDialogueNodeData = nodeData;

        dialogueController.SetText(nodeData.name, nodeData.TextLanguages.Find(line => line.LanguageType == LanguageController.Instance.Language).LanguageGenericType);
        dialogueController.SetImage(nodeData.sprite, nodeData.characterImageLocation);

        MakeButtons(nodeData.dialogueNodePorts);

    }
    private void RunNode(EventNodeData nodeData)
    {
        if(nodeData.eventSO != null)
        {
            nodeData.eventSO.RunEvent();
        }
        CheckNodeType(GetNextNode(nodeData));
    }
    private void RunNode(EndNodeData nodeData)
    {
        switch (nodeData.endNodeType)
        {
            case EndNodeType.End:
                dialogueController.ShowDialogue(false);

                break;
            case EndNodeType.Repeat:
                CheckNodeType(GetNodeByGUID(currentDialogueNodeData.nodeGuid));
                break;
            case EndNodeType.GoBack:
                CheckNodeType(GetNodeByGUID(previousDialogueNodeData.nodeGuid));
                break;
            case EndNodeType.ReturnToStart:
                CheckNodeType(GetNextNode(dialogueContainerObject.startNodeDatas[0]));
                break;
            default:
                break;
        }
    }

    private void MakeButtons(List<DialogueNodePort> nodePorts)
    {
        List<string> lines = new List<string>();
        List<UnityAction> unityActions = new List<UnityAction>();

        foreach (DialogueNodePort nodePort in nodePorts)
        {
            lines.Add(nodePort.TextLanguages.Find(line => line.LanguageType == LanguageController.Instance.Language).LanguageGenericType);
            UnityAction tempAction = null;
            tempAction += () =>
            {
                CheckNodeType(GetNodeByGUID(nodePort.InputGuid));
            };
            unityActions.Add(tempAction);
        }

        //dialogueController.SetButtons(lines, unityActions);

    }
}
