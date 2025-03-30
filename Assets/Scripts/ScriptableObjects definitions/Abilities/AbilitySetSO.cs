using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс-контейнер данных о наборе способностей.
/// </summary>
[CreateAssetMenu(menuName = "Scriptable objects/Abilities/Abilities set")]
public class AbilitySetSO : ScriptableObject
{
	[Serializable]
	public sealed class AbilityPointerToSO
	{
		public AbilityPointer Ability;
		public AbilitySO AbilityScriptableObject;
	}

	public List<AbilityPointerToSO> AbilityList;
}
