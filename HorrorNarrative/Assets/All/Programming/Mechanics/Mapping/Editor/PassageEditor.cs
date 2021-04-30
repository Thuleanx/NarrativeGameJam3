using UnityEngine;
using UnityEditor;
using Thuleanx.Mechanics.Mapping;

[CustomEditor(typeof(Passage))]
public class PassageEditor : Editor {
	int _selected = 0;

	public override void OnInspectorGUI() {
		serializedObject.Update();

		Passage passage = (Passage) target;

		passage.name = passage.passage_name = EditorGUILayout.TextField(passage.passage_name);

		EditorGUILayout.PropertyField(serializedObject.FindProperty("target_scene"));

		EditorGUI.BeginDisabledGroup(passage.target_scene == null);

		EditorGUI.BeginChangeCheck();
		if (passage.target_scene != null) {
			int num = passage.target_scene.Passages.Count;
			string[] _options = new string[num];
			for (int i = 0; i < num; i++)
				_options[i] = passage.target_scene.Passages[i].passage_name;

			// EditorGUILayout.PropertyField(serializedObject.FindProperty("target_passage"));
			_selected = EditorGUILayout.Popup("Target Passage", _selected, _options);

			if (EditorGUI.EndChangeCheck())
				passage.target_passage = passage.target_scene.Passages[_selected];
		} else {
			EditorGUILayout.Popup("Target Passage", 0, new string[]{"None"});
		}
		EditorGUI.EndDisabledGroup();

		serializedObject.ApplyModifiedProperties();
	}
}