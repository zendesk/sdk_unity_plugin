using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace ZendeskSDK {

	/// <summary>
	/// Request creation config object.
	/// </summary>
	public class ZDKRequestCreationConfig {
		private static string _logTag = "ZDKRequestCreationConfig";

		public static void Log(string message) {
			if(Debug.isDebugBuild)
				Debug.Log(_logTag + "/" + message);
		}

		/// <summary>
		/// Tags to be included when creating a request.
		/// </summary>
		public string[] Tags;

		/// <summary>
		/// Additional free text to be appended to the request description.
		/// </summary>
		public string AdditionalRequestInfo;

		/// <summary>
		/// The subject of the request. Android only
		/// </summary>
		public string RequestSubject;

		public ZDKRequestCreationConfig() {
			Tags = null;
			AdditionalRequestInfo = null;
			RequestSubject = null;
		}
	}

	/// <summary>
	/// Core SDK class providing access to request deflection, creation.
	/// </summary>
	public class ZDKRequests : ZDKBaseComponent {

		private static ZDKRequests _instance;

		private static ZDKRequests instance() {
			if (_instance != null)
				return _instance;
			_instance = new ZDKRequests();
			return _instance;
		}

		override protected string GetIOsMethodPrefix() {
			return "_zendeskRequests";
		}

		/// <summary>
		/// Displays a simple request creation modal.
		/// </summary>
		public static void ShowRequestCreation() {
			instance().Do("showRequestCreation");
		}

		/// <summary>
		/// Displays the request list screen.
		/// </summary>
		public static void ShowRequestList() {
		    instance().Do("showRequestList");
		}

		public static void ShowRequestCreationWithConfig(ZendeskSDK.ZDKRequestCreationConfig config) {

			if (config != null) {
			    #if UNITY_IPHONE
				_zendeskRequestsConfigureZDKRequests(config.RequestSubject,
					config.Tags, config.Tags != null ? config.Tags.Length : 0,
					config.AdditionalRequestInfo);
				_zendeskRequestsShowRequestCreation();
				#elif UNITY_ANDROID
				instance().DoAndroid("showRequestCreationWithConfig", config.RequestSubject, config.Tags, config.AdditionalRequestInfo);
                #endif
			}
		}

		#if UNITY_IPHONE
		[DllImport("__Internal")]
		private static extern void _zendeskRequestsShowRequestCreation();
		[DllImport("__Internal")]
		private static extern void _zendeskRequestsConfigureZDKRequests(string requestSubject, string[] tags, int tagLength, string additionalInfo);
		[DllImport("__Internal")]
		private static extern void _zendeskRequestsShowRequestList();
		#endif
	}
}
