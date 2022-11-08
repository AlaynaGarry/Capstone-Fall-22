using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using System.Linq;
using System.Runtime.CompilerServices;

public enum CharacterImageLocation
{
    Left,
    Right
}

public class DialogueNode : BaseNode
{
    private string name = "";
    private Sprite characterImage;
    private CharacterImageLocation characterImageLocation;

    private List<DialogueNodePort> dialogueNodePorts = new List<DialogueNodePort>();

    public string Name { get => name; set => name = value; }
    public Sprite CharacterImage { get => characterImage; set => characterImage = value; }
    public CharacterImageLocation CharacterImageLocation { get => characterImageLocation; set => characterImageLocation = value; }

    private TextField nameField;
    private ObjectField characterImageField;
    private EnumField characterImageLocationField;

    public DialogueNode() { }
    public DialogueNode(Vector2 position, DialogueEditorWindow _editorWindow, DialogueGraphView _graphView) {
        editorWindow = _editorWindow;
        graphView = _graphView;

        title = "Dialogue";
        SetPosition(new Rect(position, defaultNodeSize));
        nodeGUID = Guid.NewGuid().ToString();
        AddInputPort("input", Port.Capacity.Multi);

        characterImageField = new ObjectField();

        //Image
        Label labelImage = new Label("Image");
        labelImage.AddToClassList("labelImage");
        labelImage.AddToClassList("Label");
        mainContainer.Add(labelImage);

        characterImageField = new ObjectField 
        { 
            objectType = typeof(Sprite), 
            allowSceneObjects = false, 
            value = characterImage 
        };
        characterImageField.RegisterValueChangedCallback(value =>
        {
            characterImage = value.newValue as Sprite;
        });
        mainContainer.Add(characterImageField);

        //Character Image Enum
        characterImageLocationField = new EnumField()
        {
            value = characterImageLocation
        };
        characterImageLocationField.Init(characterImageLocation);
        characterImageLocationField.RegisterValueChangedCallback(value =>
        {
            characterImageLocation = (CharacterImageLocation)value.newValue;
        });
        mainContainer.Add(characterImageLocationField);

        //text name
        Label labelName = new Label("Name");
        labelName.AddToClassList("labelName");
        labelName.AddToClassList("Label");
        mainContainer.Add(labelName);
        
        nameField = new TextField("");
        nameField.RegisterValueChangedCallback(value =>
        {
            name = value.newValue;
        });
        nameField.SetValueWithoutNotify(name);
        nameField.AddToClassList("TextName");
        mainContainer.Add(nameField);

        Button button = new Button()
        {
            text = "Add choice"
        };
        button.clicked += () =>
        {
            AddChoicePort(this);
        };
        titleButtonContainer.Add(button);
    }

    public override void LoadValueInToField()
    {
        characterImageField.SetValueWithoutNotify(characterImage);
        characterImageLocationField.SetValueWithoutNotify(characterImageLocation);
        nameField.SetValueWithoutNotify(name);
    }

    public Port AddChoicePort(BaseNode baseNode, DialogueNodePort _dialogueNodePort = null)
    {
        Port port = GetPortInstance(Direction.Output);

        int outputPortCount = baseNode.outputContainer.Query("connector").ToList().Count;
        string outputPortName = $"{outputPortCount + 1}";

        DialogueNodePort dialogueNodePort = new DialogueNodePort();

        if(_dialogueNodePort != null)
        {
            dialogueNodePort.InputGuid = _dialogueNodePort.InputGuid;
            dialogueNodePort.OutputGuid = _dialogueNodePort.OutputGuid;
        }

        //Delete Button
        Button deleteButton = new Button(() => DeletePort(baseNode, port))
        {
            text = "X"
        };

        port.contentContainer.Add(deleteButton);

        dialogueNodePort.myPort = port;
        port.name = "";

        dialogueNodePorts.Add(dialogueNodePort);

        baseNode.outputContainer.Add(port);

        //refresh
        baseNode.RefreshPorts();
        baseNode.RefreshExpandedState();

        return port;
    }

    private void DeletePort(BaseNode _node, Port _port)
    {
        DialogueNodePort temp = dialogueNodePorts.Find(port => port.myPort == _port);
        dialogueNodePorts.Remove(temp);

        IEnumerable<Edge> portEdge = graphView.edges.ToList().Where(edge => edge.output == _port);

        if (portEdge.Any())
        {
            Edge edge = portEdge.First();
            edge.input.Disconnect(edge);
            edge.output.Disconnect(edge);
            graphView.RemoveElement(edge);
        }

        _node.outputContainer.Remove(_port);

        //refresh
        _node.RefreshPorts();
        _node.RefreshExpandedState();
    }
}
