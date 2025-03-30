using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace More.UI
{
	public partial class SettingsBlockUI
	{
		#region Fields declarations
		private const string AUDIO_SETTINGS = "Audio Settings";

		/// <summary>
		/// Настройки аудио.
		/// </summary>
		private GameObject _audioSettings;
		#endregion

		/// <summary>
		/// Настройка настроек аудио.
		/// </summary>
		private void SetupAudioSettings()
		{
			_audioSettings = _content.transform.Find(AUDIO_SETTINGS).gameObject;

			_audioSettings.SetActive(false);

			#region Sound volume
			var soundSlider = _audioSettings.transform.Find("Sound Volume").Find("Slider").gameObject.GetComponent<Slider>();

			soundSlider.minValue = 0f;
			soundSlider.maxValue = 100f;

			soundSlider.value = SettingsSystem.Instance.CurrentSettings.SoundVolume;

			var soundField = _audioSettings.transform.Find("Sound Volume").Find("InputField").gameObject.GetComponent<TMP_InputField>();

			soundField.text = soundSlider.value.ToString();

			soundSlider.onValueChanged.AddListener((float newValue) =>
			{
				SettingsSystem.Instance.ChangeProperty(
					SettingsSystem.Instance.CurrentSettings.GetType().GetField(nameof(SettingsSystem.Instance.CurrentSettings.SoundVolume)),
					Convert.ToInt32(newValue)
					);

				soundField.text = soundSlider.value.ToString();
			});

			soundField.onSubmit.AddListener((string newValue) =>
			{
				soundSlider.value = Mathf.Clamp(int.Parse(newValue), soundSlider.minValue, soundSlider.maxValue);
			});
			#endregion

			#region Music volume
			var musicSlider = _audioSettings.transform.Find("Music Volume").Find("Slider").gameObject.GetComponent<Slider>();

			musicSlider.minValue = 0f;
			musicSlider.maxValue = 100f;

			musicSlider.value = SettingsSystem.Instance.CurrentSettings.MusicVolume;

			var musicField = _audioSettings.transform.Find("Music Volume").Find("InputField").gameObject.GetComponent<TMP_InputField>();

			musicField.text = musicSlider.value.ToString();

			musicSlider.onValueChanged.AddListener((float newValue) =>
			{
				SettingsSystem.Instance.ChangeProperty(
					SettingsSystem.Instance.CurrentSettings.GetType().GetField(nameof(SettingsSystem.Instance.CurrentSettings.MusicVolume)),
					Convert.ToInt32(newValue)
					);

				musicField.text = musicSlider.value.ToString();
			});

			musicField.onSubmit.AddListener((string newValue) =>
			{
				musicSlider.value = Mathf.Clamp(int.Parse(newValue), musicSlider.minValue, musicSlider.maxValue);
			});
			#endregion

			_audioSettings.transform.Find("Back To Main Buttons Button").gameObject.GetComponent<Button>().onClick.AddListener(BackToMainButtonsButtonClickedHandler);
			_audioSettings.transform.Find("To Video Settings Button").gameObject.GetComponent<Button>().onClick.AddListener(() => ContentChildButtonClickedHandler(_videoSettings));
			_audioSettings.transform.Find("To Controls Settings Buttons Button").gameObject.GetComponent<Button>().onClick.AddListener(() => ContentChildButtonClickedHandler(_controlsSettingsButtons));
		}
	}
}
