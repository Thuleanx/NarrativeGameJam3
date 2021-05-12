using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Thuleanx.Mechanics.Mapping;
using Thuleanx.Utility;
using Thuleanx.AI;
using Thuleanx.AI.Context;

namespace Thuleanx.Master.Global {
	public class StoryMode : GameMode {

		public override void OnEditorStart() {
			#if UNITY_EDITOR
				// Load Save
				string _activeSceneName = SceneManager.GetActiveScene().name;
				foreach (SceneHandler handler in General.GetAllInstances<SceneHandler>())
					if (handler.SceneReference.SceneName == _activeSceneName)
						_activeSceneHandler = handler;

				Debug.Log("At scene: " + _activeSceneName + " " + (_activeSceneHandler));

				_state = GameModeState.Started;
				Resume();
			#endif
		}
		public override IEnumerator OnStart() {
			if (_state != GameModeState.Ended && _state != GameModeState.Loading)
				yield break;
			Pause();
			_state = GameModeState.Starting;
			_state = GameModeState.Started;
			Resume();
		}
		public override IEnumerator OnEnd() {
			if (_state != GameModeState.Started) yield break;
			Pause();
			_state = GameModeState.Ending;
			_state = GameModeState.Ended;
			Resume();
		}

		public override IEnumerator TransitionThroughPassage(Passage passage) {
			Pause();
			// backdrop

			Player player = GameObject.FindObjectOfType<Player>();
			PlayerLocalContext ctx = GameObject.FindObjectOfType<Player>().LocalContext as PlayerLocalContext;
			PlayerState state = player.Machine.Value.Current as PlayerState;

			if (passage.target_scene.SceneReference.SceneName != SceneManager.GetActiveScene().name) {
				// load next level
				yield return SceneManager.LoadSceneAsync(passage.target_scene.SceneReference.SceneName, LoadSceneMode.Single);
			}

			// position player + fill in data
			GameObject playerObj = GameObject.FindWithTag("Player");
			player = playerObj.GetComponent<Player>();

			// find an anchor
			Anchor targetAnchor = GameObject.FindObjectOfType<Anchor>();
			foreach (Anchor potentialTarget in GameObject.FindObjectsOfType<Anchor>())
				if (potentialTarget.passage == passage.target_passage)
					targetAnchor = potentialTarget;

			playerObj.transform.position = (Vector2) targetAnchor.transform.position;
			ctx.Position = playerObj.transform.position;

			player.ForcePlayerState(state);
			player.LocalContext = ctx;

			// endbackdrop

			Resume();
		}
	}
}