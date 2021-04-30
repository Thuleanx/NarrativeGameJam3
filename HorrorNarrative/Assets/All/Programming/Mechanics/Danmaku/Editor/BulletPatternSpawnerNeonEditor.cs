using UnityEngine;
using UnityEditor;
using Thuleanx.Mechanics.Danmaku;

[CustomEditor(typeof(BulletPatternSpawnerNeon))]
public class BulletPatternSpawnerNeonEditor : Editor {
	public override void OnInspectorGUI() {
		serializedObject.Update();
		DrawDefaultInspector();

		BulletPatternSpawnerNeon spawner = (BulletPatternSpawnerNeon) target;

		// EditorGUILayout.LabelField("Current Degree: ", spawner.CurrentDegree.ToString());
		// EditorGUILayout.LabelField("Current Spin: ", spawner.SpinRate.ToString());
		// EditorGUILayout.LabelField("SpinInverted: ", spawner.SpinInverted?"Yes":"No");

		// Play / Disable Button
		EditorGUILayout.BeginHorizontal();

		EditorGUI.BeginDisabledGroup(spawner.Active || !Application.isPlaying || spawner.BulletPool == null || spawner.Pattern == null || spawner.Array == null);
		if (GUILayout.Button("Play", new GUILayoutOption[]{
			GUILayout.Width(.33f * Screen.width), 
			GUILayout.Height(32)} )) {
			
			spawner.Activate();
		}
		EditorGUI.EndDisabledGroup();

		EditorGUI.BeginDisabledGroup(!spawner.Active || !Application.isPlaying || spawner.BulletPool == null || spawner.Pattern == null || spawner.Array == null);
		if (GUILayout.Button("Stop", new GUILayoutOption[]{
			GUILayout.Width(.33f * Screen.width), 
			GUILayout.Height(32)} )) {
			
			spawner.Disable();
		}
		EditorGUI.EndDisabledGroup();

		EditorGUI.BeginDisabledGroup(spawner.Active || !Application.isPlaying || spawner.BulletPool == null || spawner.Pattern == null || spawner.Array == null);
		if (GUILayout.Button("Burst", new GUILayoutOption[]{
			GUILayout.Width(.33f * Screen.width), 
			GUILayout.Height(32)} )) {
			
			spawner.Burst();
		}
		EditorGUI.EndDisabledGroup();


		EditorGUILayout.EndHorizontal();

		serializedObject.ApplyModifiedProperties();
	}
}