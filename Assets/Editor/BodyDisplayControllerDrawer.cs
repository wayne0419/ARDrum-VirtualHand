using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(BodyDisplayController.BodyGroup))]
public class BodyDisplayControllerDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty groupName = property.FindPropertyRelative("groupName");
        SerializedProperty renderers = property.FindPropertyRelative("renderers");
        SerializedProperty color = property.FindPropertyRelative("color");

        Rect groupNameRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        Rect renderersRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + 2, position.width, EditorGUI.GetPropertyHeight(renderers));
        Rect colorRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + EditorGUI.GetPropertyHeight(renderers) + 4, position.width, EditorGUIUtility.singleLineHeight);

        EditorGUI.PropertyField(groupNameRect, groupName);
        EditorGUI.PropertyField(renderersRect, renderers, true);
        Color newColor = EditorGUI.ColorField(colorRect, "Color", color.colorValue);

        if (newColor != color.colorValue)
        {
            color.colorValue = newColor;
            property.serializedObject.ApplyModifiedProperties(); // 確保即時更新
            // 找到並更新對應的 BodyGroup
            BodyDisplayController controller = (BodyDisplayController)property.serializedObject.targetObject;
            foreach (var bodyGroup in controller.bodyGroups)
            {
                if (bodyGroup.groupName == groupName.stringValue)
                {
                    controller.UpdateColor(bodyGroup);
                    break;
                }
            }
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight + EditorGUI.GetPropertyHeight(property.FindPropertyRelative("renderers")) + EditorGUIUtility.singleLineHeight + 6;
    }
}
