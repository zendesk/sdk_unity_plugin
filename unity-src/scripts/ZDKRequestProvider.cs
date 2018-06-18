using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace ZendeskSDK {

	public class ZDKCreateRequest {
		/// <param name="tags">List of label that mark the request</param>
		public string[] Tags;
		/// <param name="attachments">List of ZDKUploadResponse objects</param>
		public string[] Attachments;
		/// <param name="subject">Message describing the subject of the request</param>
		public string Subject;
		/// <param name="description">More detailed description of a problem</param>
		public string Description;

		// android only
		/// <param name="email">End-user's email address (Android only)</param>
		public string Email;
//		public long TicketFormId;
//		public Hashtable Metadata; // String map of String,String maps
//		public Hashtable CustomFields; // list of Long,String pairs
//		public String Id;

		public ZDKCreateRequest() {
			//
		}

		public ZDKCreateRequest(string email, string subject, string description) {
			this.Email = email;
			this.Subject = subject;
			this.Description = description;
		}

		public ZDKCreateRequest(string email, string subject, string description, string[] tags) {
			this.Email = email;
			this.Subject = subject;
			this.Description = description;
			this.Tags = tags;
		}

		public ZDKCreateRequest(string email, string subject, string description, string[] tags, string[] attachments) {
			this.Email = email;
			this.Subject = subject;
			this.Description = description;
			this.Tags = tags;
			this.Attachments = attachments;
		}
	}

	public class ZDKRequestProvider : ZDKBaseComponent {

		private static ZDKRequestProvider _instance;

		private static ZDKRequestProvider instance() {
			if (_instance != null)
				return _instance;
			_instance = new ZDKRequestProvider();
			return _instance;
		}

		override protected string GetAndroidClass() {
			return "com.zendesk.unity.providers.RequestProvider";
		}
		override protected string GetIOsMethodPrefix() {
			return "_zendeskRequestProvider";
		}

		/// <summary>
		/// Calls a request service to create a request on behalf of the end-user.
		/// </summary>
		/// <param name="createRequest">Request creation information</param>
		/// <param name="callback">callback invoked in response to remote API invokation</param>
		public static void CreateRequest(ZDKCreateRequest createRequest, Action<Hashtable,ZDKError> callback) {
			if (createRequest.Tags == null)
				createRequest.Tags = new string[0];
			if (createRequest.Attachments == null)
				createRequest.Attachments = new string[0];
			instance().Call("createRequest", callback, createRequest.Subject, createRequest.Description, createRequest.Email,
			                createRequest.Tags, createRequest.Tags.Length,
			                createRequest.Attachments, createRequest.Attachments.Length);
		}

		/// <summary>
		/// Gets all requests that user has opened.
		/// </summary>
		/// <param name="callback">callback invoked in response to remote API invokation</param>
		public static void GetAllRequests(Action<ArrayList,ZDKError> callback) {
			instance().Call("getAllRequests", callback);
		}

		/// <summary>
		/// Filters requests that user has opened by a status.
		/// </summary>
		/// <param name="status">A comma separated list of status to filter the results by</param>
		/// <param name="">The callback to invoke which will return a list of requests</param>
		public static void GetAllRequests(string status, Action<ArrayList,ZDKError> callback) {
			instance().Call("getRequestsByStatus", callback, status);
		}

		/// <summary>
		/// Gets all comments for a request.
		/// </summary>
		/// <param name="requestId">Id of a request</param>
		/// <param name="callback">callback invoked in response to remote API invokation</param>
		public static void GetComments(string requestId, Action<ArrayList,ZDKError> callback) {
			instance().Call("getCommentsWithRequestId", callback, requestId);
		}

		/// <summary>
		/// Gets a particular request.
		/// </summary>
		/// <param name="requestId">Id of a request</param>
		/// <param name="callback">callback invoked in response to remote API invokation</param>
		public static void GetRequest(string requestId, Action<Hashtable,ZDKError> callback) {
			instance().Call("getRequestWithId", callback, requestId);
		}

		/// <summary>
		/// Add a comment message to a request.
		/// </summary>
		/// <param name="comment">The text of the comment to create</param>
		/// <param name="requestId">Id of a request to add this comment to</param>
		/// <param name="callback">Callback that will deliver a ZDKComment</param>
		public static void AddComment(string comment, string requestId, Action<Hashtable,ZDKError> callback) {
			instance().Call("addComment", callback, comment, requestId);
		}

		/// <summary>
		/// Add a comment message to a request with attachments on behalf of the end-user.
		/// </summary>
		/// <param name="comment">The text of the comment to create</param>
		/// <param name="requestId">Id of a request to add this comment to</param>
		/// <param name="attachments">List of ZDKUploadResponse objects</param>
		/// <param name="callBack">Callback that will deliver a ZDKComment.</param>
		public static void AddComment(string comment, string requestId, string[] attachments, Action<Hashtable,ZDKError> callback) {
			if (attachments == null)
				attachments = new string[0];
			instance().Call("addCommentWithAttachments", callback, comment, requestId, attachments, attachments.Length);
		}

		/// <summary>
		/// Gets ticketforms from a  list of ids.
		/// </summary>
		/// <param name="ticketForms">List of ticket form ids to get.</param>
		/// <param name="callback">Callback that will deliver a List of Ticket Forms.</param>
		public static void GetTicketForms(long[] ticketForms, Action<ArrayList,ZDKError> callback) {
			if (ticketForms == null)
				ticketForms = new long[0];
			instance().Call("getTicketFormWithIds", callback, ticketForms, ticketForms.Length);
		}

		/// <summary>
    /// Gets request updates for this device.
    /// </summary>
    /// <param name="callback">Callback that will deliver the request updates.</param>
		public static void GetUpdatesForDevice(Action<Hashtable, ZDKError> callback) {
		    instance().Call("getUpdatesForDevice", callback);
  	}

		public static void MarkRequestAsRead(string requestId) {
			#if UNITY_IPHONE
			instance().DoIOS("markRequestAsRead", requestId);
			#elif UNITY_ANDROID
			instance().DoAndroid("markRequestAsRead", requestId);
			#endif
		}

		#if UNITY_IPHONE

		[DllImport("__Internal")]
		private static extern void _zendeskRequestProviderCreateRequest(string gameObjectName, string callbackId, string subject, string description, string email, string[] tags, int tagsLength, string[] attachments, int attachmentsLength);
		[DllImport("__Internal")]
		private static extern void _zendeskRequestProviderGetAllRequests(string gameObjectName, string callbackId);
		[DllImport("__Internal")]
		private static extern void _zendeskRequestProviderGetRequestsByStatus(string gameObjectName, string callbackId, string status);
		[DllImport("__Internal")]
		private static extern void _zendeskRequestProviderGetRequestWithId(string gameObjectName, string callbackId, string requestId);
		[DllImport("__Internal")]
		private static extern void _zendeskRequestProviderGetCommentsWithRequestId(string gameObjectName, string callbackId, string requestId);
		[DllImport("__Internal")]
		private static extern void _zendeskRequestProviderAddComment(string gameObjectName, string callbackId, string comment, string requestId);
		[DllImport("__Internal")]
		private static extern void _zendeskRequestProviderAddCommentWithAttachments(string gameObjectName, string callbackId, string comment, string requestId, string[] attachments, int attachmentsLength);
		[DllImport("__Internal")]
		private static extern void _zendeskRequestProviderGetTicketFormWithIds(string gameObjectName, string callbackId, long[] ticketFormsIds, int formsCount);
		[DllImport("__Internal")]
		private static extern void _zendeskRequestProviderGetUpdatesForDevice(string gameObjectName, string callbackId);
		[DllImport("__Internal")]
		private static extern void _zendeskRequestProviderMarkRequestAsRead(string requestId);


		#endif
	}
}
