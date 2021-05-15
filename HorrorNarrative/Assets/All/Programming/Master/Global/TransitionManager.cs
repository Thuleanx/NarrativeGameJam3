using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Thuleanx.UI;

namespace Thuleanx.Master.Global {
	enum BackdropState {
		_Loading,
		_Blocking,
		_Blocked,
		_Releasing,
		_Released
	}

	public class TransitionManager : MonoBehaviour {
		BackdropState _backdropState = BackdropState._Loading;
		[SerializeField] Fader Fader;
		bool BackdropComplete = false;

		public IEnumerator BackdropBlock() {
			if (_backdropState != BackdropState._Loading && _backdropState != BackdropState._Released)
				yield break;
			_backdropState = BackdropState._Blocking;
			BackdropComplete = false;
			yield return Fader.StartBlock();
			while (!BackdropComplete) yield return null;
			_backdropState = BackdropState._Blocked;
		}
		public IEnumerator BackdropRelease() {
			if (_backdropState != BackdropState._Loading && _backdropState != BackdropState._Blocked)
				yield break;
			_backdropState = BackdropState._Releasing;
			BackdropComplete = false;
			yield return Fader.StartUnblock();
			while (!BackdropComplete) yield return null;
			_backdropState = BackdropState._Released;
		}
		public void MarkBackdropComplete() => BackdropComplete = true;

	}
}