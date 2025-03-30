using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace More.UI
{
	public partial class SettingsBlockUI
	{
		#region Fields declarations
		private const string CONTROLS_SETTINGS_BUTTONS = "Controls Settings Buttons";

		private const string MOUSE_BUTTON = "Mouse Settings Button";
		private const string KEYS_BUTTON = "Keys Settings Button";

		/// <summary>
		/// Кнопки перехода к настройкам управления.
		/// </summary>
		private GameObject _controlsSettingsButtons;

		/// <summary>
		/// Словарь кнопок перехода к настройкам управления.
		/// </summary>
		private Dictionary<string, GameObject> _controlsButtonNamesToGameObjects;
		#endregion

		/// <summary>
		/// Настройка кнопок перехода к настройкам управления.
		/// </summary>
		private void SetupControlsSettingsButtons()
		{
			_controlsSettingsButtons = _content.transform.Find(CONTROLS_SETTINGS_BUTTONS).gameObject;

			_controlsSettingsButtons.SetActive(false);

			_controlsButtonNamesToGameObjects = new()
			{
				[MOUSE_BUTTON] = _controlsSettingsButtons.transform.Find(MOUSE_BUTTON).gameObject,
				[KEYS_BUTTON] = _controlsSettingsButtons.transform.Find(KEYS_BUTTON).gameObject,
			};

			_controlsButtonNamesToGameObjects[MOUSE_BUTTON].GetComponent<Button>().onClick.AddListener(MouseSettingsButtonClickedHandler);
			_controlsButtonNamesToGameObjects[KEYS_BUTTON].GetComponent<Button>().onClick.AddListener(KeysSettingsButtonClickedHandler);

			_controlsSettingsButtons.transform.Find("Back To Main Buttons Button").gameObject.GetComponent<Button>().onClick.AddListener(BackToMainButtonsButtonClickedHandler);
			_controlsSettingsButtons.transform.Find("To Audio Settings Button").gameObject.GetComponent<Button>().onClick.AddListener(() => ContentChildButtonClickedHandler(_audioSettings));
			_controlsSettingsButtons.transform.Find("To Gameplay Settings Button").gameObject.GetComponent<Button>().onClick.AddListener(() => ContentChildButtonClickedHandler(_gameplaySettings));
		}

		#region Events handlers
		private void MouseSettingsButtonClickedHandler()
		{
			_mouseSettings.SetActive(true);

			ContentAnchoredPosition = (
				_contentInitialPosition.x - _mouseSettings.GetComponent<RectTransform>().anchoredPosition.x,
				_contentInitialPosition.y - _mouseSettings.GetComponent<RectTransform>().anchoredPosition.y
				);

			_focusedContentChild.SetActive(false);
			_focusedContentChild = _mouseSettings;
		}

		private void KeysSettingsButtonClickedHandler()
		{
			_keysSettings.SetActive(true);

			ContentAnchoredPosition = (
				_contentInitialPosition.x - _keysSettings.GetComponent<RectTransform>().anchoredPosition.x,
				_contentInitialPosition.y - _keysSettings.GetComponent<RectTransform>().anchoredPosition.y
				);

			_focusedContentChild.SetActive(false);
			_focusedContentChild = _keysSettings;
		}
		#endregion
	}
}
