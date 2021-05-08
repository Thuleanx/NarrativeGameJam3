using UnityEngine;
using System.Collections.Generic;
using System.Collections;

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

		public bool BackdropComplete = false;

		public IEnumerator BackdropBlock() {
			if (_backdropState != BackdropState._Loading && _backdropState != BackdropState._Released)
				yield break;
			_backdropState = BackdropState._Blocking;
			BackdropComplete = false;
			while (!BackdropComplete) yield return null;
			_backdropState = BackdropState._Blocked;
		}
		public IEnumerator BackdropRelease() {
			if (_backdropState != BackdropState._Loading && _backdropState != BackdropState._Blocked)
				yield break;
			_backdropState = BackdropState._Releasing;
			BackdropComplete = false;
			while (!BackdropComplete) yield return null;
			_backdropState = BackdropState._Released;
		}
		public void MarkBackdropComplete() => BackdropComplete = true;

	}
}