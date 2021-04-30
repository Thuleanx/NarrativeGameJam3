using UnityEngine;
using Thuleanx.Master;

namespace Thuleanx.Animation {
	public class PlayerRespawner : MonoBehaviour {
		public void Respawn() {
			Game.Instance.StartRepsawn();
		}
	}
}