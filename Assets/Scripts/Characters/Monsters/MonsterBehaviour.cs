using More.Basic.Management;
using System.Collections;
using UnityEngine;

/// <summary>
/// Поведение врага.
/// </summary>
public class MonsterBehaviour : MonoBehaviour
{
	/// <summary>
	/// Состояния поведения.
	/// </summary>
	public enum BehaviourStates
	{
		/// <summary>
		/// Игрок потерян из виду.
		/// </summary>
		LostPlayer,

		/// <summary>
		/// Игрок найден, в области видимости.
		/// </summary>
		DiscoveredPlayer,

		/// <summary>
		/// Ожидание.
		/// </summary>
		IdleWaiting
	}

	#region Fields declarations
	/// <summary>
	/// Точка фокуса на персонаже.
	/// </summary>
	private GameObject _playerCharacterFocus;

	/// <summary>
	/// Половинный угол обзора.
	/// </summary>
	private float _halfSightAngle = 30f;

	/// <summary>
	/// Вектор от врага до игрока.
	/// </summary>
	public Vector3 VectorToPlayer { get; private set; } = Vector3.zero;

	/// <summary>
	/// Текущее состояние.
	/// </summary>
	public BehaviourStates BehaviourState { get; private set; } = BehaviourStates.LostPlayer;
	#endregion

	private void Start()
	{
		_playerCharacterFocus = PlayerManager.Instance.Focus;
	}

	private void Update()
	{
		VectorToPlayer = _playerCharacterFocus.transform.position - transform.position;
		var forwardPlayerAngle = Vector3.Angle(transform.forward, VectorToPlayer);

		switch (BehaviourState)
		{
			case BehaviourStates.LostPlayer:
				FloatingFindingPlayer(forwardPlayerAngle);
				break;

			case BehaviourStates.DiscoveredPlayer:
				OnPlayerIsFound();
				break;

			case BehaviourStates.IdleWaiting:
				IdleFindingPlayer(forwardPlayerAngle);
				break;
		}
	}

	/// <summary>
	/// Враг ищет игрока, двигаясь в случайном направлении.
	/// </summary>
	/// <param name="forwardPlayerAngle"> Угол между вектором "вперёд" и вектором от врага до игрока. </param>
	private void FloatingFindingPlayer(float forwardPlayerAngle)
	{
		if (forwardPlayerAngle >= -_halfSightAngle && forwardPlayerAngle <= _halfSightAngle)
		{
			Physics.Raycast(transform.position, VectorToPlayer, out RaycastHit rayHit, VectorToPlayer.magnitude);

			if (rayHit.collider.gameObject.CompareTag(PlayerManager.Instance.PlayerObject.tag))
			{
				BehaviourState = BehaviourStates.DiscoveredPlayer;
			}
		}
	}

    /// <summary>
    /// Враг обнаружил игрока.
    /// </summary>
    private void OnPlayerIsFound()
	{
		if (Physics.Raycast(transform.position, VectorToPlayer, out RaycastHit rayHit, VectorToPlayer.magnitude))
		{
			// lost?
			if (!rayHit.collider.gameObject.CompareTag(PlayerManager.Instance.PlayerObject.tag))
			{
				BehaviourState = BehaviourStates.IdleWaiting;
				StartCoroutine(WaitWhenLost()); // убрать отсюда? в idlefinding
				return;
			}
		}
	}

	/// <summary>
	/// Враг ищет игрока, находясь на месте (потерял из виду).
	/// </summary>
	/// <param name="forwardPlayerAngle"> Угол между вектором "вперёд" и вектором от врага до игрока. </param>
	private void IdleFindingPlayer(float forwardPlayerAngle)
	{		
		if (forwardPlayerAngle >= -_halfSightAngle && forwardPlayerAngle <= _halfSightAngle)
		{
			Physics.Raycast(transform.position, VectorToPlayer, out var tryHit);

			// found?
			if (tryHit.collider.gameObject.CompareTag(PlayerManager.Instance.PlayerObject.tag))
			{
				BehaviourState = BehaviourStates.DiscoveredPlayer;
				return;
			}
		}
	}

	private IEnumerator WaitWhenLost()
	{
		yield return new WaitForSeconds(5);

		if (BehaviourState == BehaviourStates.IdleWaiting)
		{
			BehaviourState = BehaviourStates.LostPlayer;
		}
	}
}
