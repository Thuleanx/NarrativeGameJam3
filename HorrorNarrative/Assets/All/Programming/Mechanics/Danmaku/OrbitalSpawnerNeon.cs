using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using Thuleanx.Optimization;

namespace Thuleanx.Mechanics.Danmaku {
	public class OrbitalSpawnerNeon : MonoBehaviour {
		public BubblePool OrbitoidPool;
		public BulletArray Array;
		public OrbitalPatternNeon Pattern;

		public Color GizmoColor = Color.red;

		public bool Active {get; private set; }
		public UnityEvent OnActivate;
		public UnityEvent OnStop;

		List<GameObject> orbitoids = new List<GameObject>();

		public void Activate() {
			if (Active) Stop();

			Active = true;
			orbitoids = new List<GameObject>();
			foreach (Transform trans in transform) {
				float additionalSpin = trans.rotation.eulerAngles.z;
				List<GameObject> sub = Pattern.Spawn(OrbitoidPool, Array, trans.position, additionalSpin);
				foreach (GameObject obj in sub) {
					obj.transform.SetParent(trans);
					orbitoids.Add(obj);
				}
			}
			OnActivate?.Invoke();
		}

		public void Stop() {
			if (Active) {
				foreach (GameObject obj in orbitoids)
					obj.SetActive(false);
				OnStop?.Invoke();
				orbitoids.Clear();
				Active = false;
			}
		}

		void OnDrawGizmosSelected() {
			foreach (Transform trans in transform) {
				Thuleanx.Utility.DrawArrow.ForGizmo(trans.position, new Vector2(
					Mathf.Cos(trans.eulerAngles.z * Mathf.Deg2Rad),
					Mathf.Sin(trans.eulerAngles.z * Mathf.Deg2Rad)
				), GizmoColor);
				Gizmos.color = GizmoColor;
				Gizmos.DrawWireSphere(trans.position, .25f);
			}
		}
	}
}
