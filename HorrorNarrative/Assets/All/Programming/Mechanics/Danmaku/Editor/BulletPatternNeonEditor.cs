using UnityEngine;
using UnityEditor;
using Thuleanx.Mechanics.Danmaku;

[CustomEditor(typeof(BulletPatternNeon))]
public class BulletPatternNeonEditor : Editor {
	public override void OnInspectorGUI() {
		serializedObject.Update();

		BulletPatternNeon pattern = (BulletPatternNeon) target;

		EditorGUILayout.LabelField("Array Description", EditorStyles.boldLabel);
		pattern.TotalBulletArrays = EditorGUILayout.IntSlider("Total Bullet Arrays", pattern.TotalBulletArrays, 1, 30);
		pattern.ArrayDegreeSpread = EditorGUILayout.Slider("Array Spread Degrees", pattern.ArrayDegreeSpread, -360f, 360f);

		EditorGUILayout.Separator();

		pattern.Spin = EditorGUILayout.BeginToggleGroup("Spin", pattern.Spin);
			EditorGUI.indentLevel++;

			EditorGUILayout.PropertyField(serializedObject.FindProperty("LimitStyle"));
			pattern.SpinStartVelocity = EditorGUILayout.Slider("Starting Spin Velocity", pattern.SpinStartVelocity, -360f, 360f);

			EditorGUILayout.LabelField("Limit Velocity", EditorStyles.boldLabel);
			EditorGUI.BeginDisabledGroup(pattern.LimitStyle != BulletPatternNeon.SpinLimitStyle.Velocity);
				EditorGUI.indentLevel++;
				pattern.SpinVelocityAccel = EditorGUILayout.Slider("Spin Accel", pattern.SpinVelocityAccel, 0, 100f);
				pattern.SpinRateMax = EditorGUILayout.Slider("Spin Rate Max", pattern.SpinRateMax, 0, 3000f);
				pattern.InvertSpin = EditorGUILayout.Toggle("Invert Spin", pattern.InvertSpin);
				EditorGUI.indentLevel--;
			EditorGUI.EndDisabledGroup();


			EditorGUILayout.LabelField("Limit Area", EditorStyles.boldLabel);
			EditorGUI.BeginDisabledGroup(pattern.LimitStyle != BulletPatternNeon.SpinLimitStyle.Area);
				EditorGUI.indentLevel++;
				pattern.SpinArcMax = EditorGUILayout.Slider("Spin Arc", pattern.SpinArcMax, 0, 180f);
				pattern.Period = EditorGUILayout.Slider("Spin Period", pattern.Period, 0.1f, 180f);
				EditorGUI.indentLevel--;
			EditorGUI.EndDisabledGroup();

			EditorGUI.indentLevel--;
		EditorGUILayout.EndToggleGroup();

		EditorGUILayout.Separator();
		EditorGUILayout.LabelField("Bullet Description", EditorStyles.boldLabel);
		pattern.BulletSpeed = EditorGUILayout.Slider("Bullet Speed", pattern.BulletSpeed, 0, 30f);
		pattern.BulletsPerMinute = EditorGUILayout.Slider("Bullet Per Minute", pattern.BulletsPerMinute, 0, 2000f);

		EditorGUILayout.Separator();
		EditorGUILayout.LabelField("Bullet Spawn Offset", EditorStyles.boldLabel);
		pattern.ObjectRadius = EditorGUILayout.Slider("Radius", pattern.ObjectRadius, 0, 20f);

		serializedObject.ApplyModifiedPropertiesWithoutUndo();
		EditorUtility.SetDirty(target);
	}
}