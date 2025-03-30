using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace More.UI
{
	public partial class SettingsBlockUI
	{
		#region Fields declarations
		private const string MOUSE_SETTINGS = "Mouse Settings";

		/// <summary>
		/// Настройки мыши.
		/// </summary>
		private GameObject _mouseSettings;
		#endregion

		/// <summary>
		/// Настройка настроек мыши.
		/// </summary>
		private void SetupMouseSettings()
		{
			_mouseSettings = _content.transform.Find(MOUSE_SETTINGS).gameObject;

			_mouseSettings.SetActive(false);

			#region Mouse sensivity
			var sensivitySlider = _mouseSettings.transform.Find("Sensivity").Find("Slider").gameObject.GetComponent<Slider>();

			sensivitySlider.minValue = 0f;
			sensivitySlider.maxValue = 10f;

			sensivitySlider.value = SettingsSystem.Instance.CurrentSettings.MouseSensivity;

			var sensivityField = _mouseSettings.transform.Find("Sensivity").Find("InputField").gameObject.GetComponent<TMP_InputField>();

			sensivityField.text = string.Format("{0:f2}", sensivitySlider.value);

			sensivitySlider.onValueChanged.AddListener((float newValue) =>
			{
				SettingsSystem.Instance.ChangeProperty(
					SettingsSystem.Instance.CurrentSettings.GetType().GetField(nameof(SettingsSystem.Instance.CurrentSettings.MouseSensivity)),
					newValue
					);

				sensivityField.text = string.Format("{0:f2}", sensivitySlider.value);
			});

			sensivityField.onSubmit.AddListener((string newValue) =>
			{
				sensivitySlider.value = Mathf.Clamp(float.Parse(newValue), sensivitySlider.minValue, sensivitySlider.maxValue);
			});
			#endregion

			#region Mouse scope sensivity scale
			var scaleSlider = _mouseSettings.transform.Find("Scope Sensivity Scale").Find("Slider").gameObject.GetComponent<Slider>();

			scaleSlider.minValue = 0f;
			scaleSlider.maxValue = 1f;

			scaleSlider.value = SettingsSystem.Instance.CurrentSettings.MouseScopeSensivityScale;

			var scaleField = _mouseSettings.transform.Find("Scope Sensivity Scale").Find("InputField").gameObject.GetComponent<TMP_InputField>();

			scaleField.text = string.Format("{0:f2}", scaleSlider.value);

			scaleSlider.onValueChanged.AddListener((float newValue) =>
			{
				SettingsSystem.Instance.ChangeProperty(
					SettingsSystem.Instance.CurrentSettings.GetType().GetField(nameof(SettingsSystem.Instance.CurrentSettings.MouseScopeSensivityScale)),
					newValue
					);

				scaleField.text = string.Format("{0:f2}", scaleSlider.value);
			});

			scaleField.onSubmit.AddListener((string newValue) =>
			{
				scaleSlider.value = Mathf.Clamp(float.Parse(newValue), scaleSlider.minValue, scaleSlider.maxValue);
			});
			#endregion

			#region Mouse inversion
			var inversionToggle = _mouseSettings.transform.Find("Inversion").Find("Toggle").gameObject.GetComponent<Toggle>();

			inversionToggle.isOn = SettingsSystem.Instance.CurrentSettings.MouseInversion;

			inversionToggle.onValueChanged.AddListener((bool newValue) =>
			{
				SettingsSystem.Instance.ChangeProperty(
					SettingsSystem.Instance.CurrentSettings.GetType().GetField(nameof(SettingsSystem.Instance.CurrentSettings.MouseInversion)),
					newValue
					);
			});
			#endregion

			_mouseSettings.transform.Find("Back To Controls Buttons Button").gameObject.GetComponent<Button>().onClick.AddListener(() => ContentChildButtonClickedHandler(_controlsSettingsButtons));
			_mouseSettings.transform.Find("To Keys Settings Button").gameObject.GetComponent<Button>().onClick.AddListener(KeysSettingsButtonClickedHandler);
		}
	}
}
