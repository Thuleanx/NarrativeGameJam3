using UnityEngine;
using UnityEngine.Events;
using Thuleanx.Controls;

namespace Thuleanx.Interaction {
	[RequireComponent(typeof(Collider2D))]
	public class Interactible : MonoBehaviour {

		[HideInInspector]
		public bool _selected;
		
		[SerializeField] 
		SpriteRenderer[] Sprites;

		Material[] DefaultMaterial;
		[SerializeField] Material HighlightMaterial;

		[SerializeField] UnityEvent OnSelectStart;
		[SerializeField] UnityEvent OnSelectStop;
		[SerializeField] UnityEvent OnInteractionStart;
		[SerializeField] UnityEvent OnInteractionStop;

		private void Awake() {
			DefaultMaterial = new Material[Sprites.Length];
			for (int i = 0; i < Sprites.Length; i++) DefaultMaterial[i] = Sprites[i].material;
		}

		public void SelectedStart() {
			_selected = true;
			OnSelectStart?.Invoke();

			for (int i = 0; i < Sprites.Length; i++) DefaultMaterial[i] = Sprites[i].material;
			for (int i = 0; i < Sprites.Length; i++)
				Sprites[i].material = HighlightMaterial;

		}

		public void SelectedStop() {
			OnSelectStop?.Invoke();
			_selected = false;

			for (int i = 0; i < Sprites.Length; i++)
				Sprites[i].material = DefaultMaterial[i];

		}

		public void StartInteraction() {
			SelectedStop();
			Controls.InputController.Instance.StartInteracting();
			OnInteractionStart?.Invoke();
		}

		public void StopInteraction() {
			OnInteractionStop?.Invoke();
			Controls.InputController.Instance.StopInteracting();
		}

		public static Interactible GetInteractible(Vector2 worldPosition, LayerMask interactibleMask) {
			RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, 0f, interactibleMask);
			if (hit) return hit.collider.gameObject.GetComponent<Interactible>();
			return null;
		}

		void Update() {
			if (_selected && Controls.InputController.Instance.MouseClick)
				StartInteraction();
		}
	}
}