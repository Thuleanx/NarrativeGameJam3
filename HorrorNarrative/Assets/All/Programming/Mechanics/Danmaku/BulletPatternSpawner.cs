using UnityEngine;
using Thuleanx.Math;
using Thuleanx.Optimization;

namespace Thuleanx.Mechanics.Danmaku {
	public class BulletPatternSpawner : MonoBehaviour {
		Timer BulletCD;
		public BubblePool BulletPool;
		public BulletPattern Pattern;
		public bool Active {get; private set; }

		public float CurrentDegree {get; private set; }
		public float SpinRate {get; private set; }
		public bool SpinInverted {get; private set; }

		void OnEnable() {
			Active = false;
		}

		public void Activate() {
			Active = true;
			SpinInverted = false;
			CurrentDegree = Pattern.StartingDegree;
			if (Pattern.Spin) SpinRate = Pattern.StartingSpinRate;
		}
		public void Disable() {
			Active = false;
			BulletCD.Stop();
		} 

		void Update() {
			if (Active && Pattern.BulletsPerMinute != 0) {
				// Actual code
				if (Pattern.Spin) {
					if (Pattern.SpinRateAccel != 0) {
						if (Pattern.LimitSpin) {
							float modifier = (SpinInverted?-1:1);
							float nxtSpinRate = Calc.Approach(SpinRate, Pattern.SpinRateMax*modifier, 
								Pattern.SpinRateAccel*Time.deltaTime);

							if (Calc.Approximately(Pattern.SpinRateMax*modifier, nxtSpinRate) && Pattern.InvertSpin) {
								SpinInverted ^= true;
							}
							SpinRate = nxtSpinRate;
						} else {
							SpinRate += Pattern.SpinRateAccel * Time.deltaTime;
						}
					}
					CurrentDegree += SpinRate*Time.deltaTime;
				}
				
				if (!BulletCD) {
					Burst();
					BulletCD = new Timer(Pattern.BulletsPerMinute == 0 ? 1f : 60f/Pattern.BulletsPerMinute);
					BulletCD.Start();
				}
			}
		}

		public void Burst() {
			if (!Active) CurrentDegree = Pattern.StartingDegree;
			float additionalSpin = transform.rotation.eulerAngles.z;
			Pattern.Spawn(BulletPool, transform.position, CurrentDegree + additionalSpin);
		}
	}
}