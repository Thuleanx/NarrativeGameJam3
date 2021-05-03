using UnityEngine;
using System.Collections;

namespace Thuleanx.Master.Global {
	public class StoryMode : GameMode {

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

		}
	}
}