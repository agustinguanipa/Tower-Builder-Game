using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(AutoMap))]
public class AutoEditor : Editor {
	AutoMap autoMap; 

	public void OnEnable() {
		autoMap = (AutoMap)target; 
	}

	public override void OnInspectorGUI(){
		GUILayout.BeginVertical(); GUILayout.Label(" AutoModel Ver.1.0 ");

		GUILayout.BeginHorizontal();
		GUILayout.Label("tileX: ");
		autoMap.tileX = EditorGUILayout.IntField(autoMap.tileX, GUILayout.Width(100)); 
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.Label("tileY: ");
		autoMap.tileY = EditorGUILayout.IntField(autoMap.tileY, GUILayout.Width(100)); 
		GUILayout.EndHorizontal();

		GUILayout.EndVertical();
	}
}
