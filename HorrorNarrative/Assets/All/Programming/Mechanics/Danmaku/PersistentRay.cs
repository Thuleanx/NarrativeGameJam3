using UnityEngine;

namespace Thuleanx.Mechanics.Danmaku {
	public class PersistentRay : MonoBehaviour {
		public Collider2D Collider {get; private set; }

		void Awake() {
			Collider = GetComponent<Collider2D>();
		}
	}
}