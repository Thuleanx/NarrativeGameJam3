using UnityEngine;

namespace Thuleanx.Behaviour {
	public class DisableOnStart : MonoBehaviour {
		void Start() {
			gameObject.SetActive(false);
		}
	}
}