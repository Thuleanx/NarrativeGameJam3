using UnityEngine;

namespace Thuleanx.Animation {
	public class ParticleTrigger : MonoBehaviour {
		[SerializeField] ParticleCombo Combo;

		public void Activate() {
			Combo.Activate();
		}
	}
}