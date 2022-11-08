using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class NodeSearchWindow : ScriptableObject, ISearchWindowProvider
{
    private DialogueEditorWindow editorWindow;
    private DialogueGraphView graphView;

    public void Configure(DialogueEditorWindow _editorWindow, DialogueGraphView _graphView)
    {
        editorWindow = _editorWindow;
        graphView = _graphView;
    }

    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        List<SearchTreeEntry> tree = new List<SearchTreeEntry>()
        {
            new SearchTreeGroupEntry(new GUIContent("Dialogue Node"), 0),
            new SearchTreeGroupEntry(new GUIContent("Dialogue"), 1),
            AddNodeSeach("Start Node", new StartNode()),
            AddNodeSeach("End Node", new EndNode()),
            AddNodeSeach("Event Node", new EventNode()),
            AddNodeSeach("Dialogue Node", new DialogueNode())
        };

        return tree;
    }

    private SearchTreeEntry AddNodeSeach(string name, BaseNode baseNode)
    {
        SearchTreeEntry temp = new SearchTreeEntry(new GUIContent(name))
        {
            level = 2,
            userData = baseNode
        };
        return temp;
    }

    public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
    {
        Vector2 mousePosition = editorWindow.rootVisualElement.ChangeCoordinatesTo(editorWindow.rootVisualElement.parent, context.screenMousePosition - editorWindow.position.position);
        Vector2 graphMousePosition = graphView.contentViewContainer.WorldToLocal(mousePosition);

        return CheckForNodeType(searchTreeEntry, graphMousePosition);
    }

    private bool CheckForNodeType(SearchTreeEntry searchTreeEntry, Vector2 pos)
    {
        switch (searchTreeEntry.userData)
        {
            case StartNode node:
                graphView.AddElement(graphView.CreateStartNode(pos));
                return true;
            case EndNode node:
                graphView.AddElement(graphView.CreateEndNode(pos));
                return true;
            case EventNode node:
                graphView.AddElement(graphView.CreateEventNode(pos));
                return true;
            case DialogueNode node:
                graphView.AddElement(graphView.CreateDialogueNode(pos));
                return true;
            default: 
                break;
        }
        return false;
    }
}
