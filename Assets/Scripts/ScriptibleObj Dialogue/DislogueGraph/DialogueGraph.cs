using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class DialogueGraph : EditorWindow
{
    private DialogueGraphView graphView;

    [MenuItem("Graph/Dialogue Graph")]
    public static void OpenDialogieGraophWindow()
    {
        var window = GetWindow<DialogueGraph>();
        window.titleContent = new GUIContent("Dialogue Graph");
    }

    private void OnEnable()
    {
        ConstructGraphView();
        GenerateToolBar();
    }

    private void OnDisable()
    {
        rootVisualElement.Remove(graphView);
    }

    private void ConstructGraphView()
    {
        graphView = new DialogueGraphView
        {
            name = "Dialogue Graph"
        };

        graphView.StretchToParentSize();
        rootVisualElement.Add(graphView);
    }

    public void GenerateToolBar()
    {
        var toolbar = new Toolbar();

        var nodeCreationButton = new Button(()=> { graphView.CreateNode("Dialogue Node"); });
        nodeCreationButton.text = "Create Node";

        toolbar.Add(nodeCreationButton);

        rootVisualElement.Add(toolbar);
    }
}
