using System;
using System.Collections;
using UnityEngine;

namespace More.Basic.Management
{
	/// <summary>
	/// Состояние интерфейса.
	/// </summary>
	public enum UIState
	{
		None,

		MainMenu,

		Level
	}

	/// <summary>
	/// Машина состояний интерфейса.
	/// </summary>
	public class UIStateMachine : StateMachine
	{
		#region Properties declarations
		/// <summary>
		/// Состояние интерфейса.
		/// </summary>
		public UIState State { get; private set; } = UIState.None;
		#endregion

		/// <inheritdoc/>
		public UIStateMachine(MonoBehaviour mono) : base(mono) { }

		#region Machine runners
		public IEnumerator RunOnGameStart()
		{
			yield return _mono.StartCoroutine(InternalRun(UIState.None));
		}

		public IEnumerator RunOnMainMenu()
		{
			yield return _mono.StartCoroutine(InternalRun(UIState.MainMenu));
		}

		public IEnumerator RunOnPrePlaying()
		{
			yield return _mono.StartCoroutine(InternalRun(UIState.None));
		}

		public IEnumerator RunOnPlayProcessStart()
		{
			yield return _mono.StartCoroutine(InternalRun(UIState.Level));
		}

		protected override IEnumerator InternalRun(params Enum[] states)
		{
			IsRunning = true;
			yield return null;

			for (int i = 0; i < states.Length; i++)
			{
				State = (UIState)states[i];

				yield return State switch
				{
					UIState.None => OnNone(),
					UIState.MainMenu => OnMainMenu(),
					UIState.Level => OnLevel(),
					_ => null
				};
			}

			IsRunning = false;
		}
		#endregion

		#region States handlers
		private IEnumerator OnNone()
		{
			yield return null;

			UIManager.Instance.MainMenuUI.SetActive(false);
			UIManager.Instance.LevelUI.SetActive(false);
		}

		private IEnumerator OnMainMenu()
		{
			yield return null;

			UIManager.Instance.MainMenuUI.SetActive(true);
			UIManager.Instance.LevelUI.SetActive(false);
		}

		private IEnumerator OnLevel()
		{
			yield return null;

			UIManager.Instance.MainMenuUI.SetActive(false);
			UIManager.Instance.LevelUI.SetActive(true);
		}
		#endregion
	}
}
