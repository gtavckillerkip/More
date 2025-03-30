using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Класс контроллера способностей.
/// </summary>
public class AbilitiesController : MonoBehaviour
{
	#region Definitions
	/// <summary>
	/// Состояние способности.
	/// </summary>
	public enum AbilityState
	{
		/// <summary>
		/// Не активирована / деактивирована.
		/// </summary>
		Deactivated,

		/// <summary>
		/// Активирована.
		/// </summary>
		Activated,

		/// <summary>
		/// Исполняется.
		/// </summary>
		Casting,

		/// <summary>
		/// Перезарядка.
		/// </summary>
		Cooldown
	}

	/// <summary>
	/// Аргументы события изменения состояния способности.
	/// </summary>
	public class AbilityStateChangedEventArgs : EventArgs
	{
		/// <summary>
		/// Способность.
		/// </summary>
		public AbilityPointer Ability;

		/// <summary>
		/// Новое состояние.
		/// </summary>
		public AbilityState State;
	}
	#endregion

	#region Fields declarations
	/// <summary>
	/// Состояния способностей.
	/// </summary>
	public readonly Dictionary<AbilityPointer, AbilityState> AbilitiesStates = new()
	{
		[AbilityPointer.Elementary] = AbilityState.Activated,
		[AbilityPointer.Ability1] = AbilityState.Deactivated,
		[AbilityPointer.Ability2] = AbilityState.Deactivated,
		[AbilityPointer.Ability3] = AbilityState.Deactivated,
	};

	/// <summary>
	/// Событие изменения состояния способности.
	/// </summary>
	public event Action<AbilityStateChangedEventArgs> AbilityStateChanged;
	#endregion

	#region Properties declarations
	/// <summary>
	/// Текущая способность.
	/// </summary>
	/// <remarks> Всегда есть текущая способность, хотя бы простая. </remarks>
	public AbilityPointer CurrentAbility {  get; private set; }
	#endregion

	private void Start()
	{
		CurrentAbility = AbilityPointer.Elementary;

		InputSystem.Instance.AbilityPressed += AbilityPressedHandler;
	}

	#region Events handlers
	private void AbilityPressedHandler(AbilityPointer ap)
	{
		switch (AbilitiesStates[ap])
		{
			case AbilityState.Deactivated:
			{
				CurrentAbility = ap;
				AbilitiesStates[CurrentAbility] = AbilityState.Activated;
				AbilityStateChanged?.Invoke(new AbilityStateChangedEventArgs { Ability = ap, State = AbilityState.Activated });

				for (int i = 0; i < AbilitiesStates.Keys.Count; i++)
				{
					var key = AbilitiesStates.Keys.ElementAt(i);
					if (key != ap)
					{
						if (AbilitiesStates[key] == AbilityState.Activated)
						{
							AbilitiesStates[key] = AbilityState.Deactivated;
							AbilityStateChanged?.Invoke(new AbilityStateChangedEventArgs { Ability = key, State = AbilityState.Deactivated });
						}
					}
				}

				break;
			}

			case AbilityState.Activated:
			{
				AbilitiesStates[ap] = AbilityState.Deactivated;
				AbilityStateChanged?.Invoke(new AbilityStateChangedEventArgs { Ability = ap, State = AbilityState.Deactivated });

				CurrentAbility = AbilityPointer.Elementary;
				AbilitiesStates[CurrentAbility] = AbilityState.Activated;

				break;
			}
		}
	}
	#endregion
}
