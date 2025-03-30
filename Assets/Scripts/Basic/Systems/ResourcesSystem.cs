using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Система ресурсов.
/// </summary>
public class ResourcesSystem : SingletonMB<ResourcesSystem>
{
	/// <summary>
	/// Префаб интерфейса главного меню.
	/// </summary>
	[field: SerializeField] public GameObject MainMenuUIPrefab { get; private set; }

	/// <summary>
	/// Префаб интерфейса игрового уровня.
	/// </summary>
	[field: SerializeField] public GameObject LevelUIPrefab { get; private set; }

	/// <summary>
	/// Контейнеры данных игрока в соответствии с типами игрока.
	/// </summary>
	public Dictionary<PlayerCharacter, PlayerSO> PlayerTypeToPlayerSO { get; private set; }

	/// <summary>
	/// Контейнеры данных монстров в соответствии с типами монстров.
	/// </summary>
	public Dictionary<MonsterCharacter, MonsterSO> MonsterTypeToMonsterSO { get; private set; }

	/// <summary>
	/// Контейнеры настроек.
	/// </summary>
	public List<SettingsSO> SettingsSOs { get; private set; }

	private void Start()
	{
		PlayerTypeToPlayerSO = Resources.LoadAll<PlayerSO>("ScriptableObjects/Characters/Players").ToDictionary(c => c.PlayerCharacter, c => c);
		MonsterTypeToMonsterSO = Resources.LoadAll<MonsterSO>("ScriptableObjects/Characters/Monsters").ToDictionary(c => c.MonsterCharacter, c => c);
		SettingsSOs = Resources.LoadAll<SettingsSO>("ScriptableObjects/Servicing/Settings").ToList();
	}
}
