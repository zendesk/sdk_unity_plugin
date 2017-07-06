using UnityEngine;
using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ZendeskSDK {

	public abstract class ZDKBaseComponent {

		protected void Log(string message) {
			if(Debug.isDebugBuild)
				Debug.Log(GetLogTag() + "/" + message);
		}

		protected virtual string GetLogTag() {
			return GetType().Name;
		}
		protected virtual string GetAndroidClass() {
			return "com.zendesk.unity.ZDK_Plugin";
		}
		protected virtual string GetIOsMethodPrefix() {
			return "_zendesk";
		}

		#if UNITY_ANDROID
		private AndroidJavaObject _provider;

		protected AndroidJavaObject Provider() {
			if (_provider != null)
				return _provider;
			using (var pluginClass = new AndroidJavaClass(GetAndroidClass()))
				_provider = pluginClass.CallStatic<AndroidJavaObject>("instance");
			return _provider;
		}
		#endif

		/// <summary>
		/// Call a native method requiring a callback that exists on iOS and Android
		/// </summary>
		/// <param name="methodName">Method name, camel-case</param>
		/// <param name="varargs">Any arguments to pass to native</param>
		protected void Call<T>(String methodName, Action<T,ZDKError> callback, params object[] varargs) {
			CallImpl(true, true, methodName, callback, varargs);
		}

		/// <summary>
		/// Call a native method requiring a callback that exists on iOS only
		/// </summary>
		/// <param name="methodName">Method name, camel-case</param>
		/// <param name="varargs">Any arguments to pass to native</param>
		protected void CallIOS<T>(String methodName, Action<T,ZDKError> callback, params object[] varargs) {
			CallImpl(true, false, methodName, callback, varargs);
		}

		/// <summary>
		/// Call a native method requiring a callback that exists on Android only
		/// </summary>
		/// <param name="methodName">Method name, camel-case</param>
		/// <param name="varargs">Any arguments to pass to native</param>
		protected void CallAndroid<T>(String methodName, Action<T,ZDKError> callback, params object[] varargs) {
			CallImpl(false, true, methodName, callback, varargs);
		}

		private void CallImpl<T>(bool ios, bool android, String methodName, Action<T,ZDKError> callback, params object[] varargs) {
			string methodNameCapped = methodName.Substring(0, 1).ToUpper() + methodName.Substring(1);
			Log("Unity : " + GetLogTag() + ":" + methodNameCapped);

			#if UNITY_IPHONE
			if (ios) {
				Type thisType = this.GetType();
				MethodInfo theMethod = thisType.GetMethod(GetIOsMethodPrefix() + methodNameCapped,
				                                          BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);

				if (varargs != null) {
					object[] args = new object[varargs.Length + 2];
					args[0] = ZDKConfig.SharedGameObject.name;
					args[1] = Callback<T>(callback);
					for (int i = 0; i < varargs.Length; i++)
						args[i + 2] = varargs[i];
					doCall(theMethod, null, args);
				} else
					doCall(theMethod, null, new object[] {ZDKConfig.SharedGameObject.name, Callback<T>(callback)});
			}
			#elif UNITY_ANDROID
			if (android) {
				if (varargs != null) {
					object[] args = new object[varargs.Length + 2];
					args[0] = ZDKConfig.SharedGameObject.name;
					args[1] = Callback<T>(callback);
					for (int i = 0; i < varargs.Length; i++)
						args[i + 2] = varargs[i];
					Provider().Call(methodName, args);
				} else
					Provider().Call(methodName, ZDKConfig.SharedGameObject.name, Callback<T>(callback));
			}
			#endif
		}

		/// <summary>
		/// Call a native method (with no callback) that exists on iOS and Android
		/// </summary>
		/// <param name="methodName">Method name, camel-case</param>
		/// <param name="varargs">Any arguments to pass to native</param>
		protected void Do(String methodName, params object[] varargs) {
			DoImpl(true, true, methodName, varargs);
		}

		/// <summary>
		/// Call a native method (with no callback) that exists on iOS only
		/// </summary>
		/// <param name="methodName">Method name, camel-case</param>
		/// <param name="varargs">Any arguments to pass to native</param>
		protected void DoIOS(String methodName, params object[] varargs) {
			DoImpl(true, false, methodName, varargs);
		}

		/// <summary>
		/// Call a native method (with no callback) that exists on Android only
		/// </summary>
		/// <param name="methodName">Method name, camel-case</param>
		/// <param name="varargs">Any arguments to pass to native</param>
		protected void DoAndroid(String methodName, params object[] varargs) {
			DoImpl(false, true, methodName, varargs);
		}

		private void DoImpl(bool ios, bool android, String methodName, params object[] varargs) {
			string methodNameCapped = methodName.Substring(0, 1).ToUpper() + methodName.Substring(1);
			Log("Unity : " + GetLogTag() + ":" + methodNameCapped);

			#if UNITY_IPHONE
			if (ios) {
				Type thisType = this.GetType();
				MethodInfo theMethod = thisType.GetMethod(GetIOsMethodPrefix() + methodNameCapped, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
				if (varargs != null)
					doCall(theMethod, null, varargs);
				else
					doCall(theMethod, null, null);
			}
			#elif UNITY_ANDROID
			if (android) {
				if (varargs != null)
					Provider().Call(methodName, varargs);
				else
					Provider().Call(methodName);
			}
			#endif
		}

		/// <summary>
		/// Call a native method (with no callback) that exists on iOS and Android and return a value
		/// </summary>
		/// <param name="methodName">Method name, camel-case</param>
		/// <param name="varargs">Any arguments to pass to native</param>
		protected T Get<T>(String methodName, params object[] varargs) {
			return GetImpl<T>(true, true, methodName, varargs);
		}

		/// <summary>
		/// Call a native method (with no callback) that exists on iOS only and return a value
		/// </summary>
		/// <param name="methodName">Method name, camel-case</param>
		/// <param name="varargs">Any arguments to pass to native</param>
		protected T GetIOS<T>(String methodName, params object[] varargs) {
			return GetImpl<T>(true, false, methodName, varargs);
		}

		/// <summary>
		/// Call a native method (with no callback) that exists on Android only and return a value
		/// </summary>
		/// <param name="methodName">Method name, camel-case</param>
		/// <param name="varargs">Any arguments to pass to native</param>
		protected T GetAndroid<T>(String methodName, params object[] varargs) {
			return GetImpl<T>(false, true, methodName, varargs);
		}

		private T GetImpl<T>(bool ios, bool android, String methodName, params object[] varargs) {
			string methodNameCapped = methodName.Substring(0, 1).ToUpper() + methodName.Substring(1);
			Log("Unity : " + GetLogTag() + ":" + methodNameCapped);

			#if UNITY_IPHONE
			if (ios) {
				Type thisType = this.GetType();
				MethodInfo theMethod = thisType.GetMethod(GetIOsMethodPrefix() + methodNameCapped, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
				if (varargs != null)
					return (T) doCall(theMethod, null, varargs);
				else
					return (T) doCall(theMethod, null, null);
			}
			#elif UNITY_ANDROID
			if (android) {
				if (varargs != null)
					return Provider().Call<T>(methodName, varargs);
				else
					return Provider().Call<T>(methodName);
			}
			#endif
			return default (T);
		}

		#if UNITY_ANDROID || UNITY_IPHONE
		protected string Callback<T>(Action<T,ZDKError> callback) {
			string callbackId = Guid.NewGuid().ToString();
			ZDKConfig.ActionCallbacks.Add(callbackId,callback);
			return callbackId;
		}
		#endif

		public object doCall(MethodBase Method, object Target, object[] input) {
			return Method.Invoke(Target, input);
		}
	}
}
