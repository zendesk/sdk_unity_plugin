using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace ZendeskSDK {
	
	public enum LogLevel {
		Error, Warn, Info, Debug, Verbose
	}
	
	/// <summary>
	/// SDK Logger
	/// </summary>
	public class ZDKLogger : ZDKBaseComponent {
		
		private static ZDKLogger _instance;
		
		private static ZDKLogger instance() {
			if (_instance != null)
				return _instance;
			_instance = new ZDKLogger();
			return _instance;
		}

		override protected string GetIOsMethodPrefix() {
			return "_zendeskLogger";
		}
		
		/// <summary>
		/// Enable or disable debug logging output. iOS Only
		/// </summary>
		/// <param name="enabled">YES for debug logging</param>
		public static void SetLogLevelIOS(LogLevel level) {
			instance().DoIOS("setLogLevel", (int) level);
		}

		/// <summary>
		/// Set logger enabled
		/// </summary>
		/// <param name="enabled">enable ZDKLogger wiht YES, disable with NO</param>
		public static void Enable(bool enabled) {
			instance().Do("enableLogger", enabled);
		}

		#if UNITY_IPHONE

		[DllImport("__Internal")]
		private static extern void _zendeskLoggerEnableLogger(bool enabled);
		
		[DllImport("__Internal")]
		private static extern void _zendeskLoggerSetLogLevel(int level);
		
		#endif
	}
}		