using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Thuleanx.Math;
using Thuleanx.Interaction;

namespace Thuleanx.Controls {

	[DisallowMultipleComponent]
	public class InputManager : MonoBehaviour {
		[Tooltip("How long are the inputs buffered, in seconds.")]
		public float InputBufferTime = .2f;
		[Tooltip("Layer Mask to find interactible objects")]
		public LayerMask InteractibleMask;

		Interactible Target;
		bool Interacting;

		public bool Active;

		[HideInInspector]
		public Vector2 Movement;
		[HideInInspector]
		public Vector2 LastNonZeroMovement = Vector2.right;
		[HideInInspector]
		public Vector2 MouseScreenPos = Vector2.zero;
		public Vector2 MouseWorldPos {
			get {
				if (Camera.main != null)
					return Camera.main.ScreenToWorldPoint(MouseScreenPos);
				return Vector2.zero;
			}
		}
		
		public Timer Attack;
		public Timer Dash;
		public Timer Reload;
		public Timer SkipDialogue;
		[HideInInspector]
		public bool Aiming;

		public Timer MouseClick;

		void Update() {
			if (!Interacting) {
				Interactible NxtTarget = Interactible.GetInteractible(MouseWorldPos, InteractibleMask);
				if (NxtTarget != Target) {
					Target?.SelectedStop();
					NxtTarget?.SelectedStart();
					Target = NxtTarget;
				}
			}
		}

		// Should only be called by Interactible
		public void StartInteracting() {
			Interacting = true;
		}

		// Should only be called by Interactible
		public void StopInteracting() {
			Interacting = false;
			Target = null;
		}

		public void SignalStopInteract() => Target?.StopInteraction();

		public void OnMouseClick(InputAction.CallbackContext context) {
			if (Active) {
				if (context.started) {
					MouseClick = new Timer(InputBufferTime);
					MouseClick.Start();
				}
			}
		}

		public void OnAttackInput(InputAction.CallbackContext context) {
			if (Active) {
				if (context.started) {
					Attack = new Timer(InputBufferTime);
					Attack.Start();
				}
			}
		}

		public void OnReloadInput(InputAction.CallbackContext context) {
			if (Active) {
				if (context.started) {
					Reload = new Timer(InputBufferTime);
					Reload.Start();
				}
			}
		}


		public void OnMousePosInput(InputAction.CallbackContext context)
		{
			if (Active) {
				MouseScreenPos = context.ReadValue<Vector2>();
			}
		}
		public void OnMoveInput(InputAction.CallbackContext context)
		{
			if (Active) {
				Movement = context.ReadValue<Vector2>();
				if (Movement != Vector2.zero) LastNonZeroMovement = Movement;
			}
		}
		public void OnDashInput(InputAction.CallbackContext context) {
			if (Active && context.started) {
				Dash = new Timer(InputBufferTime);
				Dash.Start();
			}
		}
		public void OnAimInput(InputAction.CallbackContext context) {
			if (Active) {
				if (context.started) Aiming = true;
				if (context.canceled) Aiming = false;
			}
		}

		public void OnSkipDialogue(InputAction.CallbackContext context) {
			if (Active && context.started) {
				SkipDialogue = new Timer(InputBufferTime);
				SkipDialogue.Start();
			}
		}

		public void UseDashInput() => Dash.Stop();
		public void UseAttackInput() => Attack.Stop();
		public void UseReloadInput() => Reload.Stop();
		public void UseMouseInput() => MouseClick.Stop();
		public void UseSkipDialogueInput() => SkipDialogue.Stop();

	}
}
