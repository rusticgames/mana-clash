using UnityEngine;
using UnityEditor;
using System.Collections;

public class Utilities : EditorWindow
{
    Rect platformGroup;
    // Vector3 platformDirection;
    Vector3 platformOffset;
    int platformCopies;
    bool platformButton;
    
    [MenuItem ("Window/Utilities")]
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
	platformButton = GUILayout.Button("Set it");
	if (platformButton) {
	    foreach (var item in Selection.gameObjects) {
		for (int i = 0; i < platformCopies; i++) {
		    Vector3 itemPosition = item.gameObject.transform.position;
		    Object.Instantiate(item, itemPosition + platformOffset * (i + 1), item.transform.rotation);
		}
	    }
	}
	EditorGUILayout.EndVertical();
    }
}
