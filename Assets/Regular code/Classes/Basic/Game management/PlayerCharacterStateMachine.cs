using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace More.Basic.Management
{
	/// <summary>
	/// Состояние игрока.
	/// </summary>
	public enum PlayerCharacterState
	{
		Absence,

		Creation,

		SettingUp,

		Active,

		Inactive,

		Destroying
	}

	/// <summary>
	/// Машина состояний игрового персонажа.
	/// </summary>
	public class PlayerCharacterStateMachine : StateMachine
	{
		#region Properties declarations
		/// <summary>
		/// Состояние игрока.
		/// </summary>
		public PlayerCharacterState State { get; private set; } = PlayerCharacterState.Absence;
		#endregion

		/// <inheritdoc/>
		public PlayerCharacterStateMachine(MonoBehaviour mono) : base(mono) { }

		#region Machine runners
		public IEnumerator RunOnGameStart()
		{
			yield return _mono.StartCoroutine(InternalRun(PlayerCharacterState.Absence));
		}

		public IEnumerator RunOnMainMenu()
		{
			yield return _mono.StartCoroutine(InternalRun(PlayerCharacterState.Creation, PlayerCharacterState.Inactive));
		}

		public IEnumerator RunOnPrePlaying()
		{
			yield return _mono.StartCoroutine(InternalRun(PlayerCharacterState.SettingUp));
		}

		public IEnumerator RunOnPlayProcessStart()
		{
			yield return _mono.StartCoroutine(InternalRun(PlayerCharacterState.Active));
		}

		protected override IEnumerator InternalRun(params Enum[] states)
		{
			IsRunning = true;
			yield return null;

			for (int i = 0; i < states.Length; i++)
			{
				State = (PlayerCharacterState)states[i];

				yield return State switch
				{
					PlayerCharacterState.Absence => OnAbsence(),
					PlayerCharacterState.Creation => OnCreation(),
					PlayerCharacterState.SettingUp => OnSettingUp(),
					PlayerCharacterState.Active => OnActive(),
					PlayerCharacterState.Inactive => OnInactive(),
					PlayerCharacterState.Destroying => OnDestroying(),
					_ => null,
				};
			}

			IsRunning = false;
		}
		#endregion

		#region States handlers
		private IEnumerator OnAbsence()
		{
			yield return null;
		}

		private IEnumerator OnCreation()
		{
			yield return null;

			var sceneHandler = SceneManager
				.GetSceneByName(SceneName.MainMenu.ToString())
				.GetRootGameObjects()
				.First(go => go.TryGetComponent<SceneHandler>(out var _) == true)
				.GetComponent<SceneHandler>();
			var playerSpawnPoint = sceneHandler.PlayerSpawnPoint;

			PlayerManager.Instance.CreatePlayer(playerSpawnPoint);
		}

		private IEnumerator OnSettingUp()
		{
			yield return null;

			// move player left on new game or right and teleport on load game
			// get rid of it later
			var sceneHandler = SceneManager
				.GetSceneByName(GameHandler.Instance.GameStateMachine.LevelLoadParameters.SceneName.ToString())
				.GetRootGameObjects()
				.FirstOrDefault(go => go.TryGetComponent<SceneHandler>(out var _) == true)
				.GetComponent<SceneHandler>();
			GameObject tpToPoint = sceneHandler.PlayerSpawnPoint; // groil

			yield return new WaitForSeconds(2); // groil

			PlayerManager.Instance.PlayerObject.transform.position = tpToPoint.transform.position; // groil

			yield return null;

			PlayerManager.Instance.PreparePlayerForPlaying(GameHandler.Instance.GameStateMachine.LevelLoadParameters.PlayerCharacter);
		}

		private IEnumerator OnActive()
		{
			yield return null;

			PlayerManager.Instance.Controller.SetActive(true);
		}

		private IEnumerator OnInactive()
		{
			yield return null;

			PlayerManager.Instance.Controller.SetActive(false);
		}

		private IEnumerator OnDestroying()
		{
			yield return null;

			PlayerManager.Instance.DestroyPlayer();

			State = PlayerCharacterState.Absence;
		}
		#endregion
	}
}
