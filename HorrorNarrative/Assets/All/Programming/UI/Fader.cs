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
		[SerializeField] string BlockParam = "Block";

		bool blocked;
		public bool Blocked {
			get => blocked;
			set {
				if (value != blocked) {
					blocked = value;
					Anim?.SetBool(BlockParam, blocked);
				}
			}
		}

		private void Awake() {
			Anim = GetComponent<Animator>();	
		}

		private void OnEnable() {
			blocked = false;
			Anim?.SetBool(BlockParam, blocked);
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