using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Менеджер ввода.
/// </summary>
public class InputSystem : SingletonMB<InputSystem>
{
	#region Fields declarations
#pragma warning disable CS0067
	public event Action<Vector2> MouseRotated;
	public event Action<AbilityPointer> AbilityPressed;

	public event Action MoveForwardPressed;
	public event Action MoveForwardReleased;

	public event Action MoveBackwardPressed;
	public event Action MoveBackwardReleased;

	public event Action MoveLeftPressed;
	public event Action MoveLeftReleased;

	public event Action MoveRightPressed;
	public event Action MoveRightReleased;

	public event Action JumpPressed;

	public event Action SlowdownPressed;
	public event Action SlowdownReleased;

	public event Action PrimaryAttackPressed;

	public event Action SecondaryAttackPressed;
	public event Action SecondaryAttackReleased;

	public event Action UsePressed;
	public event Action UseReleased;

	public event Action FullscreenModeSwitchPressed;
	public event Action PausePressed;
#pragma warning restore CS0067

	/// <summary>
	/// Вращение мышью.
	/// </summary>
	private Vector2 _mouseRotation;

	/// <summary>
	/// Множитель инверсии мыши.
	/// </summary>
	private int _mouseInversionMultiplier;

	/// <summary>
	/// Словарь названий ввода и соответствующих клавиш.
	/// </summary>
	private Dictionary<string, KeyCode> _controlNamesToKeyCodes;
	#endregion

	private void Start()
	{
		_mouseRotation = Vector2.zero;
		_mouseInversionMultiplier = SettingsSystem.Instance.CurrentSettings.MouseInversion ? -1 : 1;

		_controlNamesToKeyCodes = new()
		{
			["Move Forward"] =				SettingsSystem.Instance.CurrentSettings.MoveForward,
			["Move Backward"] =				SettingsSystem.Instance.CurrentSettings.MoveBackward,
			["Move Left"] =					SettingsSystem.Instance.CurrentSettings.MoveLeft,
			["Move Right"] =				SettingsSystem.Instance.CurrentSettings.MoveRight,
			["Jump"] =						SettingsSystem.Instance.CurrentSettings.Jump,
			["Duck"] =						SettingsSystem.Instance.CurrentSettings.Duck,
			["Slowdown"] =					SettingsSystem.Instance.CurrentSettings.Slowdown,
			["Ability 1"] =					SettingsSystem.Instance.CurrentSettings.Ability1,
			["Ability 2"] =					SettingsSystem.Instance.CurrentSettings.Ability2,
			["Ability 3"] =					SettingsSystem.Instance.CurrentSettings.Ability3,
			["Fullscreen Mode Switch"] =	SettingsSystem.Instance.CurrentSettings.FullScreenSwitch,
			["Use"] =						SettingsSystem.Instance.CurrentSettings.Use,
			["Primary Attack"] =			SettingsSystem.Instance.CurrentSettings.PrimaryAttack,
			["Secondary Attack"] =			SettingsSystem.Instance.CurrentSettings.SecondaryAttack,
			["Pause"] =						SettingsSystem.Instance.CurrentSettings.Pause,
		};
	}

	private void Update()
	{
		#region Mouse rotation
		_mouseRotation.y = Input.GetAxis("Mouse Y") * _mouseInversionMultiplier;
		_mouseRotation.x = Input.GetAxis("Mouse X");
		
		if (_mouseRotation != Vector2.zero)
		{
			MouseRotated?.Invoke(_mouseRotation);
		}
		#endregion

		#region Movement
		if (Input.GetKeyDown(_controlNamesToKeyCodes["Move Forward"]))
		{
			MoveForwardPressed?.Invoke();
		}

		if (Input.GetKeyUp(_controlNamesToKeyCodes["Move Forward"]))
		{
			MoveForwardReleased?.Invoke();
		}

		if (Input.GetKeyDown(_controlNamesToKeyCodes["Move Backward"]))
		{
			MoveBackwardPressed?.Invoke();
		}

		if (Input.GetKeyUp(_controlNamesToKeyCodes["Move Backward"]))
		{
			MoveBackwardReleased?.Invoke();
		}

		if (Input.GetKeyDown(_controlNamesToKeyCodes["Move Left"]))
		{
			MoveLeftPressed?.Invoke();
		}

		if (Input.GetKeyUp(_controlNamesToKeyCodes["Move Left"]))
		{
			MoveLeftReleased?.Invoke();
		}

		if (Input.GetKeyDown(_controlNamesToKeyCodes["Move Right"]))
		{
			MoveRightPressed?.Invoke();
		}

		if (Input.GetKeyUp(_controlNamesToKeyCodes["Move Right"]))
		{
			MoveRightReleased?.Invoke();
		}

		if (Input.GetKeyDown(_controlNamesToKeyCodes["Jump"]))
		{
			JumpPressed?.Invoke();
		}

		if (Input.GetKeyDown(_controlNamesToKeyCodes["Slowdown"]))
		{
			SlowdownPressed?.Invoke();
		}

		if (Input.GetKeyUp(_controlNamesToKeyCodes["Slowdown"]))
		{
			SlowdownReleased?.Invoke();
		}
		#endregion

		#region Basic battle actions
		if (Input.GetKeyDown(_controlNamesToKeyCodes["Primary Attack"]))
		{
			PrimaryAttackPressed?.Invoke();
		}

		if (Input.GetKeyDown(_controlNamesToKeyCodes["Secondary Attack"]))
		{
			SecondaryAttackPressed?.Invoke();
		}

		if (Input.GetKeyUp(_controlNamesToKeyCodes["Secondary Attack"]))
		{
			SecondaryAttackReleased?.Invoke();
		}
		#endregion

		#region Abilities
		var ability1 = Input.GetKeyDown(_controlNamesToKeyCodes["Ability 1"]);
		var ability2 = Input.GetKeyDown(_controlNamesToKeyCodes["Ability 2"]);
		var ability3 = Input.GetKeyDown(_controlNamesToKeyCodes["Ability 3"]);

		if (ability1 && !ability2 && !ability3 ||
			!ability1 && ability2 && !ability3 ||
			!ability1 && !ability2 && ability3)
		{
			AbilityPressed?.Invoke(
				ability1 ?
					AbilityPointer.Ability1 :
					ability2 ?
						AbilityPointer.Ability2 :
						ability3 ?
							AbilityPointer.Ability3 :
							AbilityPointer.Elementary
			);
		}
		#endregion

		#region Miscellaneous
		if (Input.GetKeyDown(_controlNamesToKeyCodes["Use"]))
		{
			UsePressed?.Invoke();
		}

		if (Input.GetKeyDown(_controlNamesToKeyCodes["Fullscreen Mode Switch"]))
		{
			FullscreenModeSwitchPressed?.Invoke();
		}

		if (Input.GetKeyUp(_controlNamesToKeyCodes["Pause"]))
		{
			PausePressed?.Invoke();
		}
		#endregion
	}
}
