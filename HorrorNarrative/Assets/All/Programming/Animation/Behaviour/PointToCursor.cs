using UnityEngine;
using Thuleanx.Controls;

namespace Thuleanx.Animation {
	public class PointToCursor : MonoBehaviour {
		void Update() {
			Vector2 displacement = InputController.Instance.MouseWorldPos - (Vector2) transform.position;
			float angle = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;
			transform.localRotation = Quaternion.Euler(0f, 0f, angle);
		}
	}
}