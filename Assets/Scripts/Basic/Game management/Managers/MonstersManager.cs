using System;
using System.Collections.Generic;
using UnityEngine;

namespace More.Basic.Management
{
	/// <summary>
	/// Менеджер монстров.
	/// </summary>
	public class MonstersManager : SingletonMB<MonstersManager>
	{
		#region Properties declarations
		/// <summary>
		/// Монстры.
		/// </summary>
		public List<GameObject> Monsters { get; private set; }
		#endregion

		#region Events declarations
		/// <summary>
		/// Монстр порождён.
		/// </summary>
		public event Action MonsterSpawned;

		/// <summary>
		/// Монстр убит.
		/// </summary>
		public event Action MonsterKilled;
		#endregion

		/// <summary>
		/// Создание монстров.
		/// </summary>
		public void CreateMonsters()
		{
			Monsters = new();

			List<Transform> enemySpawnPoints = new()
			{
				//GameObject.Find("/Environment/Spawn points/Enemies/Spawn point 1").transform,
				//GameObject.Find("/Environment/Spawn points/Enemies/Spawn point 2").transform,
				//GameObject.Find("/Environment/Spawn points/Enemies/Spawn point 3").transform,
				//GameObject.Find("/Environment/Spawn points/Enemies/Spawn point 4").transform
			};

			foreach (var point in enemySpawnPoints)
			{
				//var monsterSO = ResourcesSystem.Instance.MonsterTypeToMonsterSO[MonsterCharacter.Doubt];
				//var monster = UnityEngine.Object.Instantiate(monsterSO.Prefab, point.position, Quaternion.identity);
				//monster.GetComponent<Character>().Setup(monsterSO.MaxHealth, monsterSO.InitialHealth);
				//AddInstantiatedEnemyToList(monster);
				//monster.GetComponent<Character>().HealthRunOut += (sender, e) =>
				//{
				//	UnityEngine.Object.Destroy(monster);
				//	RemoveInstantiatedEnemyFromList(monster);
				//};

				//var character = monster.GetComponent<Character>();
				//foreach (var abilityElement in monsterSO.AbilitySet.AbilityList)
				//{
				//	character.Abilities[abilityElement.Ability] =
				//		new Ability(
				//			abilityElement.AbilityScriptableObject.Projectile.Prefab,
				//			abilityElement.AbilityScriptableObject.Projectile.Stats,
				//			abilityElement.AbilityScriptableObject.CooldownTime
				//			);
				//}
			}
		}

		/// <summary>
		/// Добавить инстанцированного врага в список.
		/// </summary>
		/// <param name="monster"> Объект врага. </param>
		public void AddInstantiatedEnemyToList(GameObject monster)
		{
			Monsters.Add(monster);
			MonsterSpawned?.Invoke();
		}

		/// <summary>
		/// Удалить инстанцированного врага из списка.
		/// </summary>
		/// <param name="monster"> Объект врага. </param>
		public void RemoveInstantiatedEnemyFromList(GameObject monster)
		{
			Monsters.Remove(monster);
			MonsterKilled?.Invoke();
		}
	}
}
