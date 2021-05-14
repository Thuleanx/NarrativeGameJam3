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

		public void StartBlock() => Anim?.SetTrigger(BlockTrigger);
		public void StartUnblock() => Anim?.SetTrigger(UnBlockTrigger);

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