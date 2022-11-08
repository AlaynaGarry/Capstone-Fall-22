using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueGraphView : GraphView
{
    private string styleSheetName = "GraphViewStyleSheet";
    private DialogueEditorWindow editorWindow;

    private NodeSearchWindow searchWindow;

    public DialogueGraphView(DialogueEditorWindow editorWindow)
    {
        this.editorWindow = editorWindow;

        StyleSheet tempStyleSheet = Resources.Load<StyleSheet>(styleSheetName);
        styleSheets.Add(tempStyleSheet);

        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
        this.AddManipulator(new FreehandSelector());

        GridBackground grid = new GridBackground();
        Insert(0, grid);
        grid.StretchToParentSize();

        AddSearchWindow();
    }

    private void AddSearchWindow()
    {
        searchWindow = ScriptableObject.CreateInstance<NodeSearchWindow>();
        searchWindow.Configure(editorWindow, this);
        nodeCreationRequest = context => SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), searchWindow);
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        List<Port> compatiblePorts = new List<Port>();
        Port startPortView = startPort;

        ports.ForEach((port) =>
        {
            Port portView = port;
            if (startPort != portView && startPort.node != portView.node)
            {
                compatiblePorts.Add(port);
            }
        });

        return compatiblePorts;
    }

    public StartNode CreateStartNode(Vector2 position)
    {
       StartNode temp = new StartNode(position, editorWindow, this);

        return temp;
    }    
    public EndNode CreateEndNode(Vector2 position)
    {
       EndNode temp = new EndNode(position, editorWindow, this);

        return temp;
    }    
    public EventNode CreateEventNode(Vector2 position)
    {
       EventNode temp = new EventNode(position, editorWindow, this);

        return temp;
    }    
    public DialogueNode CreateDialogueNode(Vector2 position)
    {
       DialogueNode temp = new DialogueNode(position, editorWindow, this);

        return temp;
    }    
}
