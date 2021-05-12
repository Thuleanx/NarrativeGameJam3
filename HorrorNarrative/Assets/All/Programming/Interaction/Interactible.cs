using UnityEngine;
using UnityEngine.Events;
using Thuleanx.Controls;
using Thuleanx.AI;

namespace Thuleanx.Interaction {
	public class Interactible : MonoBehaviour {

		[HideInInspector]
		public bool _selected;

		[Tooltip("Will the player be halted / frozen upon interacting")]
		public bool ForceInteraction;

		[SerializeField] UnityEvent OnSelectStart;
		[SerializeField] UnityEvent OnSelectStop;
		[SerializeField] UnityEvent OnInteractionStart;
		[SerializeField] UnityEvent OnInteractionStop;

		public virtual void Awake() {}

		public virtual void SelectedStart() {
			_selected = true;
			OnSelectStart?.Invoke();
		}

		public virtual void SelectedStop() {
			OnSelectStop?.Invoke();
			_selected = false;
		}

		public virtual void StartInteraction() {
			SelectedStop();
			App.Instance._InputManager.StartInteracting(this);
			OnInteractionStart?.Invoke();
			if (ForceInteraction) Player.Instance.Halt();
		}

		public virtual void StopInteraction() {
			OnInteractionStop?.Invoke();
			App.Instance._InputManager.StopInteracting();
			if (ForceInteraction) Player.Instance.UnHalt();
		}

		public static Interactible GetInteractible(Vector2 worldPosition, LayerMask interactibleMask) {
			RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, 0f, interactibleMask);
			if (hit) return hit.collider.gameObject.GetComponent<Interactible>();
			return null;
		}

		void Update() {
			if (_selected && App.Instance._InputManager.MouseClick)
				StartInteraction();
		}
	}
}