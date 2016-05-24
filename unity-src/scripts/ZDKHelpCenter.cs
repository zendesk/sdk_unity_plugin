using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace ZendeskSDK {

	public class ZDKHelpCenter : ZDKBaseComponent {
		
		public class ShowOptions {
			
			public enum ShowType { All, Category, Section, LabelsFilter }
			
			public ShowType Action;
			public int Id;
			public string[] Labels;
			public bool ShowContactUsButton;
			
			private ShowOptions() {
				Action = ShowType.All;
				Id = 0;
				Labels = null;
				ShowContactUsButton = true;
			}
			
			public static ShowOptions ListCategories() {
				ShowOptions o = new ShowOptions ();
				o.Action = ShowType.All;
				return o;
			}
			
			public static ShowOptions ListSections(int CategoryId) {
				ShowOptions o = new ShowOptions ();
				o.Action = ShowType.Category;
				o.Id = CategoryId;
				return o;
			}
			
			public static ShowOptions ListArticles(int SectionId) {
				ShowOptions o = new ShowOptions ();
				o.Action = ShowType.Section;
				o.Id = SectionId;
				return o;
			}
			
			public static ShowOptions ListArticlesByLabels(string[] Labels) {
				ShowOptions o = new ShowOptions ();
				o.Action = ShowType.LabelsFilter;
				o.Labels = Labels;
				return o;
			}
			
			public ShowOptions SetShowContactUsButton(bool Show) {
				this.ShowContactUsButton = Show;
				return this;
			}

			public void __Call(ZDKHelpCenter caller) {
				int LabelsLen = Labels != null && Action == ShowOptions.ShowType.LabelsFilter ? Labels.Length : 0;
				caller.Do("showHelpCenterWithOptions",
				          Action == ShowOptions.ShowType.All,
				          Action == ShowOptions.ShowType.Category ? Id : 0,
				          Action == ShowOptions.ShowType.Section ? Id : 0,
				          Action == ShowOptions.ShowType.LabelsFilter ? Labels : null, LabelsLen,
				          ShowContactUsButton);
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
		public static void ShowHelpCenter(ShowOptions options) {
			options.__Call(instance());
		}

		/// <summary>
		/// Displays a specific article. In Android it does this by using the the id of the article.
		/// In iOS it does this by using the article json returned from a callback to get articles.
		/// </summary>
		/// <param name="informationString"> Android: article id. iOS: article json string</param>
		public static void ViewSpecificArticle(string informationString){
			instance().Do("viewArticle", informationString);
		}
		
		#if UNITY_IPHONE
		
		[DllImport("__Internal")]
		private static extern void _zendeskHelpCenterShowHelpCenter();
		[DllImport("__Internal")]
		private static extern void _zendeskHelpCenterShowHelpCenterWithOptions(bool listCats, int listSections, int listArticles,
		                                                                       string[] labels, int labelsLen,
		                                                                       bool showContactUsButton);
		[DllImport("__Internal")]
		private static extern void _zendeskHelpCenterViewArticle(string jsonData);

		#endif
	}
}
