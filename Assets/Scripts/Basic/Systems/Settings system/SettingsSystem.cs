using More.Basic;
using More.Settings_system;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

/// <summary>
/// ����� ��� ������ � �����������.
/// </summary>
public class SettingsSystem : SingletonMB<SettingsSystem>
{
	#region Definitions
	/// <summary>
	/// ���������� ������.
	/// </summary>
	public class Resolution
	{
		/// <summary>
		/// ������.
		/// </summary>
		public int Width { get; init; }

		/// <summary>
		/// ������.
		/// </summary>
		public int Height { get; init; }
	}
	#endregion

	#region Fields declarations
	/// <summary>
	/// ������� ���������.
	/// </summary>
	private Settings _currentSettings;

	/// <summary>
	/// ��������� ���� ��������.
	/// </summary>
	public event Action<Dictionary<FieldInfo, object>> CurrentSettingsChanged;
	#endregion

	#region Properties declarations
	public static Dictionary<ResolutionIdentifier, Resolution> ResolutionIdentifierToResolution = new()
	{
		[ResolutionIdentifier.HD] = new() { Width = 1280, Height = 720 },
		[ResolutionIdentifier.FWXGA] = new() { Width = 1366, Height = 768 },
		[ResolutionIdentifier.HDPlus] = new() { Width = 1600, Height = 900 },
		[ResolutionIdentifier.FullHD] = new() { Width = 1920, Height = 1080 },
		[ResolutionIdentifier.QHD] = new() { Width = 2560, Height = 1440 },
		[ResolutionIdentifier._4KUHD] = new() { Width = 3840, Height = 2160 },
	};

	/// <summary>
	/// ������� ���������.
	/// </summary>
	public Settings CurrentSettings
	{
		get => _currentSettings ?? LoadFromFile();
	}

	/// <summary>
	/// ��������� ��������� (��������).
	/// </summary>
	/// <remarks>
	/// � �������� ��������� �������� ���� ����������� ��������� ��������, ����� ��� ��������� ��������� ��� ����������.<br/>
	/// ����� �������, ��� ������������� �������� � ������������� �������� ��������.
	/// </remarks>
	private Dictionary<FieldInfo, object> ChangedProperties { get; } = new();
	#endregion

	/// <summary>
	/// ��������� ��������� �� �����.
	/// </summary>
	/// <returns> ������ ��������. </returns>
	private Settings LoadFromFile()
	{
		if (File.Exists(GlobalPaths.SettingsFilePath + GlobalNames.SettingsFileName))
		{
			string text = File.ReadAllText(GlobalPaths.SettingsFilePath + GlobalNames.SettingsFileName);
			_currentSettings = JsonUtility.FromJson<Settings>(text);
		}
		else
		{
			Directory.CreateDirectory(GlobalPaths.SettingsFilePath);

			var settingsSO = ResourcesSystem.Instance.SettingsSOs.Find(e => e.Title == "Default");

			_currentSettings = settingsSO == null ?
				new Settings() :
				new Settings()
				{
					ResolutionIdentifier = settingsSO.ResolutionIdentifier,
					FOV = settingsSO.FOV,
					InterfaceScale = settingsSO.InterfaceScale,

					SoundVolume = settingsSO.SoundVolume,
					MusicVolume = settingsSO.MusicVolume,

					MouseSensivity = settingsSO.MouseSensivity,
					MouseScopeSensivityScale = settingsSO.MouseScopeSensivityScale,
					MouseInversion = settingsSO.MouseInversion,

					MoveForward = settingsSO.MoveForward,
					MoveBackward = settingsSO.MoveBackward,
					MoveLeft = settingsSO.MoveLeft,
					MoveRight = settingsSO.MoveRight,
					Jump = settingsSO.Jump,
					Duck = settingsSO.Duck,
					Slowdown = settingsSO.Slowdown,
					Ability1 = settingsSO.Ability1,
					Ability2 = settingsSO.Ability2,
					Ability3 = settingsSO.Ability3,
					FullScreenSwitch = settingsSO.FullScreenSwitch,
					PrimaryAttack = settingsSO.PrimaryAttack,
					SecondaryAttack = settingsSO.SecondaryAttack,
					Use = settingsSO.Use,

					Difficulty = settingsSO.Difficulty,
				};

			SaveToFile(_currentSettings);
		}

		return _currentSettings;
	}

	/// <summary>
	/// ��������� ��������� � ����.
	/// </summary>
	/// <param name="settings"> ���� ��������. </param>
	private void SaveToFile(Settings settings)
	{
		string text = JsonUtility.ToJson(settings, true);
		File.WriteAllText(GlobalPaths.SettingsFilePath + GlobalNames.SettingsFileName, text);

		ChangedProperties.Clear();
	}

	/// <summary>
	/// ��������� ���������.
	/// </summary>
	public void Save()
	{
		for (int i = 0; i < ChangedProperties.Count; i++)
		{
			FieldInfo fieldInfo = CurrentSettings.GetType().GetField(ChangedProperties.ElementAt(i).Key.Name);
			fieldInfo.SetValue(CurrentSettings, Convert.ChangeType(ChangedProperties.ElementAt(i).Value, fieldInfo.FieldType));
		}

		if (ChangedProperties.Count > 0)
		{
			CurrentSettingsChanged?.Invoke(ChangedProperties);

			SaveToFile(CurrentSettings);
		}
	}

	/// <summary>
	/// �������� ���� (���������).
	/// </summary>
	/// <param name="fieldInfo"> ����. </param>
	/// <param name="value"> ����� ��������. </param>
	public void ChangeProperty(FieldInfo fieldInfo, object value) => ChangedProperties[fieldInfo] = value;

	/// <summary>
	/// �������� ���������.
	/// </summary>
	public void DiscardChanges() => ChangedProperties.Clear();
}
