using UnityEngine;
using UnityEditor;
using Thuleanx.Mechanics.Danmaku;

[CustomEditor(typeof(OrbitalSpawnerNeon))]
public class OrbitalSpawnerNeonEditor: Editor {
	public override void OnInspectorGUI() {
		serializedObject.Update();
		DrawDefaultInspector();

		OrbitalSpawnerNeon spawner = (OrbitalSpawnerNeon) target;

		EditorGUILayout.BeginHorizontal();

		EditorGUI.BeginDisabledGroup(spawner.Active || !Application.isPlaying || spawner.OrbitoidPool == null || spawner.Pattern == null || spawner.Array == null);
		if (GUILayout.Button("Play", new GUILayoutOption[]{
			GUILayout.Width(.5f * Screen.width), 
			GUILayout.Height(32)} )) {
			
			spawner.Activate();
		}
		EditorGUI.EndDisabledGroup();

		EditorGUI.BeginDisabledGroup(!spawner.Active || !Application.isPlaying || spawner.OrbitoidPool == null || spawner.Pattern == null || spawner.Array == null);
		if (GUILayout.Button("Stop", new GUILayoutOption[]{
			GUILayout.Width(.5f * Screen.width), 
			GUILayout.Height(32)} )) {
			
			spawner.Stop();
		}
		EditorGUI.EndDisabledGroup();

		EditorGUILayout.EndHorizontal();
		serializedObject.ApplyModifiedProperties();
	}
}