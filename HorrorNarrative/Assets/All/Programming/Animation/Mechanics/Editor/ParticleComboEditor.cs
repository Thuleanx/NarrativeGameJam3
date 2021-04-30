using UnityEngine;
using UnityEditor;
using Thuleanx.Animation;

[CustomEditor(typeof(ParticleCombo))]
public class ParticleComboEditor : Editor {
	public override void OnInspectorGUI() {
		DrawDefaultInspector();
		ParticleCombo Combo = (ParticleCombo) target;

		EditorGUILayout.BeginHorizontal();

		EditorGUI.BeginDisabledGroup(Combo.Active || !Application.isPlaying);
		if (GUILayout.Button("Activate", GUILayout.Width(.5f * Screen.width), GUILayout.Height(32f)))
			Combo.Activate();
		EditorGUI.EndDisabledGroup();

		EditorGUI.BeginDisabledGroup(!Combo.Active || !Application.isPlaying);
		if (GUILayout.Button("Stop", GUILayout.Width(.5f * Screen.width), GUILayout.Height(32f)))
			Combo.Stop();
		EditorGUI.EndDisabledGroup();

		EditorGUILayout.EndHorizontal();
	}
}