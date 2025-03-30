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
		private const string VIDEO_SETTINGS = "Video Settings";

		/// <summary>
		/// Настройки видео.
		/// </summary>
		private GameObject _videoSettings;
		#endregion

		/// <summary>
		/// Настройка настроек видео.
		/// </summary>
		private void SetupVideoSettings()
		{
			_videoSettings = _content.transform.Find(VIDEO_SETTINGS).gameObject;

			_videoSettings.SetActive(false);

			#region Resoluton
			var dropdown = _videoSettings.transform.Find("Resolution").Find("Dropdown").gameObject.GetComponent<TMP_Dropdown>();

			var options = dropdown.options;
			options.Clear();
			options.Add(new TMP_Dropdown.OptionData() { text = ResolutionIdentifier.HD.ToFriendlyString() });
			options.Add(new TMP_Dropdown.OptionData() { text = ResolutionIdentifier.FWXGA.ToFriendlyString() });
			options.Add(new TMP_Dropdown.OptionData() { text = ResolutionIdentifier.HDPlus.ToFriendlyString() });
			options.Add(new TMP_Dropdown.OptionData() { text = ResolutionIdentifier.FullHD.ToFriendlyString() });
			options.Add(new TMP_Dropdown.OptionData() { text = ResolutionIdentifier.QHD.ToFriendlyString() });
			options.Add(new TMP_Dropdown.OptionData() { text = ResolutionIdentifier._4KUHD.ToFriendlyString() });

			dropdown.value = options.FindIndex(e => e.text == SettingsSystem.Instance.CurrentSettings.ResolutionIdentifier.ToFriendlyString());
			dropdown.captionText.text = options[dropdown.value].text;

			dropdown.onValueChanged.AddListener((int newValue) =>
			{
				var resolutionIdentifier = ResolutionIdentifierExtensions.FromFriendlyStringOrNull(options.ElementAt(newValue).text);
				if (resolutionIdentifier == null)
				{
					return;
				}

				SettingsSystem.Instance.ChangeProperty(
					SettingsSystem.Instance.CurrentSettings.GetType().GetField(nameof(SettingsSystem.Instance.CurrentSettings.ResolutionIdentifier)),
					resolutionIdentifier.Value
					);

				dropdown.captionText.text = resolutionIdentifier.Value.ToFriendlyString();
			});
			#endregion

			#region FOV
			var fovSlider = _videoSettings.transform.Find("FOV").Find("Slider").gameObject.GetComponent<Slider>();

			fovSlider.minValue = 0.5f;
			fovSlider.maxValue = 2f;

			fovSlider.value = SettingsSystem.Instance.CurrentSettings.FOV;

			var fovField = _videoSettings.transform.Find("FOV").Find("InputField").gameObject.GetComponent<TMP_InputField>();

			fovField.text = string.Format("{0:f2}", fovSlider.value);

			fovSlider.onValueChanged.AddListener((float newValue) =>
			{
				SettingsSystem.Instance.ChangeProperty(
					SettingsSystem.Instance.CurrentSettings.GetType().GetField(nameof(SettingsSystem.Instance.CurrentSettings.FOV)),
					newValue
					);

				fovField.text = string.Format("{0:f2}", fovSlider.value);
			});

			fovField.onSubmit.AddListener((string newValue) =>
			{
				fovSlider.value = Mathf.Clamp(float.Parse(newValue), fovSlider.minValue, fovSlider.maxValue);
			});
			#endregion

			#region Interface scale
			var scaleSlider = _videoSettings.transform.Find("Interface Scale").Find("Slider").gameObject.GetComponent<Slider>();

			scaleSlider.minValue = 0.5f;
			scaleSlider.maxValue = 1f;

			scaleSlider.value = SettingsSystem.Instance.CurrentSettings.InterfaceScale;

			var scaleField = _videoSettings.transform.Find("Interface Scale").Find("InputField").gameObject.GetComponent<TMP_InputField>();

			scaleField.text = string.Format("{0:f2}", scaleSlider.value);

			scaleSlider.onValueChanged.AddListener((float newValue) =>
			{
				SettingsSystem.Instance.ChangeProperty(
					SettingsSystem.Instance.CurrentSettings.GetType().GetField(nameof(SettingsSystem.Instance.CurrentSettings.InterfaceScale)),
					newValue
					);

				scaleField.text = string.Format("{0:f2}", scaleSlider.value);
			});

			scaleField.onSubmit.AddListener((string newValue) =>
			{
				scaleSlider.value = Mathf.Clamp(float.Parse(newValue), scaleSlider.minValue, scaleSlider.maxValue);
			});
			#endregion

			_videoSettings.transform.Find("Back To Main Buttons Button").gameObject.GetComponent<Button>().onClick.AddListener(BackToMainButtonsButtonClickedHandler);
			_videoSettings.transform.Find("To Audio Settings Button").gameObject.GetComponent<Button>().onClick.AddListener(() => ContentChildButtonClickedHandler(_audioSettings));
		}
	}
}
