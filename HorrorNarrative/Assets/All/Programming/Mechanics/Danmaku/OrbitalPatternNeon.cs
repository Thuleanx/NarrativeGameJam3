using UnityEngine;
using Thuleanx.Optimization;
using System.Collections.Generic;

namespace Thuleanx.Mechanics.Danmaku {
	[CreateAssetMenu(fileName = "OrbitalPatternNeon", menuName = "~/Danmaku/OrbitalPatternNeon", order = 0)]
	public class OrbitalPatternNeon : ScriptableObject {
		public int TotalOrbitalArrays = 1;
		public float ArrayDegreeSpread = 30f;
		public bool AlignWithSpawnDirection = false;

		public float SpawnRadius = 0f;

		public List<GameObject> Spawn(BubblePool magazine, BulletArray array, Vector2 position, float offsetDegree) {
			List<GameObject> res = new List<GameObject>();
			for (int i = 0; i < TotalOrbitalArrays; i++) {
				float deg = offsetDegree + i * ArrayDegreeSpread - (TotalOrbitalArrays-1)/2f*ArrayDegreeSpread;

				for (int j = 0; j < array.BulletPerArray; j++) {
					float resdeg = deg + j*array.SpreadDegreeWithinArray - ((array.BulletPerArray-1)*array.SpreadDegreeWithinArray/2f);

					Vector2 offset = Vector2.zero;
					Vector2 radialDir= new Vector2(Mathf.Cos(resdeg * Mathf.Deg2Rad), Mathf.Sin(resdeg * Mathf.Deg2Rad));

					if (SpawnRadius != 0) offset = radialDir*SpawnRadius;

					GameObject orbitoid = magazine.Borrow(position+offset, Quaternion.identity);
					if (AlignWithSpawnDirection) orbitoid.transform.rotation = Quaternion.Euler(0f, 0f, resdeg);
					res.Add(orbitoid);
				}
			}
			return res;
		}
	}
}