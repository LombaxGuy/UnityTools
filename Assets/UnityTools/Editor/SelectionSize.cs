using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityTools.Extensions;

public class SelectionSize : ScriptableObject
{
    private const string menuPath = "Tools/UnityTools/Measuring tool";
    private const string lableName = "dimensions";
    private const string settingsName = "SelectionSizeToolSettings";
    private const string settingsPath = "Assets/UnityTools/Resources/" + settingsName + ".asset";

    private static SelectionSize instance;

    private bool enabled = false;

    private float lableHeight = 20;

    private SceneView[] sceneViews;

    private Bounds currentBounds;

    public static SelectionSize Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = Resources.Load<SelectionSize>(settingsName);

                if (instance == null)
                {
                    instance = CreateInstance<SelectionSize>();
                    instance.name = settingsName;

                    AssetDatabase.CreateAsset(instance, settingsPath);

                    AssetDatabase.Refresh();
                }

                return instance;
            }
            else
            {
                return instance;
            }
        }
    }

    #region Menu methods
    [MenuItem(menuPath)]
    private static void ToogleMenuItem()
    {
        // toggle the enabled state of the tool
        Instance.enabled = !Instance.enabled;

        // if the new state is enabled, enable the tool
        if (Instance.enabled)
        {
            Instance.Enable();
        }
        // if the new state is not enabled, disable the tool
        else
        {
            Instance.Disable();
        }
    }

    // Validate function for above menu item
    [MenuItem(menuPath, true)]
    private static bool ToggleMenuItemValidate()
    {
        Menu.SetChecked(menuPath, Instance.enabled);

        return true;
    }
    #endregion

    private void Enable()
    {
        EditorApplication.update += EditorUpdate;
        SceneView.duringSceneGui += DrawBounds;
    }

    private void Disable()
    {
        foreach (var sceneView in sceneViews)
        {
            RemoveLabel(sceneView);
        }

        EditorApplication.update -= EditorUpdate;
        SceneView.duringSceneGui -= DrawBounds;
    }

    /// <summary>
    /// Editor update is only called when the tool is active
    /// </summary>
    private void EditorUpdate()
    {
        sceneViews = Resources.FindObjectsOfTypeAll<SceneView>();

        if (sceneViews.Length <= 0)
        {
            return;
        }
        else
        {
            foreach (var sceneView in sceneViews)
            {
                UpdateLabel(sceneView);
            }

            currentBounds = SelectedBounds();
        }
    }

    #region Label methods
    /// <summary>
    /// Removes the selection size label of a given scene view if said scene view constains the label.
    /// </summary>
    /// <param name="sceneView">The scene view to remove the label from.</param>
    private void RemoveLabel(SceneView sceneView)
    {
        // find the label of the provided scene view
        Label label = sceneView.rootVisualElement.contentContainer.Q<Label>(lableName);

        // if the label exist, remove it from the scene view
        if (label != null)
        {
            // add the label
            sceneView.rootVisualElement.Remove(label);
        }
    }

    /// <summary>
    /// Creates the selection size label in a given scene view and returns the label instance.
    /// </summary>
    /// <param name="sceneView">The scene view to create the lable in.</param>
    private Label CreateLabel(SceneView sceneView)
    {
        // creates a new label instance and sets its name
        Label label = new Label();
        label.name = lableName;

        // adds the label to the scene view
        sceneView.rootVisualElement.Add(label);

        return label;
    }

    /// <summary>
    /// Updates the current scene views label. If no label exist in the scene view a new one is created.
    /// </summary>
    /// <param name="sceneView"></param>
    private void UpdateLabel(SceneView sceneView)
    {
        // find the label of the provided scene view
        Label label = sceneView.rootVisualElement.contentContainer.Q<Label>(lableName);

        // if the label does not exist, create it
        if (label == null)
        {
            // add the label
            label = CreateLabel(sceneView);
        }

        // update the labels text
        label.text = BoundsToString(currentBounds);

        // update the labels position
        label.transform.position = new Vector3(5, sceneView.position.height - lableHeight, 0);
    }
    #endregion

    private Bounds SelectedBounds()
    {
        GameObject[] selected = Selection.gameObjects;

        Bounds newBounds = new Bounds();

        // if nothing is selected return empty bounds
        if (selected.Length <= 0)
            return newBounds;

        newBounds.center = selected[0].transform.position;

        foreach (var gameObject in selected)
        {
            if (gameObject.TryGetComponent(out Renderer renderer))
            {
                newBounds.Encapsulate(renderer.bounds);
            }
        }

        return newBounds;
    }

    private string BoundsToString(Bounds bounds)
    {
        return $"Selection Size: ({bounds.size.x},{bounds.size.y},{bounds.size.z})";
    }

    private static void DrawBounds(SceneView sceneView)
    {
        Bounds bounds = Instance.currentBounds;

        // if the bounds doesn't exist don't draw anyting
        if (bounds.size == Vector3.zero)
            return;

        // set the color of the bounding box
        Handles.color = Color.red;

        // draws the bounding box
        Handles.DrawWireCube(bounds.center, bounds.size);

    }
}
