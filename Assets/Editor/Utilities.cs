using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class Utilities : EditorWindow
{
    Rect platformGroup;
    // Vector3 platformDirection;
    Vector3 platformOffset = new Vector3(1,0,0);
    int platformCopies = 3;
    bool platformButton;
    bool addCopiesAsChildrenOfOriginal = true;

    [MenuItem("Window/Utilities")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(Utilities));
    }

    void OnGUI()
    {
        GUILayout.Label("Platform Builder", EditorStyles.boldLabel);

        platformGroup = EditorGUILayout.BeginVertical();
        // platformDirection = EditorGUILayout.Vector2Field("Platform Size", platformDirection);
        platformOffset = EditorGUILayout.Vector3Field("Platform Offset", platformOffset);
        platformCopies = EditorGUILayout.IntField("Platform Copies", platformCopies);
        addCopiesAsChildrenOfOriginal = EditorGUILayout.Toggle("Add Copies as Children of Original", addCopiesAsChildrenOfOriginal);
        platformButton = GUILayout.Button("Clone");
        if (platformButton && platformCopies >= 0)
        {
            var newSelection = new List<Object>();
            Object o = null;
            GameObject clonee = null;
            foreach (var item in Selection.gameObjects)
            {
                clonee = item;
                for (int i = 0; i < platformCopies; i++)
                {
                    Vector3 itemPosition = clonee.gameObject.transform.position;
                    o = Object.Instantiate(clonee, itemPosition + platformOffset, clonee.transform.rotation);
										if(addCopiesAsChildrenOfOriginal) ((GameObject)o).transform.parent = clonee.transform;
                    o.name = clonee.name;
                    Undo.RegisterCreatedObjectUndo(o, "platform clone created");
                    clonee = o as GameObject;
                }
                newSelection.Add(o);
            }
            Selection.objects = newSelection.ToArray();
        }
        EditorGUILayout.EndVertical();
    }
}
