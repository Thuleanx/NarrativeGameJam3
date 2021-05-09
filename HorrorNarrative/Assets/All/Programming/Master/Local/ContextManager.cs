using UnityEngine;
using Thuleanx.AI;
using Thuleanx.Utility;

namespace Thuleanx.Master.Local {
	public class ContextManager : MonoBehaviour {
		public Player Player;

		void Awake() {
			if (Player == null) Player = FindObjectOfType<Player>();
		}
	}
}