using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Thuleanx.Mechanics.Mapping;

namespace Thuleanx.Master.Global {
	public class GameModeManager : MonoBehaviour {
		bool _isSwitching = false;
		[HideInInspector]
		public GameMode _current_mode;

		StoryMode _storyMode = new StoryMode();
		MainMenuMode _mainMenuMode = new MainMenuMode();

		private void Awake() {
			Time.timeScale = 0;

			#if UNITY_EDITOR
				App.IsEditor = true;
				switch (SceneManager.GetActiveScene().buildIndex) {
					case 0: 
						break;
					case 1:
						_current_mode = _mainMenuMode;
						_current_mode.OnEditorStart();
						break;
					default:
						_current_mode = _storyMode;
						_current_mode.OnEditorStart();
						break;
				}
			#else
			#endif
		}

		private IEnumerator SwitchMode(GameMode mode) {
			yield return new WaitUntil(() => !_isSwitching);
			if (_current_mode == mode) yield break;

			_isSwitching = true;
			// Switching code here

			// backdrop

			if (_current_mode != null) yield return _current_mode.OnStart();
			_current_mode = mode;
			yield return _current_mode.OnStart();

			// backdrop release

			_isSwitching = false;
		}

	}
}