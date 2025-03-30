using System;
using System.Collections;
using UnityEngine;

namespace More.Basic.Management
{
	/// <summary>
	/// Состояние камеры.
	/// </summary>
	public enum CameraState
	{
		Stuck,

		Free,

		Glued,

		Cinematographic,
	}

	/// <summary>
	/// Машина состояний камеры.
	/// </summary>
	public class CameraStateMachine : StateMachine
	{
		#region Properties declarations
		/// <summary>
		/// Состояние камеры.
		/// </summary>
		public CameraState State { get; private set; }
		#endregion

		/// <inheritdoc/>
		public CameraStateMachine(MonoBehaviour mono) : base(mono) { }

		#region Machine runners
		public IEnumerator RunOnGameStart()
		{
			yield return _mono.StartCoroutine(InternalRun(CameraState.Stuck));
		}

		public IEnumerator RunOnMainMenu()
		{
			yield return _mono.StartCoroutine(InternalRun(CameraState.Stuck));
		}

		public IEnumerator RunOnPrePlaying()
		{
			yield return _mono.StartCoroutine(InternalRun(CameraState.Cinematographic));
		}

		public IEnumerator RunOnPlayProcessStart()
		{
			yield return _mono.StartCoroutine(InternalRun(CameraState.Glued));
		}

		protected override IEnumerator InternalRun(params Enum[] states)
		{
			IsRunning = true;
			yield return null;

			for (int i = 0; i < states.Length; i++)
			{
				State = (CameraState)states[i];

				yield return State switch
				{
					CameraState.Stuck => OnStuck(),
					CameraState.Free => OnFree(),
					CameraState.Glued => OnGlued(),
					CameraState.Cinematographic => OnCinematographic(),
					_ => null
				};
			}

			IsRunning = false;
		}
		#endregion

		#region States handlers
		private IEnumerator OnStuck()
		{
			yield return null;

			CameraManager.Instance.Controller.SetActive(false);
		}

		private IEnumerator OnFree()
		{
			yield return null;
		}

		private IEnumerator OnGlued()
		{
			yield return null;

			CameraManager.Instance.Controller.SetActive(true);
		}

		private IEnumerator OnCinematographic()
		{
			yield return null;
		}
		#endregion
	}
}
