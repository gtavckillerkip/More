using More.Basic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace More.UI
{
	public partial class SettingsBlockUI
	{
		#region Fields declarations
		private const string GAMEPLAY_SETTINGS = "Gameplay Settings";

		/// <summary>
		/// Настройки геймплея.
		/// </summary>
		private GameObject _gameplaySettings;
		#endregion

		/// <summary>
		/// Настройка настроек геймплея.
		/// </summary>
		private void SetupGameplaySettings()
		{
			_gameplaySettings = _content.transform.Find(GAMEPLAY_SETTINGS).gameObject;

			_gameplaySettings.SetActive(false);

			#region Difficulty
			var dropdown = _gameplaySettings.transform.Find("Difficulty").Find("Dropdown").gameObject.GetComponent<TMP_Dropdown>();

			var options = dropdown.options;
			options.Clear();
			options.Add(new TMP_Dropdown.OptionData() { text = Difficulty.Low.ToFriendlyString() });
			options.Add(new TMP_Dropdown.OptionData() { text = Difficulty.Medium.ToFriendlyString() });
			options.Add(new TMP_Dropdown.OptionData() { text = Difficulty.High.ToFriendlyString() });

			dropdown.value = options.FindIndex(e => e.text == SettingsSystem.Instance.CurrentSettings.Difficulty.ToFriendlyString());
			dropdown.captionText.text = options[dropdown.value].text;

			dropdown.onValueChanged.AddListener((int newValue) =>
			{
				var difficulty = DifficultyExtensions.FromFriendlyStringOrNull(options.ElementAt(newValue).text);
				if (difficulty == null)
				{
					return;
				}

				SettingsSystem.Instance.ChangeProperty(
					SettingsSystem.Instance.CurrentSettings.GetType().GetField(nameof(SettingsSystem.Instance.CurrentSettings.Difficulty)),
					difficulty.Value
					);

				dropdown.captionText.text = difficulty.Value.ToFriendlyString();
			});
			#endregion

			_gameplaySettings.transform.Find("Back To Main Buttons Button").gameObject.GetComponent<Button>().onClick.AddListener(BackToMainButtonsButtonClickedHandler);
			_gameplaySettings.transform.Find("To Controls Settings Buttons Button").gameObject.GetComponent<Button>().onClick.AddListener(() => ContentChildButtonClickedHandler(_controlsSettingsButtons));
		}
	}
}
