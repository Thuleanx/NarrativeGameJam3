using UnityEngine;
using UnityEngine.Events;

namespace Thuleanx.Interaction {
	public class TriggerOnContact : MonoBehaviour {
		[SerializeField]
		UnityEvent OnContact;

		private void OnTriggerEnter2D(Collider2D other) {
			if (other.tag == "Player")
				OnContact?.Invoke();
		}
	}
}