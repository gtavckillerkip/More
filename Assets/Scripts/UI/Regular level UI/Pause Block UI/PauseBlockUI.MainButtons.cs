using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace More.UI
{
	public partial class PauseBlockUI
	{
		#region Fields declarations
		private const string MAIN_BUTTONS = "Main Buttons";

		private const string RESUME_BUTTON = "Resume Button";
		private const string NEW_GAME_BUTTON = "New Game Button";
		private const string LOAD_GAME_BUTTON = "Load Game Button";
		private const string SETTINGS_BUTTON = "Settings Button";
		private const string QUIT_BUTTON = "Quit Button";

		/// <summary>
		/// Основные кнопки.
		/// </summary>
		private GameObject _mainButtons;

		/// <summary>
		/// Нажата кнопка продолжения игры.
		/// </summary>
		public event Action ResumeButtonClicked;

		/// <summary>
		/// Нажата кнопка настроек.
		/// </summary>
		public event Action SettingsButtonClicked;

		/// <summary>
		/// Нажата кнопка выхода.
		/// </summary>
		public event Action QuitButtonClicked;
		#endregion

		/// <summary>
		/// Настройка основных кнопок.
		/// </summary>
		private void SetupMainButtons()
		{
			_mainButtons = _content.transform.Find(MAIN_BUTTONS).gameObject;

			_mainButtons.SetActive(true);

			_mainButtons.transform.Find(RESUME_BUTTON).gameObject.GetComponent<Button>().onClick.AddListener(() => { ResumeButtonClicked?.Invoke(); });
			_mainButtons.transform.Find(NEW_GAME_BUTTON).gameObject.GetComponent<Button>().onClick.AddListener(() => { });
			_mainButtons.transform.Find(LOAD_GAME_BUTTON).gameObject.GetComponent<Button>().onClick.AddListener(() => { });
			_mainButtons.transform.Find(SETTINGS_BUTTON).gameObject.GetComponent<Button>().onClick.AddListener(() => { SettingsButtonClicked?.Invoke(); });
			_mainButtons.transform.Find(QUIT_BUTTON).gameObject.GetComponent<Button>().onClick.AddListener(() => { QuitButtonClicked?.Invoke(); });
		}
	}
}
