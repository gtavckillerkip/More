using System.Collections;
using UnityEngine;

namespace More.Basic.Management
{
	public class GameHandler : SingletonMB<GameHandler>
	{
		#region Properties declarations
#if UNITY_EDITOR
		/// <summary>
		/// Название первой сцены.
		/// </summary>
		[field: SerializeField] public SceneName FirstSceneName { get; private set; }
#endif
		/// <summary>
		/// Машина состояний игры.
		/// </summary>
		public GameStateMachine GameStateMachine { get; private set; }
		#endregion

		private IEnumerator Start()
		{
			GameStateMachine = new(this);

#if UNITY_EDITOR
			yield return GameStateMachine.RunOnLevelDevTest();
#else
			yield return GameStateMachine.RunOnGameStart();
#endif
		}
	}
}
