using UnityEngine;

namespace Thuleanx.AI.Context {
	[CreateAssetMenu(fileName = "GlobalContext", menuName = "~/Context/GlobalContext", order = 0)]
	public class GlobalContext : ScriptableObject {
		[HideInInspector] public Vector2 PlayerPosition;
	}
}