using UnityEngine;
using System.Collections;

namespace Thuleanx.Master.Global {
	public class GameMode {
		public GameModeState _state = GameModeState.Loading;
		
		public virtual IEnumerator OnStart() { yield return null; }
		public virtual IEnumerator OnEnd() { yield return null; }

		public virtual void Pause() {}
		public virtual void Resume() {}
	}
}