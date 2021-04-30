using UnityEngine;
using UnityEditor;
using Thuleanx.Mechanics.Danmaku;

[CustomEditor(typeof(BulletPattern))]
public class BulletPatternEditor : Editor {
	public override void OnInspectorGUI() {
		serializedObject.Update();

		BulletPattern pattern = (BulletPattern) target;

		EditorGUILayout.PropertyField(serializedObject.FindProperty("array"));

		EditorGUILayout.LabelField("Array Description", EditorStyles.boldLabel);
		pattern.TotalBulletArrays = EditorGUILayout.IntSlider("Total Bullet Arrays", pattern.TotalBulletArrays, 1, 30);
		pattern.DegreeSpreadBetweenArrays = EditorGUILayout.Slider("Array Spread Degrees", pattern.DegreeSpreadBetweenArrays, 0, 360f);
		pattern.StartingDegree = EditorGUILayout.Slider("Starting Degree", pattern.StartingDegree, 0, 360);

		EditorGUILayout.Separator();
		pattern.Spin = EditorGUILayout.BeginToggleGroup("Spin", pattern.Spin);
			EditorGUI.indentLevel++;
			pattern.StartingSpinRate = EditorGUILayout.Slider("Starting Rate", pattern.StartingSpinRate, 0, 360f);
			pattern.SpinRateAccel = EditorGUILayout.Slider("Spin Accel", pattern.SpinRateAccel, 0, 100f);
			pattern.LimitSpin = EditorGUILayout.BeginToggleGroup("Limit Spin", pattern.LimitSpin);
				EditorGUI.indentLevel++;
				pattern.SpinRateMax = EditorGUILayout.Slider("Spin Rate Max", pattern.SpinRateMax, 0, 3000f);
				pattern.InvertSpin = EditorGUILayout.Toggle("Invert Spin", pattern.InvertSpin);
				EditorGUI.indentLevel--;
			EditorGUILayout.EndToggleGroup();
			EditorGUI.indentLevel--;
		EditorGUILayout.EndToggleGroup();

		EditorGUILayout.Separator();
		EditorGUILayout.LabelField("Bullet Description", EditorStyles.boldLabel);
		pattern.BulletSpeed = EditorGUILayout.Slider("Bullet Speed", pattern.BulletSpeed, 0, 30f);
		pattern.BulletsPerMinute = EditorGUILayout.Slider("Bullet Per Minute", pattern.BulletsPerMinute, 0, 2000f);

		EditorGUILayout.Separator();
		EditorGUILayout.LabelField("Bullet Spawn Offset", EditorStyles.boldLabel);
		pattern.Offset = EditorGUILayout.BeginToggleGroup("Offset", pattern.Offset);
			EditorGUI.indentLevel++;
			pattern.ObjectRadius = EditorGUILayout.Slider("Radius", pattern.ObjectRadius, 0, 20f);
			pattern.OffsetCenter.x = EditorGUILayout.Slider("Displacement X", pattern.OffsetCenter.x, -20f, 20f);
			pattern.OffsetCenter.y = EditorGUILayout.Slider("Displacement Y", pattern.OffsetCenter.y, -20f, 20f);
			EditorGUI.indentLevel--;
		EditorGUILayout.EndToggleGroup();

		serializedObject.ApplyModifiedProperties();
	}
}