using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace ZendeskSDK {

	/// <summary>
	/// Error response from a failed Zendesk provider callback request.
	/// </summary>
	public class ZDKError {
		
		private static string _logTag = "ZDKError";
		
		public static void Log(string message) {
			if(Debug.isDebugBuild)
				Debug.Log(_logTag + "/" + message);
		}

		/// <summary>
		/// Description of the error if available.
		/// </summary>
		public string Description;

		/// <summary>
		/// Error code if available.
		/// </summary>
		public Int64 Code;

		public ZDKError() {

		}

		#if UNITY_EDITOR || (!UNITY_ANDROID && !UNITY_IPHONE)
		public ZDKError(Hashtable dict) {
			Log ("Unity : ZDKError init");
		}

		#elif UNITY_IPHONE

		public ZDKError(Hashtable dict) {
			if (dict["localizedDescription"] != null) {
				Description = (string) dict["localizedDescription"];
			}
			if (dict["code"] != null) {
				Code = (Int64) dict["code"];
			}
		}

		#elif UNITY_ANDROID

		public ZDKError(Hashtable dict) {
			if(dict["reason"] != null){
				Description = (string) dict["reason"];
			}
			if(dict["status"] != null){
				Code = (Int64) dict["status"];
			}
		}

		#endif
	}
}		