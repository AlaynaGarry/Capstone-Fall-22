using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class EventNode : BaseNode
{
    private EventSO eventSO;
    private ObjectField objectField;

    public EventSO EventSO{ get => eventSO; set => eventSO = value; }

    public EventNode() { }
    public EventNode(Vector2 position,DialogueEditorWindow _editorWindow, DialogueGraphView _graphView) {
        editorWindow = _editorWindow;
        graphView = _graphView;

        title = "Event";
        SetPosition(new Rect(position, defaultNodeSize));
        nodeGUID = Guid.NewGuid().ToString();

        AddInputPort("Input", Port.Capacity.Multi);
        AddOutputPort("Output", Port.Capacity.Single);

        objectField = new ObjectField()
        {
            objectType = typeof(EventSO),
            allowSceneObjects = false,
            value = eventSO
        };

        objectField.RegisterValueChangedCallback(value => {
            eventSO = objectField.value as EventSO;
        });
       
        objectField.SetValueWithoutNotify(eventSO);

        mainContainer.Add(objectField);
    }

    public override void LoadValueInToField()
    {
        objectField.SetValueWithoutNotify(eventSO);
    }

}
