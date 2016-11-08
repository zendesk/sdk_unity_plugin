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
		/// Enables push when using GCM and webhooks
		/// </summary>
		public static void EnableWithIdentifier(String identifier, Action<Hashtable, ZDKError> callback) {
			instance().Call("enablePushWithIdentifier", callback, identifier);
		}

		/// <summary>
		/// Enables push when using Urban Airship
		/// </summary>
		public static void EnableWithUAChannelId(String identifier, Action<Hashtable, ZDKError> callback) {
			instance().Call("enablePushWithUAChannelId", callback, identifier);
		}

        /// <summary>
        /// Disable push notifications for the current device.
        /// </summary>
        public static void Disable(String identifier, Action<Hashtable, ZDKError> callback) {
			instance().Call("disablePush", callback, identifier);
        }
		
		#if UNITY_IPHONE
		[DllImport("__Internal")]
		private static extern void _zendeskPushEnablePushWithIdentifier(string gameObjectName, string callbackId, String identifier);
		[DllImport("__Internal")]
		private static extern void _zendeskPushEnablePushWithUAChannelId(string gameObjectName, string callbackId, String identifier);
		[DllImport("__Internal")]
		private static extern void _zendeskPushDisablePush(string gameObjectName, string callbackId, String identifier);

		#endif
	}
}
