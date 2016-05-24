using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace ZendeskSDK {

	/// <summary>
	/// This class models a Help Center search.  For more details about Help Centre search please see:
	/// https://developer.zendesk.com/rest_api/docs/help_center/search
	/// </summary>
	public class ZDKHelpCenterSearch {
		
		private static string _logTag = "ZDKHelpCenterSearch";
		
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
		/// This models the "include" parameter.  This specifies tne elements to side-load and include in the results.
		/// </summary>
		public string[] SideLoads;

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

		/// <summary>
		/// This models the "page" parameter. This specifies what page of results to return.  This is closely tied to the resultsPerPage
		/// property.
		/// </summary>
		public int Page;

		/// <summary>
		/// This models the "per_page" parameter.  This specifies how many results to return for the current page.  The current page is
		/// specified by the page property.
		/// </summary>
		public int ResultsPerPage;

		public ZDKHelpCenterSearch() {
			Query = null;
			LabelNames = null;
			Locale = null;
			SideLoads = null;
			CategoryId = -1;
			SectionId = -1;
			Page = -1;
			ResultsPerPage = -1;
		}
	}
}		