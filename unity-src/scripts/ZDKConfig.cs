using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace ZendeskSDK {

	/// <summary>
	/// ZDKConfig is responsible for initialization of the SDK and manages the backend configuration.
	/// </summary>
	public sealed class ZDKConfig : ZDKBaseComponent {

		private ZDKConfig(){}

		/// <summary>
		/// Get the API instance (singleton).
		/// </summary>
		private static ZDKConfig Instance { get { return Nested.instance; } }

		/// <summary>
		/// Used internally by Zendesk. Don't mess with this.
		/// </summary>
		public static Hashtable ActionCallbacks = new Hashtable();
		
		/// <summary>
		/// Used internally by Zendesk. Don't mess with this.
		/// </summary>
		public static GameObject SharedGameObject;
		
		private class Nested {
			static Nested(){}
			internal static readonly ZDKConfig instance = new ZDKConfig();
		}

		override protected string GetIOsMethodPrefix() {
			return "_zendeskConfig";
		}
		
		private bool checkInitialized() {
			#if UNITY_ANDROID || UNITY_IPHONE
			if (SharedGameObject != null) {
				return true;
			} 
			else {
				Debug.LogError("The Zendesk SDK needs to be inititalized with a Unity GameObject.");
				return false;
			}
			#else
			return true;
			#endif
		}
		
		/// <summary>
		/// Initialize the specified gameObject, zendeskUrl, applicationId and oauthClientId.
		/// </summary>
		/// <param name="gameObject">Game object. This object will persist scene changes because DontDestroyOnLoad will be called on it.</param>
		/// <param name="zendeskUrl">Zendesk URL.</param>
		/// <param name="applicationId">Application identifier.</param>
		/// <param name="oauthClientId">Oauth client identifier.</param>
		public static void Initialize(GameObject gameObject, String zendeskUrl, String applicationId, String oauthClientId) {
			SharedGameObject = gameObject;
			UnityEngine.Object.DontDestroyOnLoad(gameObject);

			Instance.Do ("initialize", zendeskUrl, applicationId, oauthClientId);
		}

		/// <summary>
		/// Configure the User Identity with an anonymous user.
		/// </summary>
		public static void AuthenticateAnonymousIdentity() {
			if (!Instance.checkInitialized())
				return;
			Instance.Do("authenticateAnonymousIdentity", null, null, null);
		}

		/// <summary>
		/// Configure the User Identity with an anonymous user.
		/// </summary>
		/// <param name="name">Username</param>
		/// <param name="email">Email Address</param>
		/// <param name="externalId">Identifier</param>
		public static void AuthenticateAnonymousIdentity(string name, string email, string externalId) {
			if (!Instance.checkInitialized())
				return;
			Instance.Do("authenticateAnonymousIdentity", name, email, externalId);
		}

		/// <summary>
		/// Configure the User Identity with a Jwt Authenticated user.
		/// </summary>
		/// <param name="jwtUserIdentity">JWT Identifier</param>
		public static void AuthenticateJwtUserIdentity(string jwtUserIdentity) {
			if (!Instance.checkInitialized())
				return;
			Instance.Do("authenticateJwtUserIdentity", jwtUserIdentity);
		}

		public static void SetCustomFields(Hashtable fields) {
		    Instance.Do("setCustomFields", ZenJSON.Serialize(fields));
		}

		public static string GetCustomFields() {
		    return Instance.Get<string>("getCustomFields");
		}

		/// <summary>
		/// Reload the config from the server, reload will be started if a reload
		/// is not already in progress and the reload interval has passed. This method
		/// will automatically be invoked when the application enters the foreground to
		/// check for updates if due.
		/// iOS Only.
		/// </summary>
		public static void ReloadiOS() {
			if (!Instance.checkInitialized())
				return;
			Instance.DoIOS("reload");
		}
		
		/// <summary>
		/// Sets whether COPPA is enabled for the SDK.
		/// </summary>
		public static void SetCoppaEnabled(bool enabled) {
			if (!Instance.checkInitialized())
				return;
			Instance.Do("setCoppaEnabled", enabled);
		}

		/// <summary>
		/// Sets the user's locale for the SDK. Best when called before Initialize.
		/// </summary>
		public static void SetUserLocale(string locale) {
			Instance.Do("setUserLocale", locale);
		}
		
		// Game Message Callbacks
		
		public static void CallbackResponse(string results) {
			Hashtable resultsDict = (Hashtable)ZenJSON.Deserialize(results);
			String methodName = resultsDict["methodName"] as String;
			if (ActionCallbacks.ContainsKey(resultsDict["callbackId"])) {
				Type[] parms = ActionCallbacks[resultsDict["callbackId"]].GetType().GetGenericArguments();
				Type arg1 = parms[0];
				
				if (arg1 == typeof(byte[])) {
					Action<byte[],ZDKError> callback = (Action<byte[],ZDKError>) ActionCallbacks[resultsDict["callbackId"]];
					callback(parseByteArray(resultsDict), parseZDKError(resultsDict));
				} else if (arg1 == typeof(Hashtable)) {
					Action<Hashtable,ZDKError> callback = (Action<Hashtable,ZDKError>) ActionCallbacks[resultsDict["callbackId"]];
					callback(parseHashtable(resultsDict), parseZDKError(resultsDict));
				} else {
					Action<ArrayList,ZDKError> callback = (Action<ArrayList,ZDKError>) ActionCallbacks[resultsDict["callbackId"]];
					callback(parseArrayList(resultsDict), parseZDKError(resultsDict));
				}
				ActionCallbacks.Remove(resultsDict["callbackId"]);
			}
			else {
				Debug.Log("ERROR: " + methodName + " - Missing callbackId for action in results.  Key = " + resultsDict["callbackId"]);
			}
		}
		
		// Result parsers
		
		private static byte[] parseByteArray(Hashtable resultsDict) {
			byte[] result = null;
			if (resultsDict["result"] != null) {
				result = System.Convert.FromBase64String((string)resultsDict["result"]);
			}
			return result;
		}
		
		private static ZDKError parseZDKError(Hashtable resultsDict) {
			ZDKError error = null;
			if (resultsDict["error"] != null) {
				error = new ZDKError ((Hashtable)ZenJSON.Deserialize ((string)resultsDict["error"]));
			}
			return error;
		}
		
		private static Hashtable parseHashtable(Hashtable resultsDict) {
			Hashtable result = null;
			if (resultsDict["result"] != null) {
				result = (Hashtable)ZenJSON.Deserialize((string)resultsDict["result"]);
			}
			return result;
		}
		
		private static ArrayList parseArrayList(Hashtable resultsDict) {
			ArrayList result = null;
			if (resultsDict["result"] != null) {
				result = (ArrayList)ZenJSON.Deserialize((string)resultsDict["result"]);
			}
			return result;
		}

		#if UNITY_IPHONE

		[DllImport("__Internal")]
		private static extern void _zendeskConfigInitialize(string zendeskUrl, string applicationId, string oauthClientId);
		[DllImport("__Internal")]
		private static extern void _zendeskConfigAuthenticateAnonymousIdentity(string name, string email, string externalId);
		[DllImport("__Internal")]
		private static extern void _zendeskConfigAuthenticateJwtUserIdentity(string jwtUserIdentity);
		[DllImport("__Internal")]
		private static extern void _zendeskConfigReload();
		[DllImport("__Internal")]
		private static extern void _zendeskConfigSetCoppaEnabled(bool enabled);
		[DllImport("__Internal")]
		private static extern void _zendeskConfigSetUserLocale(string locale);
		[DllImport("__Internal")]
		private static extern void _zendeskConfigSetCustomFields(string fields);
		[DllImport("__Internal")]
		private static extern string _zendeskConfigGetCustomFields();
		[DllImport("__Internal")]
		private static extern void _zendeskConfigSetContactConfiguration(string[] tags, int tagsLength, string additionalInfo, string requestSubject);

		#endif
	}
}
