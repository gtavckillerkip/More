using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace More.UI
{
	public partial class MainBlockUI
	{
		#region Definitions
		/// <summary>
		/// Состояние основных кнопок.
		/// </summary>
		private enum MainButtonsBlockState
		{
			/// <summary>
			/// Видимые.
			/// </summary>
			Visible,

			/// <summary>
			/// Скрытые.
			/// </summary>
			Hidden
		}
		#endregion

		#region Fields declarations
		private const string MAIN_BUTTONS = "Main Buttons";
		private const string MORE_BUTTON = "More Button";

		private const string NEW_GAME_BUTTON = "New Game Button";
		private const string LOAD_GAME_BUTTON = "Load Game Button";
		private const string SETTINGS_BUTTON = "Settings Button";
		private const string QUIT_BUTTON = "Quit Button";

		/// <summary>
		/// Основные кнопки.
		/// </summary>
		private GameObject _mainButtons;

		/// <summary>
		/// Словарь основных кнопок.
		/// </summary>
		private Dictionary<string, GameObject> _buttonNamesToGameObjects;

		/// <summary>
		/// Нажата кнопка настроек.
		/// </summary>
		public event Action SettingsButtonClicked;
		#endregion

		#region Properties declarations
		/// <summary>
		/// Состояние блока основных кнопок.
		/// </summary>
		private MainButtonsBlockState MainButtonsState { get; set; }
		#endregion

		/// <summary>
		/// Настройка основных кнопок.
		/// </summary>
		private void SetupMainButtons()
		{
			_mainButtons = _content.transform.Find(MAIN_BUTTONS).gameObject;

			_mainButtons.SetActive(false);

			MainButtonsState = MainButtonsBlockState.Hidden;

			_buttonNamesToGameObjects = new()
			{
				[NEW_GAME_BUTTON] = _mainButtons.transform.Find(NEW_GAME_BUTTON).gameObject,
				[LOAD_GAME_BUTTON] = _mainButtons.transform.Find(LOAD_GAME_BUTTON).gameObject,
				[SETTINGS_BUTTON] = _mainButtons.transform.Find(SETTINGS_BUTTON).gameObject,
				[QUIT_BUTTON] = _mainButtons.transform.Find(QUIT_BUTTON).gameObject,
			};

			_buttonNamesToGameObjects[NEW_GAME_BUTTON].GetComponent<Button>().onClick.AddListener(NewGameButtonClickedHandler);
			_buttonNamesToGameObjects[LOAD_GAME_BUTTON].GetComponent<Button>().onClick.AddListener(LoadGameButtonClickedHandler);
			_buttonNamesToGameObjects[SETTINGS_BUTTON].GetComponent<Button>().onClick.AddListener(SettingsButtonClickedHandler);
			_buttonNamesToGameObjects[QUIT_BUTTON].GetComponent<Button>().onClick.AddListener(QuitButtonClickedHandler);

			_content.transform.Find(MORE_BUTTON).GetComponent<Button>().onClick.AddListener(MoreButtonClick);
		}

		#region Events handlers
		private void MoreButtonClick()
		{
			if (MainButtonsState == MainButtonsBlockState.Visible)
			{
				MainButtonsState = MainButtonsBlockState.Hidden;
			}
			else
			{
				MainButtonsState = MainButtonsBlockState.Visible;
			}

			_mainButtons.SetActive(MainButtonsState == MainButtonsBlockState.Visible);
		}

		private void NewGameButtonClickedHandler()
		{
			SavesSystem.Instance.LoadNew("Level1");
		}

		private void LoadGameButtonClickedHandler()
		{

		}

		private void SettingsButtonClickedHandler()
		{
			SettingsButtonClicked?.Invoke();
		}

		private void QuitButtonClickedHandler()
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Exit();
#endif
		}
#endregion
	}
}
