using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Thuleanx.Math;
using Thuleanx.Interaction;

namespace Thuleanx.Controls {

	[DisallowMultipleComponent]
	public class InputController : MonoBehaviour {
		public static InputController Instance;

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
		[HideInInspector]
		public bool Aiming;

		public Timer MouseClick;

		void Awake() {
			Instance = this;
		}

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

		public void StartInteracting() {
			Interacting = true;
		}

		public void StopInteracting() {
			Interacting = false;
		}

		public void OnMouseClick(InputAction.CallbackContext context) {
			if (Active) {
				MouseClick = new Timer(InputBufferTime);
				MouseClick.Start();
			}
		}

		public void OnAttackInput(InputAction.CallbackContext context) {
			if (Active) {
				Attack = new Timer(InputBufferTime);
				Attack.Start();
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

		public void UseDashInput() => Dash.Stop();
		public void UseAttackInput() => Attack.Stop();
		public void UseMouseInput() => MouseClick.Stop();
	}
}
