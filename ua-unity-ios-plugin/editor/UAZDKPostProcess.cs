using System;
using System.IO;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.XCodeEditorZendesk;

namespace ZendeskSDK {
	public class ZendeskUrbanAirshipPostProcess : MonoBehaviour {
		
		[PostProcessBuild(-10)]
		public static void OnPostProcessBuildCleanup(BuildTarget target, string path)
		{
			//
		}
		
		[PostProcessBuild(5001)]
		public static void OnPostProcessBuild(BuildTarget target, string path)
		{
			if(target == BuildTarget.iOS) {
				PostProcessBuild_iOS(path);
			}
			else if(target == BuildTarget.Android) {
				PostProcessBuild_Android(path);
			}
		}
		
		private static void PostProcessBuild_iOS(string path)
		{
			ProcessXCodeProject(path);
		}
		
		private static void PostProcessBuild_Android(string path)
		{
			//
		}
		
		private static void ProcessXCodeProject(string path)
		{
			UnityEditor.XCodeEditorZendesk.XCProject project = new UnityEditor.XCodeEditorZendesk.XCProject(path);

			// Find and run through all projmods files to patch the project
			string projModPath = System.IO.Path.Combine(Application.dataPath, "UrbanAirship/Editor");
			var files = System.IO.Directory.GetFiles(projModPath, "*.projmods", System.IO.SearchOption.AllDirectories);
			foreach (var file in files)
			{
				project.ApplyMod(file);
			}
			project.Save();
		}
	}
}
