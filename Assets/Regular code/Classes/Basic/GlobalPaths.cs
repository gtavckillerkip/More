using UnityEngine;

namespace More.Basic
{
	public static class GlobalPaths
	{
#if DEBUG
		public static readonly string SettingsFilePath = Application.streamingAssetsPath + "/Settings/";
#else
		public static readonly string SettingsFilePath = Application.dataPath + "/Settings/";
#endif
	}
}
