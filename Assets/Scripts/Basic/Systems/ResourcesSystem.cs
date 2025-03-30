using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ������� ��������.
/// </summary>
public class ResourcesSystem : SingletonMB<ResourcesSystem>
{
	/// <summary>
	/// ������ ���������� �������� ����.
	/// </summary>
	[field: SerializeField] public GameObject MainMenuUIPrefab { get; private set; }

	/// <summary>
	/// ������ ���������� �������� ������.
	/// </summary>
	[field: SerializeField] public GameObject LevelUIPrefab { get; private set; }

	/// <summary>
	/// ���������� ������ ������ � ������������ � ������ ������.
	/// </summary>
	public Dictionary<PlayerCharacter, PlayerSO> PlayerTypeToPlayerSO { get; private set; }

	/// <summary>
	/// ���������� ������ �������� � ������������ � ������ ��������.
	/// </summary>
	public Dictionary<MonsterCharacter, MonsterSO> MonsterTypeToMonsterSO { get; private set; }

	/// <summary>
	/// ���������� ��������.
	/// </summary>
	public List<SettingsSO> SettingsSOs { get; private set; }

	private void Start()
	{
		PlayerTypeToPlayerSO = Resources.LoadAll<PlayerSO>("ScriptableObjects/Characters/Players").ToDictionary(c => c.PlayerCharacter, c => c);
		MonsterTypeToMonsterSO = Resources.LoadAll<MonsterSO>("ScriptableObjects/Characters/Monsters").ToDictionary(c => c.MonsterCharacter, c => c);
		SettingsSOs = Resources.LoadAll<SettingsSO>("ScriptableObjects/Servicing/Settings").ToList();
	}
}
