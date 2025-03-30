using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace More.UI
{
	public partial class SettingsBlockUI
	{
		#region Fields declarations
		private const string MAIN_BUTTONS = "Main Buttons";

		private const string BACK_BUTTON = "Back Button";
		private const string VIDEO_BUTTON = "Video Settings Button";
		private const string AUDIO_BUTTON = "Audio Settings Button";
		private const string CONTROLS_BUTTON = "Controls Settings Button";
		private const string GAMEPLAY_BUTTON = "Gameplay Settings Button";

		/// <summary>
		/// Основные кнопки.
		/// </summary>
		private GameObject _mainButtons;

		/// <summary>
		/// Словарь основных кнопок.
		/// </summary>
		private Dictionary<string, GameObject> _mainButtonNamesToGameObjects;

		/// <summary>
		/// Нажата кнопка возврата из настроек.
		/// </summary>
		public event Action BackButtonClicked;
		#endregion

		/// <summary>
		/// Настройка основных кнопок.
		/// </summary>
		private void SetupMainButtons()
		{
			_mainButtons = _content.transform.Find(MAIN_BUTTONS).gameObject;

			_mainButtons.SetActive(true);

			_mainButtonNamesToGameObjects = new()
			{
				[BACK_BUTTON] = _titling.transform.Find(BACK_BUTTON).gameObject,
				[VIDEO_BUTTON] = _mainButtons.transform.Find(VIDEO_BUTTON).gameObject,
				[AUDIO_BUTTON] = _mainButtons.transform.Find(AUDIO_BUTTON).gameObject,
				[CONTROLS_BUTTON] = _mainButtons.transform.Find(CONTROLS_BUTTON).gameObject,
				[GAMEPLAY_BUTTON] = _mainButtons.transform.Find(GAMEPLAY_BUTTON).gameObject,
			};

			_mainButtonNamesToGameObjects[BACK_BUTTON].GetComponent<Button>().onClick.AddListener(BackButtonClickedHandler);
			_mainButtonNamesToGameObjects[VIDEO_BUTTON].GetComponent<Button>().onClick.AddListener(() => ContentChildButtonClickedHandler(_videoSettings));
			_mainButtonNamesToGameObjects[AUDIO_BUTTON].GetComponent<Button>().onClick.AddListener(() => ContentChildButtonClickedHandler(_audioSettings));
			_mainButtonNamesToGameObjects[CONTROLS_BUTTON].GetComponent<Button>().onClick.AddListener(() => ContentChildButtonClickedHandler(_controlsSettingsButtons));
			_mainButtonNamesToGameObjects[GAMEPLAY_BUTTON].GetComponent<Button>().onClick.AddListener(() => ContentChildButtonClickedHandler(_gameplaySettings));
		}

		#region Events handlers
		private void BackButtonClickedHandler()
		{
			SettingsSystem.Instance.Save();

			_mainButtons.SetActive(true);

			_contentAnchoredPosition = _contentInitialPosition;
			_content.GetComponent<RectTransform>().anchoredPosition = new Vector2(_contentInitialPosition.x, _contentInitialPosition.y);

			if (_focusedContentChild != _mainButtons)
			{
				_focusedContentChild.SetActive(false);
				_focusedContentChild = _mainButtons;
			}

			BackButtonClicked?.Invoke();
		}

		private void ContentChildButtonClickedHandler(GameObject contentChild)
		{
			contentChild.SetActive(true);

			ContentAnchoredPosition = (
				_contentInitialPosition.x - contentChild.GetComponent<RectTransform>().anchoredPosition.x,
				_contentInitialPosition.y - contentChild.GetComponent<RectTransform>().anchoredPosition.y
				);

			_focusedContentChild.SetActive(false);
			_focusedContentChild = contentChild;
		}

		private void BackToMainButtonsButtonClickedHandler()
		{
			_mainButtons.SetActive(true);

			ContentAnchoredPosition = _contentInitialPosition;

			_focusedContentChild.SetActive(false);
			_focusedContentChild = _mainButtons;
		}
		#endregion
	}
}
