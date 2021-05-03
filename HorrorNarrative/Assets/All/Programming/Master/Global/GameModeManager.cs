using UnityEngine;
using System.Collections;

namespace Thuleanx.Master.Global {
	public class GameModeManager : MonoBehaviour {
		bool _isSwitching = false;
		GameMode _current_mode;

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