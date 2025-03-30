using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace More.Basic.Management
{
	/// <summary>
	/// Состояние игры.
	/// </summary>
	public enum GameState
	{
		None,

		Start,

		MainMenu,

#if UNITY_EDITOR
		Dev,
#endif

		PrePlaying,

		Playing,

		Pause,

		Win,

		Lose
	}

	/// <summary>
	/// Параметры загрузки уровня.
	/// </summary>
	public struct LevelLoadParameters
	{
		public SceneName SceneName;

		public PlayerCharacter PlayerCharacter;

		// other parameters
	}

	/// <summary>
	/// Машина состояний игры.
	/// </summary>
	public class GameStateMachine : StateMachine
	{
		#region Properties declarations
		/// <summary>
		/// Состояние игры.
		/// </summary>
		public GameState State { get; private set; } = GameState.None;

		/// <summary>
		/// Параметры, с которыми был загружен игровой процесс.
		/// </summary>
		public LevelLoadParameters LevelLoadParameters { get; private set; }
		#endregion

		#region Events declarations
		/// <summary>
		/// Игра приостановлена.
		/// </summary>
		public event Action Paused;

		/// <summary>
		/// Игра продолжена после приостановки.
		/// </summary>
		public event Action Unpaused;
		#endregion

		/// <inheritdoc/>
		public GameStateMachine(MonoBehaviour mono) : base(mono) { }

        #region Machine runners
        /// <summary>
        /// Запуск машины при запуске игры.
        /// </summary>
        public IEnumerator RunOnGameStart()
		{
			yield return _mono.StartCoroutine(InternalRun(GameState.Start, GameState.MainMenu));
		}

#if UNITY_EDITOR
		/// <summary>
		/// Запуск машины при тесте уровня.
		/// </summary>
		public IEnumerator RunOnLevelDevTest()
		{
			yield return _mono.StartCoroutine(InternalRun(GameState.Dev));
		}
#endif

		/// <summary>
		/// Запуск машины при обычном выборе уровня из меню.
		/// </summary>
		/// <param name="sceneName"> Имя уровня (сцены). </param>
		/// <remarks> Новая игра и первый уровень, либо загруженная игра из сохранения. </remarks>
		public IEnumerator RunOnLevelChosen(LevelLoadParameters parameters)
		{
			LevelLoadParameters = parameters;
			yield return _mono.StartCoroutine(InternalRun(GameState.PrePlaying, GameState.Playing));
		}

		protected override IEnumerator InternalRun(params Enum[] states)
		{
			IsRunning = true;
			yield return null;

			for (int i = 0; i < states.Length; i++)
			{
				State = (GameState)states[i];

				yield return State switch
				{
					GameState.Start => OnStart(),
					GameState.MainMenu => OnMainMenu(),
#if UNITY_EDITOR
					GameState.Dev => OnDev(),
#endif
					GameState.PrePlaying => OnPrePlaying(),
					GameState.Playing => OnPlaying(),
					GameState.Win => OnWin(),
					GameState.Lose => OnLose(),
					_ => null
				};
			}

			IsRunning = false;
		}
		#endregion

		#region States handlers
		private IEnumerator JustInCaseCleanUpBeforeStart()
		{
			var loadedScenes = new List<Scene>();
			for (int i = 0; i < SceneManager.loadedSceneCount; i++)
			{
				loadedScenes.Add(SceneManager.GetSceneAt(i));
			}

			foreach (var scene in loadedScenes)
			{
				if (scene != SceneManager.GetSceneByName(SceneName.Persistent.ToString()))
				{
					yield return SceneManager.UnloadSceneAsync(scene);
				}
			}
		}

		private IEnumerator OnStart()
		{
			yield return JustInCaseCleanUpBeforeStart();

			yield return SceneManager.LoadSceneAsync(SceneName.MainMenu.ToString(), LoadSceneMode.Additive);
		}

		private IEnumerator OnMainMenu()
		{
			yield return null;

			yield return PlayerManager.Instance.PlayerCharacterStateMachine.RunOnMainMenu();

			yield return CameraManager.Instance.CameraStateMachine.RunOnMainMenu();

			yield return UIManager.Instance.UIStateMachine.RunOnMainMenu();
		}

#if UNITY_EDITOR
		private IEnumerator OnDev()
		{
			if (SceneManager.GetSceneByName(SceneName.Persistent.ToString()).isLoaded == false)
			{
				yield return SceneManager.LoadSceneAsync(SceneName.Persistent.ToString());
			}

			yield return InternalRun(GameState.Start, GameState.MainMenu);

			if (GameHandler.Instance.FirstSceneName != SceneName.MainMenu)
			{
				yield return RunOnLevelChosen(
					new()
					{
						SceneName = GameHandler.Instance.FirstSceneName,
						PlayerCharacter = PlayerCharacter.Seraphine,
					});
			}
		}
#endif

		private IEnumerator OnPrePlaying()
		{
			yield return null;

			yield return UIManager.Instance.UIStateMachine.RunOnPrePlaying();

			yield return SceneManager.LoadSceneAsync(LevelLoadParameters.SceneName.ToString(), LoadSceneMode.Additive);

			yield return PlayerManager.Instance.PlayerCharacterStateMachine.RunOnPrePlaying();

			yield return CameraManager.Instance.CameraStateMachine.RunOnPrePlaying();

			InputSystem.Instance.PausePressed += PausePressedHandler;
		}

		private IEnumerator OnPlaying()
		{
			yield return PlayerManager.Instance.PlayerCharacterStateMachine.RunOnPlayProcessStart();

			yield return CameraManager.Instance.CameraStateMachine.RunOnPlayProcessStart();

			yield return UIManager.Instance.UIStateMachine.RunOnPlayProcessStart();
		}

		private IEnumerator OnWin()
		{
			InputSystem.Instance.PausePressed -= PausePressedHandler;

			yield return null;
		}

		private IEnumerator OnLose()
		{
			InputSystem.Instance.PausePressed -= PausePressedHandler;

			yield return null;
		}
		#endregion

		#region Methods
		/// <summary>
		/// Поставить паузу.
		/// </summary>
		/// <param name="pause"> Состояние паузы. </param>
		private void SetPause(bool pause)
		{
			if (pause)
			{
				Time.timeScale = 0;
				Paused?.Invoke();
				_mono.StartCoroutine(InternalRun(GameState.Pause));
			}
			else
			{
				Time.timeScale = 1;
				Unpaused?.Invoke();
				_mono.StartCoroutine(InternalRun(GameState.Playing));
			}
		}
		#endregion

		#region Events handlers
		private void PausePressedHandler() => SetPause(State == GameState.Playing);
		#endregion
	}
}
