using UnityEngine;
using Thuleanx.Optimization;
using Thuleanx.Math;
using System;

namespace Thuleanx.Mechanics.Danmaku {
	public class BulletPatternSpawnerNeon : MonoBehaviour {
		Timer bulletCD;

		public BubblePool BulletPool;
		public BulletPatternNeon Pattern;
		public BulletArray Array;

		public Color GizmoColor = Color.red;

		public bool Active {get; private set; }

		public float CurrentDegree {get; private set; }
		public float SpinRate {get; private set; }
		public bool SpinInverted {get; private set; }

		float startTime;

		[Tooltip("Leave this to true")]
		public bool ShootAll = true;
		public int seed = 12345;
		System.Random rand;

		void Awake() {
			rand = new System.Random(seed);
		}

		void OnEnable() => Active = false;
		public void Activate() {
			Active = true;
			SpinInverted = false;
			CurrentDegree = 0f;
			startTime = Time.time;
			if (Pattern.Spin) SpinRate = Pattern.SpinStartVelocity;
		}
		public void Disable() {
			Active = false;
			bulletCD.Stop();
		} 
		public void ResetSeed() => rand = new System.Random(seed);
		void Update() {
			if (Active && Pattern.BulletsPerMinute != 0) {
				if (Pattern.Spin) {
					if (Pattern.LimitStyle == BulletPatternNeon.SpinLimitStyle.Velocity) {
						float modifier = (SpinInverted?-1:1);
						float nxtSpinRate = Calc.Approach(SpinRate, Pattern.SpinRateMax*modifier, 
							Pattern.SpinVelocityAccel*Time.deltaTime);
						if (Calc.Approximately(Pattern.SpinRateMax*modifier, nxtSpinRate) && Pattern.InvertSpin)
							SpinInverted ^= true;
						SpinRate = nxtSpinRate;

						CurrentDegree += SpinRate*Time.deltaTime;
					} else if (Pattern.LimitStyle == BulletPatternNeon.SpinLimitStyle.Area) {
						CurrentDegree = Mathf.Sin( 2*Mathf.PI / Pattern.Period * (Time.time - startTime)) * Pattern.SpinArcMax;
					} else if (Pattern.LimitStyle == BulletPatternNeon.SpinLimitStyle.None) {
						CurrentDegree += SpinRate*Time.deltaTime;
					}
				}

				if (!bulletCD) {
					Burst();
					bulletCD = new Timer(Pattern.BulletsPerMinute == 0 ? 1f : 60f/Pattern.BulletsPerMinute);
					bulletCD.Start();
				}
			}
		}
		public void Burst() {
			if (!Active) CurrentDegree = 0f;
			int chosen = ShootAll ? 0 : rand.Next()%transform.childCount;
			foreach (Transform trans in transform) if (ShootAll || chosen-- == 0) {
				float additionalSpin = trans.rotation.eulerAngles.z;
				Pattern.Spawn(BulletPool, Array, trans.position, additionalSpin + CurrentDegree);
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