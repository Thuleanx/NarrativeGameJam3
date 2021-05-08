using UnityEngine;
using UnityEditor;
using Thuleanx;
using Thuleanx.Master.Global;
using Thuleanx.Controls;
using FMOD_Thuleanx;

[CustomEditor(typeof(App))]
public class AppEditor : Editor {
	public override void OnInspectorGUI() {
		DrawDefaultInspector();
		if (GUILayout.Button("Find Managers", GUILayout.Height(24)))
			CollectManagers();
	}

	public void CollectManagers() {
		App app = (App) target;

		app._GameModeManager = app.GetComponentInChildren<GameModeManager>(true);
		app._InputManager = app.GetComponentInChildren<InputManager>(true);
		app._AudioManager = app.GetComponentInChildren<AudioManager>(true);
	}
}