using More.Basic.Management;
using More.Settings_system;
using System;
using System.Collections.Generic;
using Unity.Properties;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace More.Workers
{
	/// <summary>
	/// Вспомогательный класс для работы с UI.
	/// </summary>
	public static class UIWorker
	{
		/// <summary>
		/// Контейнер с функциями для работы с UI главного меню.
		/// </summary>
		public static class MainMenuUI
		{
			#region Definitions
			private enum SettingsElementType
			{
				Enum,
				SliderFloat,
				Toggle,
				OneSymbol
			}
			#endregion

			#region Fields declarations
			private static readonly TypeConverter<KeyCode, string> _fromKeyCodeToStringConverter = new((ref KeyCode kc) => kc.ToFriendlyString());
			private static readonly TypeConverter<string, KeyCode> _fromStringToKeyCodeConverter = new((ref string s) => s.ToKeyCode());

			private static readonly Dictionary<string, (string LabelText, PropertyPath SettingsPropertyPath, SettingsElementType SettingsElementType)>
				_settingsElementNameToVitalFeatures = new()
			{
				["Resolution"] =		("Resolution",				PropertyPath.FromName(nameof(Settings.ResolutionIdentifier)),		SettingsElementType.Enum),
				["FOV"] =				("FOV",						PropertyPath.FromName(nameof(Settings.FOV)),						SettingsElementType.SliderFloat),
				["InterfaceScale"] =	("Interface scale",			PropertyPath.FromName(nameof(Settings.InterfaceScale)),				SettingsElementType.SliderFloat),

				["SoundVolume"] =		("Sound volume",			PropertyPath.FromName(nameof(Settings.SoundVolume)),				SettingsElementType.SliderFloat),
				["MusicVolume"] =		("Music volume",			PropertyPath.FromName(nameof(Settings.MusicVolume)),				SettingsElementType.SliderFloat),

				["Sensivity"] =			("Sensivity",				PropertyPath.FromName(nameof(Settings.MouseSensivity)),				SettingsElementType.SliderFloat),
				["ScopeSensivity"] =	("Scope sensivity\nscale",	PropertyPath.FromName(nameof(Settings.MouseScopeSensivityScale)),	SettingsElementType.SliderFloat),
				["Inversion"] =			("Inversion",				PropertyPath.FromName(nameof(Settings.MouseInversion)),				SettingsElementType.Toggle),

				["MoveForward"] =		("Move forward",			PropertyPath.FromName(nameof(Settings.MoveForward)),				SettingsElementType.OneSymbol),
				["MoveBackward"] =		("Move backward",			PropertyPath.FromName(nameof(Settings.MoveBackward)),				SettingsElementType.OneSymbol),
				["MoveLeft"] =			("Move left",				PropertyPath.FromName(nameof(Settings.MoveLeft)),					SettingsElementType.OneSymbol),
				["MoveRight"] =			("Move right",				PropertyPath.FromName(nameof(Settings.MoveRight)),					SettingsElementType.OneSymbol),
				["Jump"] =				("Jump",					PropertyPath.FromName(nameof(Settings.Jump)),						SettingsElementType.OneSymbol),
				["Duck"] =				("Duck",					PropertyPath.FromName(nameof(Settings.Duck)),						SettingsElementType.OneSymbol),
				["Slowdown"] =			("Slowdown",				PropertyPath.FromName(nameof(Settings.Slowdown)),					SettingsElementType.OneSymbol),

				["PrimaryAttack"] =		("Primary attack",			PropertyPath.FromName(nameof(Settings.PrimaryAttack)),				SettingsElementType.OneSymbol),
				["SecondaryAttack"] =	("Secondary attack",		PropertyPath.FromName(nameof(Settings.SecondaryAttack)),			SettingsElementType.OneSymbol),
				["Ability1"] =			("Ability 1",				PropertyPath.FromName(nameof(Settings.Ability1)),					SettingsElementType.OneSymbol),
				["Ability2"] =			("Ability 2",				PropertyPath.FromName(nameof(Settings.Ability2)),					SettingsElementType.OneSymbol),
				["Ability3"] =			("Ability 3",				PropertyPath.FromName(nameof(Settings.Ability3)),					SettingsElementType.OneSymbol),

				["Use"] =				("Use",						PropertyPath.FromName(nameof(Settings.Use)),						SettingsElementType.OneSymbol),
				["Quests"] =			("Quests",					PropertyPath.FromName(nameof(Settings.Quests)),						SettingsElementType.OneSymbol),
				["Fullscreen"] =		("Fullscreen",				PropertyPath.FromName(nameof(Settings.FullScreenSwitch)),			SettingsElementType.OneSymbol),

				["Difficulty"] =		("Difficulty",				PropertyPath.FromName(nameof(Settings.Difficulty)),					SettingsElementType.Enum),
			};
			#endregion

			/// <summary>
			/// Настройка.
			/// </summary>
			public static void Setup()
			{
				var document = UIManager.Instance.MainMenuUI.GetComponent<UIDocument>();

				#region Basic setups
				var root = document.rootVisualElement?.Q("Root");

				root.dataSource = SettingsSystem.Instance.CurrentSettings;

				#region Submenus
				var submenus = root?.Q("Submenus");

				#region Game submenu
				var gameSubmenu = submenus?.Q("GameSubmenu");
				(gameSubmenu?.style).display = DisplayStyle.None;
				#endregion

				#region Settings submenu
				var settingsSubmenu = submenus?.Q("SettingsSubmenu");
				(settingsSubmenu?.style).display = DisplayStyle.None;

				#region Category contents
				var settingsElements = new List<VisualElement>();

				#region Video settings
				settingsElements.Add(settingsSubmenu?.Q("Resolution"));
				settingsElements.Add(settingsSubmenu?.Q("FOV"));
				settingsElements.Add(settingsSubmenu?.Q("InterfaceScale"));
				#endregion

				#region Audio settings
				settingsElements.Add(settingsSubmenu?.Q("SoundVolume"));
				settingsElements.Add(settingsSubmenu?.Q("MusicVolume"));
				#endregion

				#region Controls settings
				#region Mouse settings
				var mouseSettings = settingsSubmenu?.Q("MouseSettings");
				(mouseSettings?.Q<Label>()).text = "Mouse settings";

				settingsElements.Add(mouseSettings?.Q("Sensivity"));
				settingsElements.Add(mouseSettings?.Q("ScopeSensivity"));
				settingsElements.Add(mouseSettings?.Q("Inversion"));
				#endregion

				#region Movement settings
				var movementSettings = settingsSubmenu?.Q("MovementSettings");
				(movementSettings?.Q<Label>()).text = "Movement settings";

				settingsElements.Add(movementSettings?.Q("MoveForward"));
				settingsElements.Add(movementSettings?.Q("MoveBackward"));
				settingsElements.Add(movementSettings?.Q("MoveLeft"));
				settingsElements.Add(movementSettings?.Q("MoveRight"));
				settingsElements.Add(movementSettings?.Q("Jump"));
				settingsElements.Add(movementSettings?.Q("Duck"));
				settingsElements.Add(movementSettings?.Q("Slowdown"));
				#endregion

				#region Abilities
				var abilities = settingsSubmenu?.Q("Abilities");
				(abilities?.Q<Label>()).text = "Abilities";

				settingsElements.Add(abilities?.Q("PrimaryAttack"));
				settingsElements.Add(abilities?.Q("SecondaryAttack"));
				settingsElements.Add(abilities?.Q("Ability1"));
				settingsElements.Add(abilities?.Q("Ability2"));
				settingsElements.Add(abilities?.Q("Ability3"));
				#endregion

				#region Miscellaneous
				var miscellaneous = settingsSubmenu?.Q("Miscellaneous");
				(miscellaneous?.Q<Label>()).text = "Miscellaneous";

				settingsElements.Add(miscellaneous?.Q("Use"));
				settingsElements.Add(miscellaneous?.Q("Quests"));
				settingsElements.Add(miscellaneous?.Q("FullScreen"));
				#endregion
				#endregion

				#region Gameplay settings
				settingsElements.Add(settingsSubmenu?.Q("Difficulty"));

				var difficulty = settingsSubmenu?.Q("Difficulty");
				(difficulty?.Q<Label>()).text = _settingsElementNameToVitalFeatures["Difficulty"].LabelText;
				var difficultyField = difficulty?.Q<EnumField>();
				difficultyField?.Init(Difficulty.Low);
				difficultyField?.SetBinding(nameof(EnumField.value),
					new DataBinding() { bindingMode = BindingMode.TwoWay, dataSourcePath = PropertyPath.FromName(nameof(Settings.Difficulty)) });
				#endregion

				foreach (var element in settingsElements)
				{
					SetupSettingsElement(element);
				}
				#endregion
				#endregion
				#endregion
				#endregion

				#region Callbacks
				#region Submenu buttons
				root?.Q("GameButton")?.RegisterCallback<MouseUpEvent>(mue =>
				{
					
				});
				root?.Q("SettingsButton")?.RegisterCallback<MouseUpEvent>(mue => { });
				root?.Q("QuitButton")?.RegisterCallback<MouseUpEvent>(mue => { });
				#endregion

				#region Submenus
				#region Game submenu
				#region Category buttons
				gameSubmenu?.Q("NewGameButton")?.RegisterCallback<MouseUpEvent>(mue => { });
				gameSubmenu?.Q("LoadGameButton")?.RegisterCallback<MouseUpEvent>(mue => { });
				#endregion
				#endregion

				#region Settings submenu
				#region Category buttons
				settingsSubmenu?.Q("VideoSettingsButton")?.RegisterCallback<MouseUpEvent>(mue => { });
				settingsSubmenu?.Q("AudioSettingsButton")?.RegisterCallback<MouseUpEvent>(mue => { });
				settingsSubmenu?.Q("ControlsSettingsButton")?.RegisterCallback<MouseUpEvent>(mue => { });
				settingsSubmenu?.Q("GameplaySettingsButton")?.RegisterCallback<MouseUpEvent>(mue => { });
				#endregion
				#endregion
				#endregion
				#endregion
			}

			private static void SetupSettingsElement(VisualElement element)
			{
				(element.Q<Label>()).text = _settingsElementNameToVitalFeatures[element.name].LabelText;

				switch (_settingsElementNameToVitalFeatures[element.name].SettingsElementType)
				{
					case SettingsElementType.Enum:
						var enumField = element.Q<EnumField>();
						enumField?.SetBinding(nameof(EnumField.value),
							new DataBinding { bindingMode = BindingMode.TwoWay, dataSourcePath = _settingsElementNameToVitalFeatures[element.name].SettingsPropertyPath });
						break;

					case SettingsElementType.SliderFloat:
						var slider = element.Q<Slider>();
						var floatField = element.Q<FloatField>();
						slider.SetBinding(nameof(Slider.value),
							new DataBinding { bindingMode = BindingMode.TwoWay, dataSourcePath = _settingsElementNameToVitalFeatures[element.name].SettingsPropertyPath });
						floatField.SetBinding(nameof(FloatField.value),
							new DataBinding { bindingMode = BindingMode.TwoWay, dataSourcePath = _settingsElementNameToVitalFeatures[element.name].SettingsPropertyPath });
						break;

					case SettingsElementType.Toggle:
						var toggle = element.Q<Toggle>();
						toggle.SetBinding(nameof(Toggle.value),
							new DataBinding { bindingMode = BindingMode.TwoWay, dataSourcePath = _settingsElementNameToVitalFeatures[element.name].SettingsPropertyPath });
						break;

					case SettingsElementType.OneSymbol:
						var textField = element.Q<TextField>();
						var binding = new DataBinding
						{
							bindingMode = BindingMode.TwoWay,
							dataSourcePath = _settingsElementNameToVitalFeatures[element.name].SettingsPropertyPath,
						};
						binding.sourceToUiConverters.AddConverter(_fromKeyCodeToStringConverter);
						binding.uiToSourceConverters.AddConverter(_fromStringToKeyCodeConverter);
						textField.SetBinding(nameof(TextField.value), binding);
						break;
				}
			}
		}

		/// <summary>
		/// Контейнер с функциями для работы с UI игрового процесса.
		/// </summary>
		public static class LevelUI
		{

		}
	}
}
