using UnityEngine;
using Thuleanx.Utility;
using Thuleanx.Math;

namespace Thuleanx.Animation.Effect {
	public class FlashEffect : MonoBehaviour {
		[SerializeField] Material FlashMaterial;
		[SerializeField] float flashDuration;
		[SerializeField] Optional<SpriteRenderer> Sprite;

		Material DefaultMaterial;
		Timer Flashing;
		

		void Awake() {
			if (!Sprite.Enabled)
				Sprite = new Optional<SpriteRenderer>(GetComponent<SpriteRenderer>());
			Flashing = new Timer(flashDuration);
			DefaultMaterial = Sprite.Value.material;
		}

		public void Flash() {
			Flashing.Start();
		}

		void Update() {
			if (Flashing) {
				Sprite.Value.material = FlashMaterial;
			} else {
				Sprite.Value.material = DefaultMaterial;
			}
		}
	}
}