using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace ZendeskSDK {

	public class ZDKHelpCenter : ZDKBaseComponent {

		public class ContactConfiguration {

			private string requestSubject;
			private string[] tags;
			private string additionalInfo;

			public string RequestSubject {
				get { return requestSubject; }
				set { requestSubject = value; }
			}

			public string[] Tags {
				get { return tags; }
				set { tags = value; }
			}

			public string AdditionalInfo {
				get { return additionalInfo; }
				set { additionalInfo = value; }
			}
		}

		public enum ContactUsButtonVisibility {
			  Off, ArticleListOnly, ArticleListAndArticle
		};

		public class HelpCenterOptions {

			private long[] categoryIds;
			private long[] sectionIds;
			private ContactUsButtonVisibility contactUsButtonVisibility;
			private bool collapseSections;
			private string[] labelNames;
			private ContactConfiguration contactConfiguration;
			private bool articleVoting;

			public HelpCenterOptions() {
				ContactUsButtonVisibility = ContactUsButtonVisibility.ArticleListAndArticle;
				CollapseSections = false;
				ArticleVoting = true;
			}

			public long[] IncludeCategoryIds {
				get { return categoryIds; }
				set { categoryIds = value; }
			}

			public long[] IncludeSectionIds {
				get { return sectionIds; }
				set { sectionIds = value; }
			}

			public ContactUsButtonVisibility ContactUsButtonVisibility {
				get { return contactUsButtonVisibility; }
				set { contactUsButtonVisibility = value; }
 			}

			public bool CollapseSections {
				get { return collapseSections; }
				set { collapseSections = value; }
			}

			public bool ArticleVoting {
		    get { return articleVoting; }
		    set { articleVoting = value; }
			}

			public string[] IncludeLabelNames {
				get { return labelNames; }
				set { labelNames = value; }
			}

			public ContactConfiguration ContactConfiguration {
				get { return contactConfiguration; }
				set { contactConfiguration = value; }
			}

		}

		private static ZDKHelpCenter _instance;

		private static ZDKHelpCenter instance() {
			if (_instance != null)
				return _instance;
			_instance = new ZDKHelpCenter();
			return _instance;
		}

		override protected string GetIOsMethodPrefix() {
			return "_zendeskHelpCenter";
		}

		/// <summary>
		/// Displays the Help Center view
		/// </summary>
		public static void ShowHelpCenter() {
			instance().Do("showHelpCenter");
		}

		/// <summary>
		/// Displays the Help Center view
		/// </summary>
		public static void ShowHelpCenter(HelpCenterOptions options) {
			#if UNITY_IPHONE
			_ShowHelpCenterIos(options);
			#elif UNITY_ANDROID
			_ShowHelpCenterAndroid(options);
			#endif
		}

		private static void _ShowHelpCenterAndroid(HelpCenterOptions options) {
			instance().DoAndroid("showHelpCenter",
				options.CollapseSections,
				(int) options.ContactUsButtonVisibility,
				options.IncludeLabelNames,
				options.IncludeSectionIds,
				options.IncludeCategoryIds,
				options.ContactConfiguration != null ? options.ContactConfiguration.Tags : null,
				options.ContactConfiguration != null ? options.ContactConfiguration.AdditionalInfo : null,
				options.ContactConfiguration != null ? options.ContactConfiguration.RequestSubject : null,
				options.ArticleVoting);
		}

		// corresponds to _zendeskHelpCenterShowHelpCenter
		private static void _ShowHelpCenterIos(HelpCenterOptions options) {

			bool includeCategories = options.IncludeCategoryIds != null && options.IncludeCategoryIds.Length > 0;
			bool includeSections = options.IncludeSectionIds != null && options.IncludeSectionIds.Length > 0;

			string[] ids = null;

			if (includeCategories) {
				ids = new string[options.IncludeCategoryIds.Length];

				for (int i = 0; i < options.IncludeCategoryIds.Length; i++) {
					ids[i] = options.IncludeCategoryIds[i].ToString();
				}

			} else if (includeSections) {
				ids = new string[options.IncludeSectionIds.Length];

				for (int i = 0; i < options.IncludeSectionIds.Length; i++) {
					ids[i] = options.IncludeSectionIds[i].ToString();
				}

			}

			bool includeAll = ids == null || ids.Length == 0;

			if (options.ContactConfiguration != null) {
				instance ().DoIOS ("configureZDKRequests", options.ContactConfiguration.RequestSubject,
				options.ContactConfiguration.Tags, options.ContactConfiguration.Tags != null ? options.ContactConfiguration.Tags.Length : 0,
				options.ContactConfiguration.AdditionalInfo);
			}
			// Will this conflict with the signature of Android?
			int contactUsInt = (int) options.ContactUsButtonVisibility;
			instance().DoIOS("showHelpCenterWithOptions",
				options.IncludeLabelNames,
				options.IncludeLabelNames != null ? options.IncludeLabelNames.Length : 0,
				includeAll,
				includeCategories,
				includeSections,
				ids,
				ids != null ? ids.Length : 0,
				contactUsInt,
				options.ArticleVoting);
		}

		/// <summary>
		/// Displays a specific article when passed an article ID
		/// </summary>
		/// <param name="informationString"> article ID.</param>
		public static void ViewSpecificArticle(string informationString){
			instance().Do("viewArticle", informationString);
		}

		#if UNITY_IPHONE

		[DllImport("__Internal")]
		private static extern void _zendeskHelpCenterShowHelpCenter();

		[DllImport("__Internal")]
		private static extern void _zendeskHelpCenterShowHelpCenterWithOptions(
			string[] labels, int labelsLength, bool includeAll, bool includeCategories,
			bool includeSections, string[] ids, int idsLength, int hideContactSupport, bool articleVoting);

		[DllImport("__Internal")]
		private static extern void _zendeskHelpCenterViewArticle(string articleId);

		[DllImport("__Internal")]
		private static extern void _zendeskHelpCenterConfigureZDKRequests(string requestSubject, String[] tags, int tagsLength, String additionalData);

		#endif
	}
}
