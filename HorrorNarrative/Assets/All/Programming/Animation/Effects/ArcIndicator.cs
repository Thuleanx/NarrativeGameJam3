using UnityEngine;
using Thuleanx.AI;

namespace Thuleanx.Animation {
	public class ArcIndicator : MonoBehaviour {
		[Tooltip("Lines for indicating the shooting arc")]
		public LineRenderer BottomLine, TopLine;
		public float IndicatorRange = 6f;

		public Player player {get; private set; }

		void Awake() { player = GetComponentInParent<Player>(); }

		void Update() {
			// if aiming
			if (player.IsAiming()) {
				float arc = player.AimingArc();
				Vector2 top = new Vector2(Mathf.Cos(arc/2 * Mathf.Deg2Rad), Mathf.Sin(arc/2 * Mathf.Deg2Rad)) * IndicatorRange;
				Vector2 bot = new Vector2(Mathf.Cos(arc/2 * Mathf.Deg2Rad), -Mathf.Sin(arc/2 * Mathf.Deg2Rad)) * IndicatorRange;

				TopLine.SetPositions(new Vector3[]{Vector2.zero, top});
				BottomLine.SetPositions(new Vector3[]{Vector2.zero, bot});
			} else {
				TopLine.SetPositions(new Vector3[]{Vector2.zero, Vector2.zero});
				BottomLine.SetPositions(new Vector3[]{Vector2.zero, Vector2.zero});
			}
		}
	}
}