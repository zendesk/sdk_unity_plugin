using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace ZendeskSDK {
	
	public class ZDKUploadProvider : ZDKBaseComponent {
		
		private static ZDKUploadProvider _instance;
		
		private static ZDKUploadProvider instance() {
			if (_instance != null)
				return _instance;
			_instance = new ZDKUploadProvider();
			return _instance;
		}

		override protected string GetAndroidClass() {
			return "com.zendesk.unity.providers.UploadProvider";
		}
		override protected string GetIOsMethodPrefix() {
			return "_zendeskUploadProvider";
		}

		/// <summary>
		/// Upload an image to Zendesk, returns a token in the response that can be used to attach the file to a request
		/// </summary>
		/// <param name="attachment">The attachment to upload</param>
		/// <param name="filename">The file name you wan't to store the image as.</param>
		/// <param name="contentType">The content type of the data, i.e: "image/png".</param>
		/// <param name="callback">Block callback executed on request error or success.</param>
		public static void UploadAttachment(string attachment, string filename, string contentType, Action<Hashtable,ZDKError> callback) {
			instance().Call("uploadAttachment", callback, attachment, filename, contentType);
		}

		/// <summary>
		/// Delete an upload from Zendesk. Will only work if the upload has not been associated with a request/ticket.
		/// </summary>
		/// <param name="uploadToken">Upload token of file to delete</param>
		/// <param name="callback">Block callback executed on request error or success.</param>
		public static void DeleteUpload(string uploadToken, Action<Hashtable,ZDKError> callback) {
			instance().Call("deleteUpload", callback, uploadToken);
		}

		#if UNITY_IPHONE

		[DllImport("__Internal")]
		private static extern void _zendeskUploadProviderUploadAttachment(string gameObjectName, string callbackId, string attachment, string filename, string contentType);
		[DllImport("__Internal")]
		private static extern void _zendeskUploadProviderDeleteUpload(string gameObjectName, string callbackId, string uploadToken);

		#endif
	}
}
