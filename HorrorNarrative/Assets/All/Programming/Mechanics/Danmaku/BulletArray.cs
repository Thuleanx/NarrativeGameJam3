using UnityEngine;
using Thuleanx.Optimization;

namespace Thuleanx.Mechanics.Danmaku {
	[CreateAssetMenu(fileName = "BulletArray", menuName = "~/Danmaku/BulletArray", order = 0)]
	public class BulletArray : ScriptableObject {
		public int BulletPerArray = 1;
		public float SpreadDegreeWithinArray = 0f;
	}
}