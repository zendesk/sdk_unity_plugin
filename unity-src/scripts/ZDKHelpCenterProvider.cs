using UnityEngine;
using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ZendeskSDK {

	public enum SortBy {
		Position, Title, CreatedAt, UpdatedAt
	}

	public enum SortOrder {
		Ascending, Descending
	}

	public class ZDKListArticleQuery {
		// ios and android for GetArticles, android only for GetFlatArticles
		public string[] LabelNames;

		// android only
		public string Include;
		public string Locale;
		public int Page;
		public int ResultsPerPage;
		public SortBy SortBy;
		public SortOrder SortOrder;
	}

	public class ZDKHelpCenterProvider : ZDKBaseComponent {

		private static ZDKHelpCenterProvider _instance;

		private static ZDKHelpCenterProvider instance() {
			if (_instance != null)
				return _instance;
			_instance = new ZDKHelpCenterProvider();
			return _instance;
		}

		override protected string GetAndroidClass() {
			return "com.zendesk.unity.providers.HelpCenterProvider";
		}

		override protected string GetIOsMethodPrefix() {
			return "_zendeskHelpCenterProvider";
		}

		/// <summary>
		/// Fetch a list of categories from a Help Center instance.
		/// </summary>
		/// <param name="callback">Callback that will deliver a list of categories available</param>
		public static void GetCategories(Action<ArrayList,ZDKError> callback) {
			instance().Call("getCategories", callback);
		}

		/// <summary>
		/// Fetch a list of sections for a given categoryId from a Help Center instance
		/// </summary>
		/// <param name="categoryId">String to specify what sections should be returned,
		/// only sections belonging to the category will be returned</param>
		/// <param name="callback">Callback that will deliver a list of sections available</param>
		public static void GetSections(string categoryId, Action<ArrayList,ZDKError> callback) {
			instance().Call("getSectionsForCategory", callback, categoryId);
		}

		/// <summary>
		/// Fetch a list of articles for a given sectionId from a Help Center instance
		/// </summary>
		/// <param name="sectionId">String to specify what articles should be returned,
		/// only articles belonging to the section will be returned</param>
		/// <param name="callback">Callback that will deliver a list of articles available</param>
		public static void GetArticles(string sectionId, Action<ArrayList,ZDKError> callback) {
			instance().Call("getArticlesForSection", callback, sectionId);
		}

		/// <summary>
		/// This method will search articles in your Help Center.
		/// This method will also sideload categories, sections and users.
		/// </summary>
		/// <param name="query">The query text used to perform the search</param>
		/// <param name="callback">The callback which will be called upon a successful or an erroneous response.</param>
		public static void SearchArticles(string query, Action<ArrayList,ZDKError> callback) {
			instance().Call("searchArticlesUsingQuery", callback, query);
		}

		/// <summary>
		/// This method will search articles in your Help Center filtered by an array of labels
		/// </summary>
		/// <param name="query">The query text used to perform the search</param>
		/// <param name="labels">The array of labels used to filter the search results</param>
		/// <param name="callback">The callback which will be called upon a successful or an erroneous response.</param>
		public static void SearchArticles(string query, string[] labels, Action<ArrayList,ZDKError> callback) {
			if (labels == null)
				labels = new string[0];
			instance().Call("searchArticlesUsingQueryAndLabels", callback, query, labels, labels.Length);
		}

		/// <summary>
		/// This method will search articles in your Help Center filtered by the parameters in the given ZDKHelpCenterSearch
		/// </summary>
		/// <param name="helpCenterSearch">The search to perform</param>
		/// <param name="callback">The callback which will be called upon a successful or an erroneous response.</param>
		public static void SearchArticles(ZDKHelpCenterSearch helpCenterSearch, Action<ArrayList,ZDKError> callback) {

			instance().Call("searchArticlesUsingHelpCenterSearch", callback,
			                helpCenterSearch.Query,
							helpCenterSearch.LabelNames, helpCenterSearch.LabelNames != null ? helpCenterSearch.LabelNames.Length : 0,
			                helpCenterSearch.Locale,
							helpCenterSearch.SideLoads, helpCenterSearch.SideLoads != null ? helpCenterSearch.SideLoads.Length : 0,
							helpCenterSearch.CategoryIds, helpCenterSearch.CategoryIds != null ? helpCenterSearch.CategoryIds.Length : 0,
							helpCenterSearch.SectionIds, helpCenterSearch.SectionIds != null ? helpCenterSearch.SectionIds.Length : 0,
							helpCenterSearch.Page, helpCenterSearch.ResultsPerPage);
		}

		public enum AttachmentType {
			Inline = 1, Block
		}

		/// <summary>
		/// This method returns a list of attachments for a single article.
		/// </summary>
		/// <param name="articleId">the identifier to be used to retrieve an article from a Help Center instance</param>
		/// <param name="attachmentType">The type of the attachment to get, block or inline (only on Android). This is ignored on iOS.</param>
		/// <param name="callback">The callback which will be called upon a successful or an erroneous response.</param>
		public static void GetAttachments(string articleId, AttachmentType attachmentType, Action<ArrayList,ZDKError> callback) {
			string type = "";
			if (attachmentType == AttachmentType.Block)
				type = "block";
			else if (attachmentType == AttachmentType.Inline)
				type = "inline";
			instance().Call("getAttachmentsForArticle", callback, articleId, type);
		}

		/// <summary>
		/// Fetch a list of articles for a given array of labels from a Help Center instance
		/// </summary>
		/// <param name="labels">An array of labels used to filter articles by</param>
		/// <param name="callback">The callback which will be called upon a successful or an erroneous response.</param>
		public static void GetArticles(string[] labels, Action<ArrayList,ZDKError> callback) {
			if (labels == null)
				labels = new string[0];
			instance().Call("getArticles", callback,
			                labels, labels.Length,
			                null, null, 0, 0, 0, 0);
		}

		/// <summary>
		/// Fetch a list of articles for a given query from a Help Center instance. Only the labels array is respected on iOS.
		/// </summary>
		/// <param name="labels">An array of labels used to filter articles by</param>
		/// <param name="callback">The callback which will be called upon a successful or an erroneous response.</param>
		public static void GetArticles(ZDKListArticleQuery query, Action<ArrayList,ZDKError> callback) {
			if (query.LabelNames == null)
				query.LabelNames = new string[0];
			instance().Call("getArticles", callback,
			                query.LabelNames, query.LabelNames.Length,
			                query.Include, query.Locale, query.Page, query.ResultsPerPage, (int) query.SortBy, (int) query.SortOrder);
		}

		/// <summary>
		/// Fetch a flat list of all articles from a Help Center instance.
		/// </summary>
		/// <param name="labels">An array of labels used to filter articles by</param>
		/// <param name="callback">The callback which will be called upon a successful or an erroneous response.</param>
		public static void GetFlatArticles(Action<ArrayList,ZDKError> callback) {
			instance().Call("getFlatArticles", callback, null, 0, null, null, 0, 0, 0, 0);
		}

		/// <summary>
		/// Fetch a flat list of articles for a given query from a Help Center instance. All parameters are ignored on iOS.
		/// </summary>
		/// <param name="labels">An array of labels used to filter articles by</param>
		/// <param name="callback">The callback which will be called upon a successful or an erroneous response.</param>
		public static void GetFlatArticles(ZDKListArticleQuery query, Action<ArrayList,ZDKError> callback) {
			if (query.LabelNames == null)
				query.LabelNames = new string[0];
			instance().Call("getFlatArticles", callback,
			                query.LabelNames, query.LabelNames.Length,
			                query.Include, query.Locale, query.Page, query.ResultsPerPage, (int) query.SortBy, (int) query.SortOrder);
		}

		public static void GetSuggestedArticles(ZDKHelpCenterDeflection helpCenterSearch, Action<Hashtable,ZDKError> callback) {
			if (helpCenterSearch.LabelNames == null)
				helpCenterSearch.LabelNames = new string[0];
			instance().Call("getSuggestedArticles", callback,
			                helpCenterSearch.Query,
			                helpCenterSearch.LabelNames,
			                helpCenterSearch.LabelNames.Length ,
			                helpCenterSearch.Locale,
			                helpCenterSearch.CategoryId,
			                helpCenterSearch.SectionId);
		}

		public static void GetArticle(string articleId, Action<Hashtable,ZDKError> callback) {
			instance().Call("getArticle", callback, articleId);
		}

		public static void GetSection(string sectionId, Action<Hashtable,ZDKError> callback) {
			instance().Call("getSection", callback, sectionId);
		}

		public static void GetCategory(string categoryId, Action<Hashtable,ZDKError> callback) {
			instance().Call("getCategory", callback, categoryId);
		}

		public static void UpvoteArticle(string articleId, Action<Hashtable,ZDKError> callback) {
			instance().Call("upvoteArticle", callback, articleId);
		}

		public static void DownvoteArticle(string articleId, Action<Hashtable,ZDKError> callback) {
			instance().Call("downvoteArticle", callback, articleId);
		}

		public static void DeleteVote(string voteId, Action<Hashtable,ZDKError> callback) {
			instance().Call("deleteVote", callback, voteId);
		}

		public static void SubmitRecordArticleView(string articleId, string htmlUrl, string title, string locale, Action<Hashtable,ZDKError> callback) {
  		instance().Call("submitRecordArticleView", callback, articleId, htmlUrl, title, locale);
 		}

		#if UNITY_IPHONE

		[DllImport("__Internal")]
		private static extern void _zendeskHelpCenterProviderGetCategories(string gameObjectName, string callbackId);
		[DllImport("__Internal")]
		private static extern void _zendeskHelpCenterProviderGetSectionsForCategory(string gameObjectName, string callbackId, string categoryId);
		[DllImport("__Internal")]
		private static extern void _zendeskHelpCenterProviderGetArticlesForSection(string gameObjectName, string callbackId, string sectionId);
		[DllImport("__Internal")]
		private static extern void _zendeskHelpCenterProviderSearchArticlesUsingQuery(string gameObjectName, string callbackId, string query);
		[DllImport("__Internal")]
		private static extern void _zendeskHelpCenterProviderSearchArticlesUsingQueryAndLabels(string gameObjectName,
		                                                                                          string callbackId,
		                                                                                          string query,
		                                                                                          string[] labelsArray,
		                                                                                          int labelsLength);
		[DllImport("__Internal")]
		private static extern void _zendeskHelpCenterProviderSearchArticlesUsingHelpCenterSearch(string gameObjectName,
		                                                                                            string callbackId,
		                                                                                            string query,
		                                                                                            string[] labelNames,
		                                                                                            int labelNamesLength,
		                                                                                            string locale,
		                                                                                            string[] sideLoads,
		                                                                                            int sideLoadsLength,
																									string[] categoryIds,
																									int categoryIdsLength,
																									string[] sectionIds,
																									int sectionIdsLength,
		                                                                                            int page,
		                                                                                            int resultsPerPage);
		[DllImport("__Internal")]
		private static extern void _zendeskHelpCenterProviderGetAttachmentsForArticle(string gameObjectName, string callbackId, string articleId, string attachmentType);
		[DllImport("__Internal")]
		private static extern void _zendeskHelpCenterProviderGetArticles(string gameObjectName, string callbackId, string[] labelsArray, int labelsLength,
		                                                                 string include, string locale, int page, int resultsPerPage, int sortBy, int sortOrder);

		[DllImport("__Internal")]
		private static extern void _zendeskHelpCenterProviderGetFlatArticles(string gameObjectName, string callbackId, string[] labelsArray, int labelsLength,
		                                                                     string include, string locale, int page, int resultsPerPage, int sortBy, int sortOrder);
		[DllImport("__Internal")]
		private static extern void _zendeskHelpCenterProviderGetSuggestedArticles(string gameObjectName,
		                                                                          string callbackId,
		                                                                          string query,
		                                                                          string[] labelNames,
		                                                                          int labelNamesLength,
		                                                                          string locale,
		                                                                          int categoryId,
		                                                                          int sectionId);
		[DllImport("__Internal")]
		private static extern void _zendeskHelpCenterProviderGetArticle(string gameObjectName, string callbackId, string articleId);
		[DllImport("__Internal")]
		private static extern void _zendeskHelpCenterProviderGetSection(string gameObjectName, string callbackId, string sectionId);
		[DllImport("__Internal")]
		private static extern void _zendeskHelpCenterProviderGetCategory(string gameObjectName, string callbackId, string categoryId);
		[DllImport("__Internal")]
		private static extern void _zendeskHelpCenterProviderUpvoteArticle(string gameObjectName, string callbackId, string articleId);
		[DllImport("__Internal")]
		private static extern void _zendeskHelpCenterProviderDownvoteArticle(string gameObjectName, string callbackId, string articleId);
		[DllImport("__Internal")]
		private static extern void _zendeskHelpCenterProviderDeleteVote(string gameObjectName, string callbackId, string voteId);
		[DllImport("__Internal")]
		private static extern void _zendeskHelpCenterProviderSubmitRecordArticleView(string gameObjectName, string callbackId, string articleId, string htmlUrl, string title, string locale);

		#endif
	}
}
