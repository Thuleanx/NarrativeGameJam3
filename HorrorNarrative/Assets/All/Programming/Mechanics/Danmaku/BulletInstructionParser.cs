using UnityEngine;
using FMOD_Thuleanx;

/*

*/

namespace Thuleanx.Mechanics.Danmaku {
	public class BulletInstructionParser {
		public static void StartParsing(AudioTrack track) {
			// track.OnMarker(Parse);
		}

		public static void StopParsing(AudioTrack track) {
			// track.RemoveTracker(Parse);
		}

		public static void Parse(string instruction) {
			string[] tokenized = instruction.Trim().Split(' ');
			if (tokenized[0].StartsWith("!")) { // means its a command
				string command = tokenized[0].Substring(1);

				if (command == "classic") {
					string objName = tokenized[1];
					string subcommand = tokenized[2];

					GameObject target = GameObject.Find(objName);
					if (subcommand == "start")
						target?.GetComponent<ClassicalHandler>()?.OnStart?.Invoke();
					if (subcommand == "end")
						target?.GetComponent<ClassicalHandler>()?.OnEnd?.Invoke();
					if (subcommand == "burst")
						target?.GetComponent<ClassicalHandler>()?.OnBurst?.Invoke();
				}

				if (command == "tele") {
					string objName = tokenized[1];
					string subcommand = tokenized[2];

					GameObject target = GameObject.Find(objName);
					if (subcommand == "prep")
						target?.GetComponent<TelegraphHandler>()?.OnTelegraph?.Invoke();
					if (subcommand == "start")
						target?.GetComponent<TelegraphHandler>()?.OnStart?.Invoke();
					if (subcommand == "end")
						target?.GetComponent<TelegraphHandler>()?.OnEnd?.Invoke();
					if (subcommand == "burst")
						target?.GetComponent<TelegraphHandler>()?.OnBurst?.Invoke();
				}

				if (command == "avatar") {
					string objName = tokenized[1];
					string subcommand = tokenized[2];
					string arg = tokenized[3];

					GameObject target = GameObject.Find(objName);

					if (subcommand == "trigger") target?.GetComponent<AvatarHandler>()?.OnTrigger?.Invoke(arg);
				}

				if (command == "Beat") {
					foreach (OnBeatTrigger trigger in GameObject.FindObjectsOfType<OnBeatTrigger>())
						trigger.OnBeat();
				}
			}
		}
	}
}