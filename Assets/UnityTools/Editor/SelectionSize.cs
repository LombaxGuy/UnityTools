using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityTools.Attributes;

public class SelectionSize : ScriptableObject
{
    private enum DecimalDigitCount { None = 0, One = 1, Two = 2, Three = 3, Four = 4, Five = 5, Six = 6, All = 7 }
    private enum BoundsType { Both = 0, Renderer = 1, Collider = 2 }

    private const string menuPath = "Tools/UnityTools/Measuring tool";
    private const string menuSettingsPath = "Tools/UnityTools/Measuring tool settings";
    private const string lableName = "dimensions";
    private const string settingsName = "SelectionSizeToolSettings";
    private const string settingsPath = "Assets/UnityTools/Resources/" + settingsName + ".asset";

    private static SelectionSize instance;


    [Tooltip("Whether or not the tool is enabled. \n\nThe tool can be activated from the menu: \nTools > UnityTools > Measuring tool")]
    [SerializeField] [ReadOnly] private bool enabled;

    [Header("Settings")]
    [SerializeField] private BoundsType type = BoundsType.Renderer;

    [Header("Label")]
    [SerializeField] private float fontSize = 12f;
    [SerializeField] private Color labelTextColor = Color.white;
    [Space]
    [SerializeField] private DecimalDigitCount decimals = DecimalDigitCount.All;

    [Header("Outline")]
    [SerializeField] private Color outlineFrameColor = Color.red;
    [SerializeField] private Color outlineFaceColor = new Color(1, 0, 0, 0);

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
    [DidReloadScripts]
    private static void OnRecompile()
    {
        if (Instance.enabled)
        {
            Instance.Enable();
        }
        else
        {
            Instance.Disable();
        }
    }

    [MenuItem(menuPath, priority = 101)]
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

        EditorUtility.SetDirty(Instance);
    }

    [MenuItem(menuSettingsPath, priority = 102)]
    private static void ToolSettings()
    {
        Selection.activeObject = Instance;
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
        if (sceneViews != null)
        {
            foreach (var sceneView in sceneViews)
            {
                RemoveLabel(sceneView);
            }
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
        label.style.color = labelTextColor;
        label.style.fontSize = fontSize;

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

        // update the labels color
        if (label.style.color != labelTextColor)
        {
            label.style.color = labelTextColor;
        }

        if (label.style.fontSize != fontSize)
        {
            label.style.fontSize = fontSize;
        }

        // update the labels position
        label.transform.position = new Vector3(fontSize / 2, sceneView.position.height - (fontSize + fontSize / 2), 0);
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
            if (type == BoundsType.Renderer)
            {
                if (gameObject.TryGetComponent(out Renderer renderer))
                {
                    newBounds.Encapsulate(renderer.bounds);
                }
            }
            else if (type == BoundsType.Collider)
            {
                if (gameObject.TryGetComponent(out Collider collider))
                {
                    newBounds.Encapsulate(collider.bounds);
                }
            }
            else
            {
                if (gameObject.TryGetComponent(out Collider collider))
                {
                    newBounds.Encapsulate(collider.bounds);
                }

                if (gameObject.TryGetComponent(out Renderer renderer))
                {
                    newBounds.Encapsulate(renderer.bounds);
                }
            }
        }

        return newBounds;
    }

    private string BoundsToString(Bounds bounds)
    {
        string x;
        string y;
        string z;

        if (decimals == DecimalDigitCount.All)
        {
            x = bounds.size.x.ToString();
            y = bounds.size.y.ToString();
            z = bounds.size.z.ToString();
        }
        else
        {
            x = bounds.size.x.ToString($"F{(int)decimals}");
            y = bounds.size.y.ToString($"F{(int)decimals}");
            z = bounds.size.z.ToString($"F{(int)decimals}");
        }

        return $"Selection Size: ({x}, {y}, {z})";
    }

    private static void DrawBounds(SceneView sceneView)
    {
        Bounds bounds = Instance.currentBounds;

        // if the bounds doesn't exist don't draw anyting
        if (bounds.size == Vector3.zero)
            return;

        if (Instance.outlineFaceColor.a == 0)
        {
            // set the color of the bounding box
            Handles.color = Instance.outlineFrameColor;

            // draws the bounding box
            Handles.DrawWireCube(bounds.center, bounds.size);
        }
        else
        {
            Vector3 max = bounds.max;
            Vector3 min = bounds.min;
            Vector3 frontTopLeft = new Vector3(min.x, max.y, max.z);
            Vector3 frontBottomLeft = new Vector3(min.x, min.y, max.z);
            Vector3 frontBottomRight = new Vector3(max.x, min.y, max.z);
            Vector3 backTopRight = new Vector3(max.x, max.y, min.z);
            Vector3 backTopLeft = new Vector3(min.x, max.y, min.z);
            Vector3 backBottomRight = new Vector3(max.x, min.y, min.z);

            // front face
            Vector3[] frontFace = { max, frontTopLeft, frontBottomLeft, frontBottomRight };
            // back face
            Vector3[] backFace = { min, backBottomRight, backTopRight, backTopLeft };
            // top face
            Vector3[] topFace = { max, frontTopLeft, backTopLeft, backTopRight };
            // bottom face
            Vector3[] bottomFace = { min, backBottomRight, frontBottomRight, frontBottomLeft };
            // right face
            Vector3[] rightFace = { max, backTopRight, backBottomRight, frontBottomRight };
            // left face
            Vector3[] leftFace = { min, frontBottomLeft, frontTopLeft, backTopLeft };

            Handles.DrawSolidRectangleWithOutline(frontFace, Instance.outlineFaceColor, Instance.outlineFrameColor);
            Handles.DrawSolidRectangleWithOutline(backFace, Instance.outlineFaceColor, Instance.outlineFrameColor);
            Handles.DrawSolidRectangleWithOutline(topFace, Instance.outlineFaceColor, Instance.outlineFrameColor);
            Handles.DrawSolidRectangleWithOutline(bottomFace, Instance.outlineFaceColor, Instance.outlineFrameColor);
            Handles.DrawSolidRectangleWithOutline(rightFace, Instance.outlineFaceColor, Instance.outlineFrameColor);
            Handles.DrawSolidRectangleWithOutline(leftFace, Instance.outlineFaceColor, Instance.outlineFrameColor);
        }
    }
}
