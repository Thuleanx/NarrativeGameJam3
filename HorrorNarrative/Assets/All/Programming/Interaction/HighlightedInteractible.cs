using UnityEngine;

namespace Thuleanx.Interaction {
	public class HighlightedInteractible : Interactible {
		[Header("Hightlight")]
		[SerializeField] 
		SpriteRenderer[] Sprites;

		Material[] DefaultMaterial;
		[SerializeField] Material HighlightMaterial;

		public override void Awake() {
			base.Awake();
			DefaultMaterial = new Material[Sprites.Length];
			for (int i = 0; i < Sprites.Length; i++) DefaultMaterial[i] = Sprites[i].material;
		}

		public override void SelectedStart() {
			base.SelectedStart();
			for (int i = 0; i < Sprites.Length; i++) DefaultMaterial[i] = Sprites[i].material;
			for (int i = 0; i < Sprites.Length; i++)
				Sprites[i].material = HighlightMaterial;
		}

		public override void SelectedStop() {
			base.SelectedStop();
			for (int i = 0; i < Sprites.Length; i++)
				Sprites[i].material = DefaultMaterial[i];
		}
	}
}