using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace ZendeskSDK {
	
	public class ZDKStringUtil : ZDKBaseComponent {
		
		private static ZDKStringUtil _instance;
		
		private static ZDKStringUtil instance() {
			if (_instance != null)
				return _instance;
			_instance = new ZDKStringUtil();
			return _instance;
		}

		/// <summary>
		/// This method converts an array of strings into a comma separated string of the array's items. 
		/// For example an array with 
		/// three items, "one", "two" and "three" will be converted into the string "one,two,three".
		/// </summary>
		/// <param name="strings">An array of Strings to convert into a comma-separated string</param>
		/// <returns>A comma separated string of the items in the array or an empty string if there were none.</returns>
		public static string CsvStringFromArray(string[] strings) {
			if (strings == null)
				return "";
			return instance().Get<string>("csvStringFromArray", strings, strings.Length);
		}
		
		#if UNITY_IPHONE
		
		[DllImport("__Internal")]
		private static extern string _zendeskCsvStringFromArray(string[] charArray, int length);

		#endif
	}
}
