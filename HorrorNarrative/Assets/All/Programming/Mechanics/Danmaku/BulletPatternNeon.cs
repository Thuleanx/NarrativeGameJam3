using UnityEngine;
using Thuleanx.Optimization;

namespace Thuleanx.Mechanics.Danmaku {
	[CreateAssetMenu(fileName = "BulletPatternNeon", menuName = "~/Danmaku/BulletPatternNeon", order = 0)]
	public class BulletPatternNeon : ScriptableObject {
		public int TotalBulletArrays = 1;
		public float ArrayDegreeSpread = 30f;
		public bool AlignWithVelocity = false;

		[System.Serializable]
		public enum SpinLimitStyle {
			None,
			Velocity,
			Area
		}

		public bool Spin;
		public float SpinStartVelocity = 0f;
		public SpinLimitStyle LimitStyle = SpinLimitStyle.None;

		// Limit Velocity
		public float SpinVelocityAccel = 0f;
		public float SpinRateMax = 0f;
		public bool InvertSpin = false;

		// Limit By Area
		public float SpinArcMax = 0f;
		public float Period = 1f;

		public float BulletSpeed = 1f;
		public float BulletsPerMinute = 80f;

		public float ObjectRadius = 0f;

		public void Spawn(BubblePool magazine, BulletArray array, Vector2 position, float offsetDegree) {
			for (int i = 0; i < TotalBulletArrays; i++) {
				float deg = offsetDegree + i * ArrayDegreeSpread - (TotalBulletArrays-1)/2f*ArrayDegreeSpread;

				for (int j = 0; j < array.BulletPerArray; j++) {
					float resdeg = deg + j*array.SpreadDegreeWithinArray - ((array.BulletPerArray-1)*array.SpreadDegreeWithinArray/2f);

					Vector2 offset = Vector2.zero;
					Vector2 radialDir= new Vector2(Mathf.Cos(resdeg * Mathf.Deg2Rad), Mathf.Sin(resdeg * Mathf.Deg2Rad));

					if (ObjectRadius != 0) offset = radialDir * ObjectRadius;

					GameObject bulletObj = magazine.Borrow(position+offset, Quaternion.identity);
					Bullet bullet = bulletObj.GetComponent<Bullet>();
					if (bullet != null) {
						bullet.velocity = radialDir*BulletSpeed;
						if (AlignWithVelocity) bullet.transform.rotation = Quaternion.Euler(0f, 0f, resdeg);
					}
				}
			}
		}
	}
}