using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Thuleanx.Mechanics.Mapping;
using Thuleanx.Optimization;
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
				
				App.Instance._AudioManager.SetMainTrack(
					App.Instance._AudioManager.GetTrack(_activeSceneHandler.Ambiance));
				App.Instance._AudioManager.MainTrack.Play();

				Debug.Log("At scene: " + _activeSceneName + " " + (_activeSceneHandler));
				App.Instance.StartCoroutine(App.Instance._TransitionManager.BackdropRelease());

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
			yield return App.Instance._TransitionManager.BackdropBlock();

			Player player = GameObject.FindObjectOfType<Player>();
			PlayerLocalContext ctx = GameObject.FindObjectOfType<Player>().LocalContext as PlayerLocalContext;
			// PlayerState state = player.Machine.Value.Current as PlayerState;


			if (passage.traversal_sound != null && passage.traversal_sound.Length>0)
				App.Instance._AudioManager.PlayOneShot(passage.traversal_sound);
			if (passage.target_scene.SceneReference.SceneName != SceneManager.GetActiveScene().name) {
				// tells all bubble pools to collect
				foreach (var pool in App.Instance.activePools) 
					pool.CollectsAll(SceneManager.GetActiveScene());


				// load next level
				yield return SceneManager.LoadSceneAsync(passage.target_scene.SceneReference.SceneName, LoadSceneMode.Single);

				if (passage.target_scene.Ambiance != null && passage.target_scene.Ambiance.Length > 0
					&& (App.Instance._AudioManager.MainTrack == null || 
						passage.target_scene.Ambiance != App.Instance._AudioManager.MainTrack.reference)) {
					App.Instance._AudioManager.MainTrack.Stop();
					App.Instance._AudioManager.SetMainTrack(
						App.Instance._AudioManager.GetTrack(passage.target_scene.Ambiance));
					App.Instance._AudioManager.MainTrack.Play();
				}
			}

			// position player + fill in data
			GameObject playerObj = GameObject.FindWithTag("Player");
			player = playerObj.GetComponent<Player>();

			// find an anchor
			Anchor targetAnchor = GameObject.FindObjectOfType<Anchor>();
			foreach (Anchor potentialTarget in GameObject.FindObjectsOfType<Anchor>()) {
				if (potentialTarget.passage == passage.target_passage)
					targetAnchor = potentialTarget;
			}
			if (targetAnchor.passage != passage.target_passage)
				Debug.Log("Anchor not found.");

			playerObj.transform.position = (Vector2) targetAnchor.transform.position;
			ctx.Position = playerObj.transform.position;

			player.LocalContext = ctx;

			// endbackdrop
			yield return App.Instance._TransitionManager.BackdropRelease();

			Resume();
		}
	}
}