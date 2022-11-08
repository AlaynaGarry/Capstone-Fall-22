using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class StartNode : BaseNode
{
    public StartNode() { }
    public StartNode(Vector2 position, DialogueEditorWindow _editorWindow, DialogueGraphView _graphView) {
        editorWindow = _editorWindow;
        graphView = _graphView;

        title = "Start";
        SetPosition(new Rect (position, defaultNodeSize));
        nodeGUID = Guid.NewGuid().ToString();

        AddOutputPort("Output", Port.Capacity.Single);

        RefreshExpandedState();
        RefreshPorts();
    }

}
