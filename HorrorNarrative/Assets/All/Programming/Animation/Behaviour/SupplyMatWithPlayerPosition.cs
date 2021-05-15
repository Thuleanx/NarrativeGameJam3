using UnityEngine;

namespace Thuleanx.Animation {
	[RequireComponent(typeof(SpriteRenderer))]
	public class SupplyMatWithPlayerPosition : MonoBehaviour {
		[SerializeField] string varName;
		SpriteRenderer Sprite;

		private void Awake() {
			Sprite = GetComponent<SpriteRenderer>();
		}

		void Update() {
			if (varName!=null && varName.Length>0) {
				Sprite.material.SetVector(varName, App.LocalInstance._ContextManager.Player.transform.position);
			}
		}
	}
}