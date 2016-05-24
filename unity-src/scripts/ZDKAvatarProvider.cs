using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace ZendeskSDK {
	
	public class ZDKAvatarProvider : ZDKBaseComponent {
		
		private static ZDKAvatarProvider _instance;
		
		private static ZDKAvatarProvider instance() {
			if (_instance != null)
				return _instance;
			_instance = new ZDKAvatarProvider();
			return _instance;
		}

		override protected string GetAndroidClass() {
			return null; // there is no Android avatar provider
		}
		override protected string GetIOsMethodPrefix() {
			return "_zendeskAvatarProvider";
		}

		/// <summary>
		/// Get the image/avatar data for a given URL.
		/// iOS only (callback will not return on Android)
		/// </summary>
		/// <param name="avatarUrl">String url of the image to be fetched.</param>
		/// <param name="callback">block callback executed on error or success states</param>
		public static void GetAvatar(string avatarUrl, Action<byte[],ZDKError> callback) {
			instance().CallIOS("getAvatar", callback, avatarUrl);
		}

		#if UNITY_IPHONE

		[DllImport("__Internal")]
		private static extern void _zendeskAvatarProviderGetAvatar(string gameObjectName, string callbackId, string avatarUrl);

		#endif
	}
}
