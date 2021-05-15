using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thuleanx.AI;
using Thuleanx.Controls;
using Thuleanx.Math;
using UnityEngine.Events;

namespace Thuleanx.UI {
	public class Fader : MonoBehaviour {
		[SerializeField] UnityEvent BlockFinish, UnblockFinish, BlockStart, UnblockStart;

		public Animator Anim {get; private set; }
		[SerializeField] string BlockTrigger = "Block";
		[SerializeField] string UnBlockTrigger = "UnBlock";

		private void Awake() {
			Anim = GetComponent<Animator>();	
		}

		public IEnumerator StartBlock() {
			while (Anim == null) yield return null;
			Anim?.SetTrigger(BlockTrigger);
		}
		public IEnumerator StartUnblock() {
			while (Anim == null) yield return null;
			Anim?.SetTrigger(UnBlockTrigger);
		}

		public void OnBlockFinish() {
			BlockFinish?.Invoke();
		}
		public void OnUnblockFinish() {
			UnblockFinish?.Invoke();
		}
		public void OnBlockStart() {
			BlockStart?.Invoke();
		}
		public void OnUnblockStart() {
			UnblockStart?.Invoke();
		}

	}
}