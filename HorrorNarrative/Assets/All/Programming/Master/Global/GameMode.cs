using UnityEngine;
using System.Collections;
using Thuleanx.Mechanics.Mapping;

namespace Thuleanx.Master.Global {
	public class GameMode {
		public GameModeState _state = GameModeState.Loading;
		public SceneHandler _activeSceneHandler;
		
		public virtual void OnEditorStart() { }
		public virtual IEnumerator OnStart() { yield return null; }
		public virtual IEnumerator OnEnd() { yield return null; }

		public virtual void Pause() => Time.timeScale = 0f;
		public virtual void Resume() {
			Time.timeScale = 1f;
			Debug.Log("RESUMED");
		}

		public virtual IEnumerator TransitionThroughPassage(Passage passage) { yield return null; }
	}
}