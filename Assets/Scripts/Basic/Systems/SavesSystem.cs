using UnityEngine.SceneManagement;

/// <summary>
/// Класс для работы с сохранениями.
/// </summary>
public class SavesSystem : SingletonMB<SavesSystem>
{
	#region Loaders
	/// <summary>
	/// Загрузить игру с сохранения.
	/// </summary>
	/// <param name="saveName"> Имя сохранения. </param>
	public void Load(string saveName)
	{

	}

	/// <summary>
	/// Загрузить новую игру.
	/// </summary>
	/// <param name="sceneName"> Имя сцены. </param>
	public void LoadNew(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}

	/// <summary>
	/// Загрузить следующий (предыдущий) уровень.
	/// </summary>
	/// <param name="sceneName"> Имя сцены. </param>
	public void LoadNext(string sceneName)
	{

		// + autosave
		// mb via Save(saveName) or via separate method Autosave(saveName)
	}
	#endregion
	
	#region Savers
	/// <summary>
	/// Сохранить игру.
	/// </summary>
	/// <param name="saveName"> Имя сохранения. </param>
	public void Save(string saveName)
	{

	}
	#endregion
}
