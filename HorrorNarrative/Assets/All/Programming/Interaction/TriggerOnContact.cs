using UnityEngine;
using UnityEngine.Events;
using Thuleanx.AI;

namespace Thuleanx.Interaction {
	public class TriggerOnContact : MonoBehaviour {
		[SerializeField]
		UnityEvent OnContact;

		private void OnTriggerEnter2D(Collider2D other) {
			if (other.tag == "Player" && !App.LocalInstance._ContextManager.Player.IsDead())
				OnContact?.Invoke();
		}
	}
}