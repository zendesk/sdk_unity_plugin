using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace ZendeskSDK {

	public class ZDKSettingsProvider : ZDKBaseComponent {
		
		private static ZDKSettingsProvider _instance;
		
		private static ZDKSettingsProvider instance() {
			if (_instance != null)
				return _instance;
			_instance = new ZDKSettingsProvider();
			return _instance;
		}

		override protected string GetAndroidClass() {
			return "com.zendesk.unity.providers.SettingsProvider";
		}
		override protected string GetIOsMethodPrefix() {
			return "_zendeskSettingsProvider";
		}

		/// <summary>
		/// Get SDK Settings from Zendesk instance
		/// </summary>
		/// <param name="callback">block callback invoked on success and error states</param>
		public static void GetSdkSettings(Action<Hashtable,ZDKError> settingsCallback) {
			instance().Call("getSettings", settingsCallback);
		}
		
		/// <summary>
		/// Get SDK Settings from Zendesk instance using the specified locale. Locale setting is iOS only, and is ignored on Android.
		/// </summary>
		/// <param name="locale">IETF language code. Config returned from server will contain 
		/// this string if the local is supported, will be the default locale otherwise</param>
		/// <param name="callback">block callback invoked on success and error states</param>
		public static void GetSdkSettings(string locale, Action<Hashtable,ZDKError> settingsCallback) {
			#if UNITY_ANDROID
			instance().Call("getSettings", settingsCallback);
			#else
			instance().CallIOS("getSettingsWithLocale", settingsCallback, locale);
			#endif
		}

		#if UNITY_IPHONE

		[DllImport("__Internal")]
		private static extern void _zendeskSettingsProviderGetSettings(string gameObjectName, string callbackId);
		
		[DllImport("__Internal")]
		private static extern void _zendeskSettingsProviderGetSettingsWithLocale(string gameObjectName, string callbackId, string locale);

		#endif
	}
}		
