using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace ZendeskSDK {
	
	public class ZDKUserProvider : ZDKBaseComponent {
		
		private static ZDKUserProvider _instance;
		
		private static ZDKUserProvider instance() {
			if (_instance != null)
				return _instance;
			_instance = new ZDKUserProvider();
			return _instance;
		}

		override protected string GetAndroidClass() {
			return "com.zendesk.unity.providers.UserProvider";
		}
		override protected string GetIOsMethodPrefix() {
			return "_zendeskUserProvider";
		}
		
		/// <summary>
		/// Add a list of tags for the current user.
		/// </summary>
		/// <param name="tags">List of string tags to add.</param>
		/// <param name="callback">block callback executed on error or success states</param>
		public static void AddTags(string[] tags, Action<ArrayList,ZDKError> callback) {
			if (tags == null)
				tags = new string[0];
			instance().Call("addTags", callback, tags, tags.Length);
		}
		
		/// <summary>
		/// Remove a list of tags for the current user.
		/// </summary>
		/// <param name="tags">List of string tags to remove.</param>
		/// <param name="callback">block callback executed on error or success states</param>
		public static void DeleteTags(string[] tags, Action<ArrayList,ZDKError> callback) {
			if (tags == null)
				tags = new string[0];
			instance().Call("deleteTags", callback, tags, tags.Length);
		}
		
		/// <summary>
		/// Get current user info.
		/// </summary>
		/// <param name="callback">block callback executed on error or success states</param>
		public static void GetUser(Action<Hashtable,ZDKError> callback) {
			instance().Call("getUser", callback);
		}
		
		/// <summary>
		/// Get current user field info as a list of key-value pairs
		/// </summary>
		/// <param name="callback">block callback executed on error or success states</param>
		public static void GetUserFields(Action<ArrayList,ZDKError> callback) {
			instance().Call("getUserFields", callback);
		}
		
		/// <summary>
		/// Set current user field info. The callback provides a Hashtable of string-string pairs representing the new user fields.
		/// </summary>
		/// <param name="fields">A hashtable of string-string pairs representing custom user info.</param>
		/// <param name="callback">block callback executed on error or success states</param>
		public static void SetUserFields(Hashtable fields, Action<Hashtable,ZDKError> callback) {
			string fieldJson = ZenJSON.Serialize(fields);
			instance().Call("setUserFields", callback, fieldJson);
		}

		#if UNITY_IPHONE
		
		[DllImport("__Internal")]
		private static extern void _zendeskUserProviderAddTags(string gameObjectName, string callbackId, string[] tags, int tagsLength);
		
		[DllImport("__Internal")]
		private static extern void _zendeskUserProviderDeleteTags(string gameObjectName, string callbackId, string[] tags, int tagsLength);
		
		[DllImport("__Internal")]
		private static extern void _zendeskUserProviderGetUser(string gameObjectName, string callbackId);
		
		[DllImport("__Internal")]
		private static extern void _zendeskUserProviderGetUserFields(string gameObjectName, string callbackId);
		
		[DllImport("__Internal")]
		private static extern void _zendeskUserProviderSetUserFields(string gameObjectName, string callbackId, string fieldMap);

		#endif
	}
}
