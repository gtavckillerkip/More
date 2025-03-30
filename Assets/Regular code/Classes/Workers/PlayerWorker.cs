using More.Basic.Management;
using UnityEngine;

namespace More.Workers
{
	public static class PlayerWorker
	{
		/// <summary>
		/// Создать игрового персонажа.
		/// </summary>
		/// <param name="playerSpawnPoint"> Объект точки спавна. </param>
		/// <returns> Объект игрового персонажа и его элементы. </returns>
		public static (GameObject playerObject, GameObject focus, GameObject groundChecker,
			GameObject firingPoint, GameObject directionSetter, GameObject controller) CreatePlayerCharacter(GameObject playerSpawnPoint)
		{
			var playerSO = ResourcesSystem.Instance.PlayerTypeToPlayerSO[PlayerCharacter.Seraphine];

			var playerObject = GameObject.Instantiate(playerSO.Prefab, playerSpawnPoint.transform.position, playerSO.Prefab.transform.rotation);

			var focus = playerObject.transform.Find("Focus").gameObject;
			var groundChecker = playerObject.transform.Find("Ground Checker").gameObject;
			var firingPoint = playerObject.transform.Find("Firing Point").gameObject;
			//var directionSetter = playerObject.transform.Find("Direction Setter").gameObject;
			var directionSetter = CameraManager.Instance.CameraObject;
			var controller = playerObject.transform.Find("Controller").gameObject;

			return (playerObject, focus, groundChecker, firingPoint, directionSetter, controller);
		}

		/// <summary>
		/// Подготовить игрока к игре.
		/// </summary>
		/// <param name="playerCharacter"> Игровой персонаж. </param>
		public static void PreparePlayerForPlaying(PlayerCharacter playerCharacter)
		{
			var playerSO = ResourcesSystem.Instance.PlayerTypeToPlayerSO[playerCharacter];
			var character = PlayerManager.Instance.PlayerObject.GetComponent<Character>();

			character.Setup(playerSO.MaxHealth, playerSO.InitialHealth);
			character.HealthRunOut += () =>
			{
				// game over
			};

			foreach (var abilityElement in playerSO.AbilitySet.AbilityList)
			{
				character.Abilities[abilityElement.Ability] =
					new Ability(
						abilityElement.AbilityScriptableObject.Projectile.Prefab,
						abilityElement.AbilityScriptableObject.Projectile.Stats,
						abilityElement.AbilityScriptableObject.CooldownTime
						);
			}

			PlayerManager.Instance.Controller.SetActive(true);

			GameHandler.Instance.GameStateMachine.Paused += () => GamePauseSwitchedHandler(true);
			GameHandler.Instance.GameStateMachine.Unpaused += () => GamePauseSwitchedHandler(false);
		}

		#region Events handlers
		private static void GamePauseSwitchedHandler(bool paused)
		{
			PlayerManager.Instance.Controller.SetActive(!paused);
		}
		#endregion
	}
}
