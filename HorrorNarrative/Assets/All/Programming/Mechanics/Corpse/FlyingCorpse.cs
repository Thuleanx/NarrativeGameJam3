using UnityEngine;
using UnityEngine.Events;
using Thuleanx.Math;

namespace Thuleanx.Mechanics.Corpse {
	public class FlyingCorpse : Corpse {
		[SerializeField] float Duration = 1f;
		[SerializeField] float FlyingHeight = 2f;
		[SerializeField] float Speed = 3f;
		[SerializeField] GameObject Body;

		[SerializeField] UnityEvent OnFinish;
		[SerializeField] UnityEvent OnStart;

		Timer Animating;

		public void OnEnable() {
			Animating = new Timer(Duration);
			Animating.Start();
			OnStart?.Invoke();
		}

		void Update() {

			if (Animating) {
				// height of body
				Vector3 BodyPos = Body.transform.localPosition;
				BodyPos.y = Mathf.Sin(Calc.Remap(Animating.TimeLeft, 0, Duration, Mathf.PI, 0)) * FlyingHeight;
				Body.transform.localPosition = BodyPos;
				// position of the corpse
				RigidBody.velocity = Dir * Speed;
			} else {
				Vector3 BodyPos = Body.transform.localPosition;
				BodyPos.y = 0;
				Body.transform.localPosition = BodyPos;
				RigidBody.velocity = Vector2.zero;
				OnFinish?.Invoke();
			}
		}
	}
}