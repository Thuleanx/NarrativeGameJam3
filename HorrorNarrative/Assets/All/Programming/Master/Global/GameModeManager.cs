using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Thuleanx.Mechanics.Mapping;
using Thuleanx.Optimization;

namespace Thuleanx.Master.Global {
	public class GameModeManager : MonoBehaviour {
		bool _isSwitching = false;
		[HideInInspector]
		public GameMode _current_mode;

		StoryMode _storyMode = new StoryMode();
		MainMenuMode _mainMenuMode = new MainMenuMode();
		public SceneHandler InitialScene;

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

		public void Boot() {
			SceneManager.LoadScene(InitialScene.SceneReference.SceneName);
			App.Instance.StartCoroutine(SwitchMode(_storyMode));
		}

		private IEnumerator SwitchMode(GameMode mode) {
			yield return new WaitUntil(() => !_isSwitching);
			if (_current_mode == mode) yield break;

			_isSwitching = true;

			if (_current_mode != null) yield return _current_mode.OnEnd();
			_current_mode = mode;
			yield return _current_mode.OnStart();

			_isSwitching = false;
		}

	}
}