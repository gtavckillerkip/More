using UnityEngine;

namespace More.Basic.Management
{
	/// <summary>
	/// Босс сцены.
	/// </summary>
	public class SceneHandler : MonoBehaviour
	{
		#region Properties declarations
		/// <summary>
		/// Точка появления игрока.
		/// </summary>
		/// <remarks> Нужна при загрузке сцены непосредственно, а не при процессе прохождения игры. </remarks>
		[field: SerializeField] public GameObject PlayerSpawnPoint { get; private set; }
		#endregion

		private void OnEnable()
		{
			
		}

		private void OnDisable()
		{
			
		}
	}
}
