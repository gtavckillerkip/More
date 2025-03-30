using More.Basic.Management;
using System;
using UnityEngine;

namespace More.UI
{
	/// <summary>
	/// Контроллер интерфейса игровой сцены.
	/// </summary>
	[Obsolete]
	public class RegularLevelUIController : UIController
	{
		#region Properties declarations
		/// <summary>
		/// Интерфейс игрового блока.
		/// </summary>
		public PlayingBlockUI PlayingBlockUI { get; private set; }

		/// <summary>
		/// Интерфейс блока паузы.
		/// </summary>
		public PauseBlockUI PauseBlockUI { get; private set; }

		/// <summary>
		/// Интерфейс блока настроек.
		/// </summary>
		public SettingsBlockUI SettingsBlockUI { get; private set; }
		#endregion

		public override void Setup()
		{
			PerformingUICanvas = UIManager.Instance.LevelUI.transform.Find("UI Objects Container").Find(PERFORMING_UI_CANVAS).GetComponent<Canvas>();

			PlayingBlockUI = PerformingUICanvas.gameObject.transform.Find("Playing Block").gameObject.GetComponent<PlayingBlockUI>();
			PlayingBlockUI.Setup();

			PauseBlockUI = PerformingUICanvas.gameObject.transform.Find("Pause Block").gameObject.GetComponent<PauseBlockUI>();
			PauseBlockUI.Setup();

			SettingsBlockUI = PerformingUICanvas.gameObject.transform.Find("Settings Block").gameObject.GetComponent<SettingsBlockUI>();
			SettingsBlockUI.Setup();

			//GameHandlerObs.Instance.GetManager<RegularLevelManager>().GamePaused += () =>
			//{
			//	PlayingBlockUI.Hide();
			//	PauseBlockUI.Show();
			//};
			//GameHandlerObs.Instance.GetManager<RegularLevelManager>().GameUnpaused += () =>
			//{
			//	PauseBlockUI.Hide();
			//	PlayingBlockUI.Show();
			//};

			//PlayingBlockUI.BlockShown += () =>
			//{
			//	Cursor.lockState = CursorLockMode.Locked;
			//	Cursor.visible = false;
			//};
			//PlayingBlockUI.BlockHidden += () =>
			//{
			//	Cursor.lockState = CursorLockMode.None;
			//	Cursor.visible = true;
			//};

			//PauseBlockUI.ResumeButtonClicked += () => GameHandlerObs.Instance.GetManager<RegularLevelManager>().SetPause(false);
			//PauseBlockUI.SettingsButtonClicked += () =>
			//{
			//	PauseBlockUI.Hide();
			//	SettingsBlockUI.Show();
			//};
			//PauseBlockUI.QuitButtonClicked += () =>
			//{
			//	Debug.Log("Quit Button clicked");

			//	// add check if sure

			//	Application.Quit();
			//};

			//SettingsBlockUI.BackButtonClicked += () =>
			//{
			//	SettingsBlockUI.Hide();
			//	PauseBlockUI.Show();
			//};

			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}
}
