using UnityEngine;
using Thuleanx.AI;

namespace Thuleanx.Animation {
	public class SignalAnimationFinish : MonoBehaviour {
		Agent agent;
		private void Awake() {
			agent = GetComponent<Agent>();
		}

		public void SignalFinish() => ((PlayerState) agent.Machine.Value.Current).AnimationFinish = true;
	}
}