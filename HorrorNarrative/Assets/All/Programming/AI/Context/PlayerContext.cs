
using UnityEngine;

namespace Thuleanx.AI.Context {
	[CreateAssetMenu(fileName = "PlayerContext", menuName = "~/Context/PlayerContext", order = 0)]
	public class PlayerContext : AgentContext {
		[Tooltip("Player's Move Speed while aiming")]
		public float AimingMoveSpeed;

		[Tooltip("Player Move Speed while reloading")]
		public float ReloadingMoveSpeed = 1f;

		[Tooltip("Default Bullet Count loaded as the game loads"), Min(0)]
		public int DefaultBulletCount = 1;

		public PlayerEquipment DefaultEquipment = PlayerEquipment.NONE;
	}
}