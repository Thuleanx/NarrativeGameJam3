using UnityEngine;
using Thuleanx.Optimization;

namespace Thuleanx.Mechanics.Danmaku {
	[CreateAssetMenu(fileName = "BulletPattern", menuName = "~/Danmaku/BulletPattern", order = 0)]
	public class BulletPattern : ScriptableObject {
		public BulletArray array;
		public BubblePool Pool;

		[Header("Array Description")]
		public int TotalBulletArrays = 1;
		public float DegreeSpreadBetweenArrays= 30f;
		public bool LimitSpin = false;
		public float RotationClamp = 10f;

		public bool Spin;
		public float StartingSpinRate = 0f;
		public float SpinRateMax = 0f;
		public float SpinRateAccel = 0f;
		public bool InvertSpin = false;

		public float StartingDegree = 0f;

		[Header("Bullet Description")]
		public float BulletSpeed = 1f;
		public float BulletsPerMinute = 1f;

		public bool Offset = false;
		public float ObjectRadius;
		public Vector2 OffsetCenter;


		public void Spawn(BubblePool bulletPool, Vector2 position, float currentDegree) {
			for (int i = 0; i < TotalBulletArrays; i++) {
				float deg = currentDegree + i*DegreeSpreadBetweenArrays;

				for (int j = 0; j < array.BulletPerArray; j++) {
					float resdeg = deg + j*array.SpreadDegreeWithinArray;

					Vector2 offset = Vector2.zero;
					Vector2 radialDir= new Vector2(Mathf.Cos(resdeg * Mathf.Deg2Rad), Mathf.Sin(resdeg * Mathf.Deg2Rad));

					if (Offset) {
						if (ObjectRadius != 0) offset = radialDir * ObjectRadius;
						offset += OffsetCenter;
					}

					GameObject bulletObj = bulletPool.Borrow(position+offset, Quaternion.identity);
					Bullet bullet = bulletObj.GetComponent<Bullet>();
					bullet.velocity = radialDir*BulletSpeed;
				}
			}
		}
	}
}