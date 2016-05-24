using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace ZendeskSDK {

	/// <summary>
	/// ZDKPush contains interfaces for configuring push notifications.
	/// </summary>
	public class ZDKPush : ZDKBaseComponent {
		
		private static ZDKPush _instance;
		
		private static ZDKPush instance() {
			if (_instance != null)
				return _instance;
			_instance = new ZDKPush();
			return _instance;
		}
		
		override protected string GetAndroidClass() {
			return "com.zendesk.unity.push.ZDKPush";
		}
		override protected string GetIOsMethodPrefix() {
			return "_zendeskPush";
		}
		
		/// <summary>
		/// Enable push notifications for the current device.
		/// Uses the Urban Airship channel ID if this SDK was built with Urban Airship, otherwise uses the GCM/APN push ID.
		/// An identity must be authenticated with ZDKConfig before calling this method.
		/// </summary>
		public static void Enable(Action<Hashtable,ZDKError> callback) {
			instance().Call("enable", callback);
		}
		
		/// <summary>
		/// Disable push notifications for the current device.
		/// </summary>
		public static void Disable(Action<Hashtable,ZDKError> callback) {
			instance().Call("disable", callback);
		}
		
		#if UNITY_IPHONE
		
		[DllImport("__Internal")]
		private static extern void _zendeskPushEnable(string gameObjectName, string callbackId);
		[DllImport("__Internal")]
		private static extern void _zendeskPushDisable(string gameObjectName, string callbackId);

		#endif
	}
}
