using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace ZendeskSDK {

	/// <summary>
	/// This class models a Help Center deflection query (returning suggested articles).
	/// </summary>
	public class ZDKHelpCenterDeflection {
		
		private static string _logTag = "ZDKHelpCenterDeflection";
		
		public static void Log(string message) {
			if(Debug.isDebugBuild)
				Debug.Log(_logTag + "/" + message);
		}

		/// <summary>
		/// This models the free-form text query
		/// </summary>
		public string Query;

		/// <summary>
		/// This models the "label_names" parameter. This will be a comma-separated list of label names to restrict the search to.
		/// </summary>
		public string[] LabelNames;

		/// <summary>
		/// This models the "locale" parameter. This specifies that the search will be restricted to content with this locale.  The locale
		/// is in the format of "ll-cc", e.g. "en-us".  Locales in the form of "ll" are also permitted, e.g. "en".
		/// </summary>
		public string Locale;

		/// <summary>
		/// This models the "category" parameter.  This specifies that the search will be restricted to content that is in the given
		/// category.
		/// </summary>
		public int CategoryId;

		/// <summary>
		/// This models the "section" parameter.  This specifies that the search will be restricted to content that is in the given 
		/// section.
		/// </summary>
		public int SectionId;

		public ZDKHelpCenterDeflection() {
			Query = null;
			LabelNames = null;
			Locale = null;
			CategoryId = -1;
			SectionId = -1;
		}
	}
}		