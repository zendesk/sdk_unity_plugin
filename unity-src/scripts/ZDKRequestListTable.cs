using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace ZendeskSDK {

	// <summary>
	// UITableView containing the users request list. On init, the list will show a loading indicator 
	// and refresh the requests from the server, once loaded the list will reload itself and will notify 
	// that the table has been updated.
	// </summary>
	public class ZDKRequestListTable {

		public static IOSAppearance _appearance = new IOSAppearance("ZDKRequestListTable");

		private static string _logTag = "ZDKRequestListTable";
		public static void Log(string message) {
			if(Debug.isDebugBuild)
				Debug.Log(_logTag + "/" + message);
		}

		public static void SetCellSeparatorColor(ZenColor color) {
			_appearance.SetColor("cellSeparatorColor", color);
		}

		public static void SetTableBackgroundColor(ZenColor color) {
			_appearance.SetColor("tableBackgroundColor", color);
		}

		public static void SetBackgroundColor(ZenColor color) {
			_appearance.SetColor("backgroundColor", color);
		}

		public static void SetSectionIndexColor(ZenColor color) {
			_appearance.SetColor("sectionIndexColor", color);
		}

		public static void SetSectionIndexBackgroundColor(ZenColor color) {
			_appearance.SetColor("sectionIndexBackgroundColor", color);
		}

		public static void SetSectionIndexTrackingBackgroundColor(ZenColor color) {
			_appearance.SetColor("sectionIndexTrackingBackgroundColor", color);
		}

		public static void SetSeparatorColor(ZenColor color) {
			_appearance.SetColor("separatorColor", color);
		}

		public static void SetSeparatorInset(float top, float left, float bottom, float right) {
			_appearance.SetEdgeInsets("separatorInset", top, left, bottom, right);
		}
	}
}		