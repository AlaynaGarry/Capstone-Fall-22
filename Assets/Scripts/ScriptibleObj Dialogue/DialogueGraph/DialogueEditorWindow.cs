using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class DialogueEditorWindow : EditorWindow
{
    private DialogueContainerObject currentContainerObject;
    private DialogueGraphView graphView;

    private DialogueSaveAndLoad saveAndLoad;

    private ToolbarMenu toolbarMenu;
    private LanguageType languageType = LanguageType.English;
    private Label nameOfDialogueContainer;

    public LanguageType LanguageType { get => languageType; set => languageType = value; }

    [MenuItem("Graph/Dialogue Graph"), OnOpenAsset(1)]
    public static bool OpenDialogueGraphWindow(int instanceID, int line)
    {
        UnityEngine.Object item = EditorUtility.InstanceIDToObject(instanceID);

        if (item is DialogueContainerObject)
        {
            DialogueEditorWindow window = (DialogueEditorWindow)GetWindow(typeof(DialogueEditorWindow));
            window.titleContent = new GUIContent("Dialogue Editor");
            window.currentContainerObject = item as DialogueContainerObject;
            window.minSize = new Vector2(500, 250);
            window.Load();
        }
        return false;
    }

    private void OnEnable()
    {
        ConstructGraphView();
        GenerateToolBar();
        Load();
    }

    private void OnDisable()
    {
        rootVisualElement.Remove(graphView);
    }

    private void Save()
    {
        if (currentContainerObject != null)
        {
            saveAndLoad.Save(currentContainerObject);
        }
    }

    private void Load()
    {
        if (currentContainerObject != null)
        {
            Language(LanguageType.English, toolbarMenu);
            nameOfDialogueContainer.text = "NodeName:   " + currentContainerObject.name;
            saveAndLoad.Load(currentContainerObject);
        }
    }

    private void ConstructGraphView()
    {
        graphView = new DialogueGraphView(this);
        graphView.StretchToParentSize();
        rootVisualElement.Add(graphView);

        saveAndLoad = new DialogueSaveAndLoad(graphView);
    }

    private void GenerateToolBar()
    {
        StyleSheet style = Resources.Load<StyleSheet>("GraphViewStyleSheet");
        rootVisualElement.styleSheets.Add(style);

        Toolbar toolbar = new Toolbar();
        //save button
        Button saveBtn = new Button()
        {
            text = "save",
        };
        saveBtn.clicked += () => { Save(); };
        toolbar.Add(saveBtn);

        //load button
        Button loadBtn = new Button()
        {
            text = "Load",
        };
        loadBtn.clicked += () => { Load(); };
        toolbar.Add(loadBtn);

        // Dropdown menu for languages.
        toolbarMenu = new ToolbarMenu();
        foreach (LanguageType language in (LanguageType[])Enum.GetValues(typeof(LanguageType)))
        {
            toolbarMenu.menu.AppendAction(language.ToString(), new Action<DropdownMenuAction>(x => Language(language, toolbarMenu)));
        }
        toolbar.Add(toolbarMenu);

        //NodeName of current DialogueContainerObject you have open.
        nameOfDialogueContainer = new Label("");
        toolbar.Add(nameOfDialogueContainer);
        nameOfDialogueContainer.AddToClassList(".nameOfDialogueContainer");

        rootVisualElement.Add(toolbar);
    }
    private void Language(LanguageType _language, ToolbarMenu _toolbarMenu)
    {
        toolbarMenu.text = "Language: " + _language.ToString();
        languageType = _language;
        graphView.LanguageReload();
    }
}
