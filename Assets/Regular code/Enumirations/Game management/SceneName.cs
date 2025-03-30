namespace More.Basic.Management
{
	/// <summary>
	/// Название сцены.
	/// </summary>
	public enum SceneName
	{
		Persistent,
		MainMenu,
		Level1
	}

	public static class SceneNameExtensions
	{
		public static string ToFriendlyString(this SceneName sceneName)
		{
			return sceneName switch
			{
				SceneName.Persistent => "Persistent",
				SceneName.MainMenu => "Main Menu",
				SceneName.Level1 => "Level 1",
				_ => "",
			};
		}
	}
}
