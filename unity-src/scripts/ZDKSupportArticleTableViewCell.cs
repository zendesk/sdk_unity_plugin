using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace ZendeskSDK {
	
	public class ZDKSupportArticleTableViewCell {

		public static IOSAppearance _appearance = new IOSAppearance("ZDKSupportArticleTableViewCell");

		private static string _logTag = "ZDKSupportArticleTableViewCell";
		public static void Log(string message) {
			if(Debug.isDebugBuild)
				Debug.Log(_logTag + "/" + message);
		}

		public static void SetArticleParentsLabelFont(string fontName, float size) {
			_appearance.SetFont("articleParentsLabelFont", fontName, size);
		}

		public static void SetTitleLabelFont(string fontName, float size) {
			_appearance.SetFont("titleLabelFont", fontName, size);
		}

		public static void SetBackgroundColor(ZenColor color) {
			_appearance.SetColor("backgroundColor", color);
		}

		public static void SetArticleParentsLabelColor(ZenColor color) {
			_appearance.SetColor("articleParentsLabelColor", color);
		}

		public static void SetArticleParentsLabelBackground(ZenColor color) {
			_appearance.SetColor("articleParentsLabelBackground", color);
		}

		public static void SetTitleLabelColor(ZenColor color) {
			_appearance.SetColor("titleLabelColor", color);
		}

		public static void SetTitleLabelBackground(ZenColor color) {
			_appearance.SetColor("titleLabelBackground", color);
		}

		public static void SetSeparatorInset(float top, float left, float bottom, float right) {
			_appearance.SetEdgeInsets("separatorInset", top, left, bottom, right);
		}
	}
}		