using More.UI;
using UnityEngine;

namespace More.Basic.Management
{
	/// <summary>
	/// Менеджер интерфейса.
	/// </summary>
	public class UIManager : SingletonMB<UIManager>
	{
		#region Properties declarations
		/// <summary>
		/// Контейнер с объектами интерфейса.
		/// </summary>
		public GameObject UIContainer { get; private set; }

		/// <summary>
		/// Интерфейс главного меню.
		/// </summary>
		public GameObject MainMenuUI { get; private set; }

		/// <summary>
		/// Интерфейс игрового уровня.
		/// </summary>
		public GameObject LevelUI { get; private set; }

		/// <summary>
		/// Машина состояний интерфейса.
		/// </summary>
		public UIStateMachine UIStateMachine { get; private set; }
		#endregion

		private void Start()
		{
			UIContainer = GameObject.Find("/UI");

			MainMenuUI = GameObject.Instantiate(ResourcesSystem.Instance.MainMenuUIPrefab, UIContainer.transform);
			LevelUI = GameObject.Instantiate(ResourcesSystem.Instance.LevelUIPrefab, UIContainer.transform);

			MainMenuUI.GetComponent<UIController>().Setup(); // obsolete
			LevelUI.GetComponent<UIController>().Setup(); // obsolete
														  // obsolete above should be removed after switching to ui toolkit

			UIStateMachine = new(this);
			UIStateMachine.RunOnGameStart();
		}
	}
}
