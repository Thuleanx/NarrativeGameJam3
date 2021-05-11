using UnityEngine;
using Yarn.Unity;
using UnityEngine.Playables;

namespace Thuleanx_Timeline {
	public class TimelineController : MonoBehaviour {
		[YarnCommand("PlayTimeline")]
		public void PlayTimeline() {
			GetComponent<PlayableDirector>()?.Play();
		}
	}
}