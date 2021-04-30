using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Thuleanx.Mechanics.Mapping;

[CustomEditor(typeof(SceneHandler))]
public class SceneHandlerEditor : Editor {

	bool showPosition;

	public override void OnInspectorGUI() {
		serializedObject.Update();

		DrawDefaultInspector();
		SceneHandler handler = (SceneHandler) target;
		
		SerializedProperty list = serializedObject.FindProperty("Passages");
		EditorGUI.indentLevel += 1;
		List<int> toRem = new List<int>();
		EditorGUILayout.Separator();
		for (int i = 0; i < list.arraySize; i++) {
			EditorGUILayout.BeginHorizontal();

			EditorGUI.BeginDisabledGroup(true);
			EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), GUIContent.none, GUILayout.Width(Screen.width*.8f));
			EditorGUI.EndDisabledGroup();
			if (GUILayout.Button("-", GUILayout.Width(Screen.width*.1f)))
				toRem.Add(i);

			EditorGUILayout.EndHorizontal();
		}
		EditorGUI.indentLevel -= 1;

		if (GUILayout.Button("+", GUILayout.Height(24))) {
			AddPassage();
		}

		serializedObject.ApplyModifiedProperties();
		foreach (int index in toRem)
			RemovePassage(index);
	}

	public void RemovePassage(int index) {
		SceneHandler handler = (SceneHandler) target;
		Passage passage = handler.Passages[index];

		handler.Passages.RemoveAt(index);
		DestroyImmediate(passage, true);

		AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(handler));
		AssetDatabase.SaveAssets();
	}

	public void AddPassage() {
		SceneHandler handler = (SceneHandler) target;

		#if UNITY_EDITOR
		Passage passage = ScriptableObject.CreateInstance<Passage>();

		AssetDatabase.AddObjectToAsset(passage, handler);
		passage.name = passage.passage_name = string.Format(handler.name+"_"+handler.Passages.Count);
		AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(passage));
		AssetDatabase.SaveAssets();

		handler.Passages.Add(passage);
		#endif

	}
}