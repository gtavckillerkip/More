using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace More.UI
{
	public partial class SettingsBlockUI
	{
		#region Definitions
		private enum ControlKeysBlock
		{
			Movement,
			Abilities,
			Miscellaneous
		}
		#endregion

		#region Fields declarations
		private const string KEYS_SETTINGS = "Keys Settings";

		private const string BUTTONS_LISTENER = "Buttons Listener";

		/// <summary>
		/// Настройки клавиш.
		/// </summary>
		private GameObject _keysSettings;

		private GameObject _buttonsListener;
		#endregion

		/// <summary>
		/// Настройка настроек клавиш.
		/// </summary>
		private void SetupKeysSettings()
		{
			_keysSettings = _content.transform.Find(KEYS_SETTINGS).gameObject;

			_keysSettings.SetActive(false);

			_buttonsListener = _keysSettings.transform.Find(BUTTONS_LISTENER).gameObject;

			SetupGameControlsSettingUpButtons(ControlKeysBlock.Movement);
			SetupGameControlsSettingUpButtons(ControlKeysBlock.Abilities);
			SetupGameControlsSettingUpButtons(ControlKeysBlock.Miscellaneous);

			_keysSettings.transform.Find("Back To Controls Buttons Button").gameObject.GetComponent<Button>().onClick.AddListener(() => ContentChildButtonClickedHandler(_controlsSettingsButtons));
			_keysSettings.transform.Find("To Mouse Settings Button").gameObject.GetComponent<Button>().onClick.AddListener(MouseSettingsButtonClickedHandler);
		}

		private void SetupGameControlsSettingUpButtons(ControlKeysBlock controlsBlock)
		{
			var controlsBlockTransfrom = controlsBlock switch
			{
				ControlKeysBlock.Movement =>		_keysSettings.transform.Find("Scroll View/Viewport/Content/Movement"),
				ControlKeysBlock.Abilities =>		_keysSettings.transform.Find("Scroll View/Viewport/Content/Abilities"),
				ControlKeysBlock.Miscellaneous =>	_keysSettings.transform.Find("Scroll View/Viewport/Content/Miscellaneous"),
				_ => null
			};

			var controlsNamesToKeyCodesAndNames = controlsBlock switch
			{
				ControlKeysBlock.Movement => new Dictionary<string, (KeyCode KeyCode, string FieldName)>()
				{
					["Move Forward"] =				(SettingsSystem.Instance.CurrentSettings.MoveForward, nameof(SettingsSystem.Instance.CurrentSettings.MoveForward)),
					["Move Backward"] =				(SettingsSystem.Instance.CurrentSettings.MoveBackward, nameof(SettingsSystem.Instance.CurrentSettings.MoveBackward)),
					["Move Left"] =					(SettingsSystem.Instance.CurrentSettings.MoveLeft, nameof(SettingsSystem.Instance.CurrentSettings.MoveLeft)),
					["Move Right"] =				(SettingsSystem.Instance.CurrentSettings.MoveRight, nameof(SettingsSystem.Instance.CurrentSettings.MoveRight)),
					["Jump"] =						(SettingsSystem.Instance.CurrentSettings.Jump, nameof(SettingsSystem.Instance.CurrentSettings.Jump)),
					["Duck"] =						(SettingsSystem.Instance.CurrentSettings.Duck, nameof(SettingsSystem.Instance.CurrentSettings.Duck)),
					["Slowdown"] =					(SettingsSystem.Instance.CurrentSettings.Slowdown, nameof(SettingsSystem.Instance.CurrentSettings.Slowdown)),
				},

				ControlKeysBlock.Abilities => new Dictionary<string, (KeyCode KeyCode, string FieldName)>()
				{
					["Primary Attack"] =			(SettingsSystem.Instance.CurrentSettings.PrimaryAttack, nameof(SettingsSystem.Instance.CurrentSettings.PrimaryAttack)),
					["Secondary Attack"] =			(SettingsSystem.Instance.CurrentSettings.SecondaryAttack, nameof(SettingsSystem.Instance.CurrentSettings.SecondaryAttack)),
					["Ability 1"] =					(SettingsSystem.Instance.CurrentSettings.Ability1, nameof(SettingsSystem.Instance.CurrentSettings.Ability1)),
					["Ability 2"] =					(SettingsSystem.Instance.CurrentSettings.Ability2, nameof(SettingsSystem.Instance.CurrentSettings.Ability2)),
					["Ability 3"] =					(SettingsSystem.Instance.CurrentSettings.Ability3, nameof(SettingsSystem.Instance.CurrentSettings.Ability3)),
				},

				ControlKeysBlock.Miscellaneous => new Dictionary<string, (KeyCode KeyCode, string FieldName)>()
				{
					["Use"] =						(SettingsSystem.Instance.CurrentSettings.Use, nameof(SettingsSystem.Instance.CurrentSettings.Use)),
					["Fullscreen Mode Switch"] =	(SettingsSystem.Instance.CurrentSettings.FullScreenSwitch, nameof(SettingsSystem.Instance.CurrentSettings.FullScreenSwitch)),
				},

				_ => null
			};

			for (int i = 0; i < controlsNamesToKeyCodesAndNames.Count; i++)
			{
				var element = controlsNamesToKeyCodesAndNames.ElementAt(i);

				var button = controlsBlockTransfrom.Find(element.Key).Find("Button").gameObject;

				button.GetComponent<Button>().onClick.AddListener(() =>
				{
					if (_buttonsListener.activeSelf == false)
					{
						_buttonsListener.GetComponent<ButtonsListener>().ButtonObject = button;
						_buttonsListener.GetComponent<ButtonsListener>().FieldName = element.Value.FieldName;
						_buttonsListener.GetComponent<ButtonsListener>().TextUpdater = ButtonTextUpdate;
						_buttonsListener.SetActive(true);
					}
				});

				ButtonTextUpdate(button, element.Value.KeyCode);
			}
		}

		private void ButtonTextUpdate(GameObject buttonObject, KeyCode kc)
		{
			buttonObject.transform.Find("Text").gameObject.GetComponent<TMP_Text>().text = kc switch
			{
				KeyCode.A => "A",
				KeyCode.B => "B",
				KeyCode.C => "C",
				KeyCode.D => "D",
				KeyCode.E => "E",
				KeyCode.F => "F",
				KeyCode.G => "G",
				KeyCode.H => "H",
				KeyCode.I => "I",
				KeyCode.J => "J",
				KeyCode.K => "K",
				KeyCode.L => "L",
				KeyCode.M => "M",
				KeyCode.N => "N",
				KeyCode.O => "O",
				KeyCode.P => "P",
				KeyCode.Q => "Q",
				KeyCode.R => "R",
				KeyCode.S => "S",
				KeyCode.T => "T",
				KeyCode.U => "U",
				KeyCode.V => "V",
				KeyCode.W => "W",
				KeyCode.X => "X",
				KeyCode.Y => "Y",
				KeyCode.Z => "Z",
				KeyCode.Alpha0 => "0",
				KeyCode.Alpha1 => "1",
				KeyCode.Alpha2 => "2",
				KeyCode.Alpha3 => "3",
				KeyCode.Alpha4 => "4",
				KeyCode.Alpha5 => "5",
				KeyCode.Alpha6 => "6",
				KeyCode.Alpha7 => "7",
				KeyCode.Alpha8 => "8",
				KeyCode.Alpha9 => "9",
				KeyCode.Minus => "-",
				KeyCode.Equals => "=",
				KeyCode.Tilde => "`",
				KeyCode.LeftShift => "LShift",
				KeyCode.RightShift => "RShift",
				KeyCode.LeftControl => "LCtrl",
				KeyCode.RightControl => "RCtrl",
				KeyCode.LeftAlt => "LAlt",
				KeyCode.RightAlt => "RAlt",
				KeyCode.Space => "Space",
				KeyCode.Tab => "Tab",
				KeyCode.LeftBracket => "[",
				KeyCode.RightBracket => "]",
				KeyCode.Semicolon => ";",
				KeyCode.Quote => "'",
				KeyCode.Comma => ",",
				KeyCode.Period => ".",
				KeyCode.Slash => "/",
				KeyCode.Backslash => "\\",
				KeyCode.CapsLock => "CLock",
				KeyCode.Backspace => "BSpace",
				KeyCode.UpArrow => "↑",
				KeyCode.DownArrow => "↓",
				KeyCode.LeftArrow => "←",
				KeyCode.RightArrow => "→",
				KeyCode.Insert => "Ins",
				KeyCode.Delete => "Del",
				KeyCode.Home => "Home",
				KeyCode.End => "End",
				KeyCode.PageUp => "PgUp",
				KeyCode.PageDown => "PgDn",

				KeyCode.Mouse0 => "LMB",
				KeyCode.Mouse1 => "RMB",
				KeyCode.Mouse2 => "MMB",
				KeyCode.Mouse3 => "Mouse4",
				KeyCode.Mouse4 => "Mouse5",
				KeyCode.Mouse5 => "Mouse6",
				KeyCode.Mouse6 => "Mouse7",

				_ => ""
			};
		}
	}
}
