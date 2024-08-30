using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(CameraController))]
public class CC_Inspector : Editor
{
    //[SerializeField] private List<Transform> transforms;
    //public override bool RequiresConstantRepaint() => true;

    public override VisualElement CreateInspectorGUI()
    {
        VisualElement new_inspector = new VisualElement();
        VisualTreeAsset inspector_uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Amir/Scripts/CameraController/Scripts/Editor/CC_Inspector_UXML.uxml");

        // Add a simple label.
        //myInspector.Add(new Label("This is a custom Inspector"));

        ///AddContainer(new_inspector);

        ///m_InspectorXML.name = "CameraController/Scripts/Editor/CC_Inspector_UXML.uxml";

        // Load from default reference.
        inspector_uxml.CloneTree(new_inspector);

        CameraController script = (CameraController)target;

        return new_inspector;
    }

    /*public override void OnInspectorGUI()
    {
        //CameraController script = (CameraController)target;

        //GUILayoutOption opt1 = GUILayoutOption(GUILayout.Button();

        ///if (GUILayout.Button("Расставить объекты"))
            ///script.RemoveLastTransform();

        ///if (GUILayout.Button("Убрать объекты"))
            ///script.RemoveLastTransform();

        //_enabled = GUILayout.Toggle(_enabled, "Сам текст");

        ///if (GUILayout.Toggle(_enabled, "Some текст") != _enabled)
            ///script.ShowMesh(!_enabled);

        //if (GUILayoutOption)

        ///GUILayout.BeginHorizontal();

        ///GUILayout.EndHorizontal();

        ///DrawDefaultInspector();
    }*/

    /*void AddContainer(VisualElement new_inspector)
    {
        var container = new IMGUIContainer(() =>
        {
            CameraController script = (CameraController)target;

            var serializedObject = new SerializedObject(script);
            //var property = serializedObject.FindProperty(nameof(transforms));
            serializedObject.Update();
            //EditorGUILayout.PropertyField(property);
            serializedObject.ApplyModifiedProperties();
        });

        new_inspector.Add(container);
    }*/
}
