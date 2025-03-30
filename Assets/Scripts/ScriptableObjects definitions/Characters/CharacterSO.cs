using UnityEngine;

/// <summary>
/// Класс-контейнер данных персонажа.
/// </summary>
[CreateAssetMenu(menuName = "Scriptable objects/Characters/Character")]
public class CharacterSO : ScriptableObject
{
	/// <summary>
	/// Максимальное здоровье.
	/// </summary>
	public float MaxHealth;

	/// <summary>
	/// Начальное здоровье.
	/// </summary>
	public float InitialHealth;

	/// <summary>
	/// Префабрикэйтид.
	/// </summary>
	public GameObject Prefab;

	/// <summary>
	/// Набор способностей.
	/// </summary>
	public AbilitySetSO AbilitySet;

	/// <summary>
	/// Префаб контроллера.
	/// </summary>
	public GameObject ControllerPrefab;
}
