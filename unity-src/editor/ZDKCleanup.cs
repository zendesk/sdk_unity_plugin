using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;

public class ZDKCleanup : AssetPostprocessor
{
	private static string[] filesToRemove = {
		"Editor/ZDKAndroidSetupUI.cs",
		"Plugins/Zendesk/ZDKConfig.cs",
		"Plugins/Zendesk/ZDKDeviceInfo.cs",
		"Plugins/Zendesk/ZDKHelpCenter.cs",
		"Plugins/Zendesk/ZDKLogger.cs",
		"Plugins/Zendesk/ZDKRequests.cs",
		"Plugins/Zendesk/ZDKRMA.cs",
		"Plugins/Zendesk/ZDKStringUtil.cs",
		"Plugins/Zendesk/ZenExternal.cs",
		"Plugins/Zendesk/ZenJSON.cs",
		"Zendesk/Scripts/ZDKDispatcher.cs"
	};
	
	public static void Clean()
	{
		foreach(string fileName in filesToRemove) {
			if(File.Exists(System.IO.Path.Combine(Application.dataPath, fileName))) {
				AssetDatabase.DeleteAsset(System.IO.Path.Combine("Assets", fileName));
				Debug.Log("Removed legacy Zendesk file: " + fileName);
			}
		}

		AssetDatabase.Refresh();
	}
}
