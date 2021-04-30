using System.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

namespace FMOD_Thuleanx {
	[System.Serializable]
	public class DestinationMarkerEvent : UnityEvent<string> {}

	// adopted from https://alessandrofama.com/tutorials/fmod-unity/beat-marker-system/
	// provide a callback for beats in an event
	public class MarkerReader
	{
		public MarkerReader(FMOD.Studio.EventInstance instance) {
			timelineInfo = new TimelineInfo();
			timelineHandle = GCHandle.Alloc(timelineInfo, GCHandleType.Pinned);
			beatCallback = new FMOD.Studio.EVENT_CALLBACK(BeatEventCallback);
			instance.setUserData(GCHandle.ToIntPtr(timelineHandle));
			instance.setCallback(beatCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT | FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
			this.instance = instance;
		}

		public void StopAndClear()
		{
			instance.setUserData(IntPtr.Zero);
			instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			instance.release();
			timelineHandle.Free();
		}

		// provide a callback that gets called whenever destination marker happens, invoked with the name of the marker
		public void AttachCallback(UnityAction<string> callback) {
			timelineInfo.markerCallback.AddListener(callback);
		}

		public void RemoveCallback(UnityAction<string> callback) {
			timelineInfo.markerCallback.RemoveListener(callback);
		}

		[StructLayout(LayoutKind.Sequential)]
		class TimelineInfo
		{
			public int currentMusicBeat = 0;
			public FMOD.StringWrapper lastMarker = new FMOD.StringWrapper();
			public UnityEvent<string> markerCallback = new DestinationMarkerEvent();
		}

		TimelineInfo timelineInfo;
		GCHandle timelineHandle;
		FMOD.Studio.EVENT_CALLBACK beatCallback;
		FMOD.Studio.EventInstance instance;


		[AOT.MonoPInvokeCallback(typeof(FMOD.Studio.EVENT_CALLBACK))]
		static FMOD.RESULT BeatEventCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type, FMOD.Studio.EventInstance instance, IntPtr parameterPtr)
		{
			IntPtr timelineInfoPtr;
			FMOD.RESULT result = instance.getUserData(out timelineInfoPtr);
			if (result != FMOD.RESULT.OK)
			{
				Debug.LogError("Timeline Callback error: " + result);
			}
			else if (timelineInfoPtr != IntPtr.Zero)
			{
				GCHandle timelineHandle = GCHandle.FromIntPtr(timelineInfoPtr);
				TimelineInfo timelineInfo = (TimelineInfo)timelineHandle.Target;

				switch (type)
				{
					case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT:
						{
							var parameter = (FMOD.Studio.TIMELINE_BEAT_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_BEAT_PROPERTIES));
							timelineInfo.currentMusicBeat = parameter.beat;
						}
						break;
					case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER:
						{
							var parameter = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
							timelineInfo.lastMarker = parameter.name;
							timelineInfo.markerCallback.Invoke(timelineInfo.lastMarker);
						}
						break;
				}
			}
			return FMOD.RESULT.OK;
		}
	}
}
