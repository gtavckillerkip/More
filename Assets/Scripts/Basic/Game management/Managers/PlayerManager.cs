using More.UI;
using More.Workers;
using UnityEngine;

namespace More.Basic.Management
{
	/// <summary>
	/// Менеджер игрока.
	/// </summary>
	public class PlayerManager : SingletonMB<PlayerManager>
	{
		#region Properties declarations
		/// <summary>
		/// Игровой персонаж.
		/// </summary>
		/// <remarks> Весь объект: модель и вспомогательные элементы. </remarks>
		public GameObject PlayerObject { get; private set; }

		/// <summary>
		/// Точка фокуса персонажа.
		/// </summary>
		/// <remarks> Можно использовать для вычисления различных векторов и для других целей. </remarks>
		public GameObject Focus { get; private set; }

		/// <summary>
		/// Проверщик нахождения на земле.
		/// </summary>
		public GameObject GroundChecker { get; private set; }

		/// <summary>
		/// Точка испускания снарядов.
		/// </summary>
		public GameObject FiringPoint { get; private set; }

		/// <summary>
		/// Установщик направления.
		/// </summary>
		public GameObject DirectionSetter { get; private set; }

		/// <summary>
		/// Контроллер.
		/// </summary>
		/// <remarks> Управление персонажем. </remarks>
		public GameObject Controller { get; private set; }

		/// <summary>
		/// Машина состояний игрового персонажа.
		/// </summary>
		public PlayerCharacterStateMachine PlayerCharacterStateMachine { get; private set; }
		#endregion

		private void Start()
		{
			PlayerCharacterStateMachine = new(this);
			PlayerCharacterStateMachine.RunOnGameStart();
		}

		/// <summary>
		/// Создание игрока.
		/// </summary>
		/// <param name="playerSpawnPoint"> Точка появления игрока. </param>
		public void CreatePlayer(GameObject playerSpawnPoint)
		{
			if (PlayerObject != null)
			{
				return;
			}

			(PlayerObject, Focus, GroundChecker, FiringPoint, DirectionSetter, Controller) = PlayerWorker.CreatePlayerCharacter(playerSpawnPoint);
		}

		/// <summary>
		/// Подготовить игрока к игре.
		/// </summary>
		/// <param name="playerCharacter"> Игровой персонаж. </param>
		public void PreparePlayerForPlaying(PlayerCharacter playerCharacter)
		{
			if (PlayerObject == null)
			{
				return;
			}

			PlayerWorker.PreparePlayerForPlaying(playerCharacter);
		}

		/// <summary>
		/// Уничтожить игрока.
		/// </summary>
		public void DestroyPlayer()
		{
			GameObject.Destroy(PlayerObject);
			Focus = null;
			GroundChecker = null;
			FiringPoint = null;
			DirectionSetter = null;
			Controller = null;
		}
	}
}
