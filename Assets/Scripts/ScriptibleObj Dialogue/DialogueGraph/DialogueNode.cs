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
    private List<LanguageGeneric<string>> lines = new List<LanguageGeneric<string>>();
    private string nameTxt = "";
    private Sprite characterImage;
    private CharacterImageLocation characterImageLocation;
    private List<DialogueNodePort> dialogueNodePorts = new List<DialogueNodePort>();

    public List<LanguageGeneric<string>> Lines { get => lines; set => lines = value; }
    public string Name { get => nameTxt; set => nameTxt = value; }
    public Sprite CharacterImage { get => characterImage; set => characterImage = value; }
    public CharacterImageLocation CharacterImageLocation { get => characterImageLocation; set => characterImageLocation = value; }
    public List<DialogueNodePort> DialogueNodePorts { get => dialogueNodePorts; set => dialogueNodePorts = value; }

    private TextField lineField;
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
        AddInputPort("Input", Port.Capacity.Multi);

        //Dialogue Lines
        foreach (LanguageType language in (LanguageType[])Enum.GetValues(typeof(LanguageType)))
        {
            lines.Add(new LanguageGeneric<string>
            {
                LanguageType = language,
                LanguageGenericType = ""
            });
        }

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

        //text nameTxt
        Label labelName = new Label("NodeName");
        labelName.AddToClassList("labelName");
        labelName.AddToClassList("Label");
        mainContainer.Add(labelName);
        
        nameField = new TextField("");
        nameField.RegisterValueChangedCallback(value =>
        {
            nameTxt = value.newValue;
        });
        nameField.SetValueWithoutNotify(nameTxt);
        nameField.AddToClassList("TextName");
        mainContainer.Add(nameField);

        // Text Box
        Label textBoxLabel = new Label("Text Box");
        textBoxLabel.AddToClassList("textBoxLabel");
        textBoxLabel.AddToClassList("Label");
        mainContainer.Add(textBoxLabel);

        lineField = new TextField("");
        lineField.RegisterValueChangedCallback(value =>
        {
            lines.Find(text => text.LanguageType == editorWindow.LanguageType).LanguageGenericType = value.newValue;
        });
        lineField.SetValueWithoutNotify(lines.Find(text => text.LanguageType == editorWindow.LanguageType).LanguageGenericType);
        lineField.multiline = true;

        lineField.AddToClassList("TextBox");
        mainContainer.Add(lineField);

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
        nameField.SetValueWithoutNotify(nameTxt);
        lineField.SetValueWithoutNotify(lines.Find(lang => lang.LanguageType == editorWindow.LanguageType).LanguageGenericType);
    }

    public Port AddChoicePort(BaseNode baseNode, DialogueNodePort _dialogueNodePort = null)
    {
        Port port = GetPortInstance(Direction.Output);

        int outputPortCount = baseNode.outputContainer.Query("connector").ToList().Count;
        string outputPortName = $"{outputPortCount + 1}";

        DialogueNodePort dialogueNodePort = new DialogueNodePort();
        dialogueNodePort.PortGuid = Guid.NewGuid().ToString();

        foreach (LanguageType language in (LanguageType[])Enum.GetValues(typeof(LanguageType)))
        {
            dialogueNodePort.TextLanguages.Add(new LanguageGeneric<string>()
            {
                LanguageType = language,
                LanguageGenericType = outputPortName
            });
        }

        if (_dialogueNodePort != null)
        {
            dialogueNodePort.InputGuid = _dialogueNodePort.InputGuid;
            dialogueNodePort.OutputGuid = _dialogueNodePort.OutputGuid;
        }

        // Text for the port
        dialogueNodePort.TextField = new TextField();
        dialogueNodePort.TextField.RegisterValueChangedCallback(value =>
        {
            dialogueNodePort.TextLanguages.Find(language => language.LanguageType == editorWindow.LanguageType).LanguageGenericType = value.newValue;
        });
        dialogueNodePort.TextField.SetValueWithoutNotify(dialogueNodePort.TextLanguages.Find(language => language.LanguageType == editorWindow.LanguageType).LanguageGenericType);
        port.contentContainer.Add(dialogueNodePort.TextField);

        //Delete Button
        Button deleteButton = new Button(() => DeletePort(baseNode, port))
        {
            text = "x"
        };

        port.contentContainer.Add(deleteButton);

        dialogueNodePort.MyPort = port;
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
        DialogueNodePort temp = dialogueNodePorts.Find(port => port.MyPort == _port);
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

    public void ReloadLanguage()
    {
        lineField.RegisterValueChangedCallback(value =>
        {
            lines.Find(text => text.LanguageType == editorWindow.LanguageType).LanguageGenericType = value.newValue;
        });
        lineField.SetValueWithoutNotify(lines.Find(text => text.LanguageType == editorWindow.LanguageType).LanguageGenericType);

        foreach (DialogueNodePort nodePort in dialogueNodePorts)
        {
            nodePort.TextField.RegisterValueChangedCallback(value =>
            {
                nodePort.TextLanguages.Find(language => language.LanguageType == editorWindow.LanguageType).LanguageGenericType = value.newValue;
            });
            nodePort.TextField.SetValueWithoutNotify(nodePort.TextLanguages.Find(language => language.LanguageType == editorWindow.LanguageType).LanguageGenericType);
        }
    }
}
