using More.Basic.Management;
using System.Collections;
using UnityEngine;

/// <summary>
/// ��������� �����.
/// </summary>
public class MonsterBehaviour : MonoBehaviour
{
	/// <summary>
	/// ��������� ���������.
	/// </summary>
	public enum BehaviourStates
	{
		/// <summary>
		/// ����� ������� �� ����.
		/// </summary>
		LostPlayer,

		/// <summary>
		/// ����� ������, � ������� ���������.
		/// </summary>
		DiscoveredPlayer,

		/// <summary>
		/// ��������.
		/// </summary>
		IdleWaiting
	}

	#region Fields declarations
	/// <summary>
	/// ����� ������ �� ���������.
	/// </summary>
	private GameObject _playerCharacterFocus;

	/// <summary>
	/// ���������� ���� ������.
	/// </summary>
	private float _halfSightAngle = 30f;

	/// <summary>
	/// ������ �� ����� �� ������.
	/// </summary>
	public Vector3 VectorToPlayer { get; private set; } = Vector3.zero;

	/// <summary>
	/// ������� ���������.
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
	/// ���� ���� ������, �������� � ��������� �����������.
	/// </summary>
	/// <param name="forwardPlayerAngle"> ���� ����� �������� "�����" � �������� �� ����� �� ������. </param>
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
    /// ���� ��������� ������.
    /// </summary>
    private void OnPlayerIsFound()
	{
		if (Physics.Raycast(transform.position, VectorToPlayer, out RaycastHit rayHit, VectorToPlayer.magnitude))
		{
			// lost?
			if (!rayHit.collider.gameObject.CompareTag(PlayerManager.Instance.PlayerObject.tag))
			{
				BehaviourState = BehaviourStates.IdleWaiting;
				StartCoroutine(WaitWhenLost()); // ������ ������? � idlefinding
				return;
			}
		}
	}

	/// <summary>
	/// ���� ���� ������, �������� �� ����� (������� �� ����).
	/// </summary>
	/// <param name="forwardPlayerAngle"> ���� ����� �������� "�����" � �������� �� ����� �� ������. </param>
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
