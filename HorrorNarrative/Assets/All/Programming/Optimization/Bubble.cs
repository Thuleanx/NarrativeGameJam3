using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using Thuleanx.Master;
using Thuleanx.Utility;

namespace Thuleanx.Optimization {
	public class Bubble : MonoBehaviour {
		[HideInInspector]
		public BubblePool Pool;
		[HideInInspector]
		public bool inPool = false;

		void OnDisable() {
			Game.Instance?.StartCoroutine(General._InvokeNextFrame(() => {
				if (!inPool) Pool?.Collects(this);
			}));
		}
	}
}