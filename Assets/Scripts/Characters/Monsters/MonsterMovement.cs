using More.Basic.Management;
using UnityEngine;
using UnityEngine.LowLevel;

/// <summary>
/// Движение врага.
/// </summary>
public class MonsterMovement : MonoBehaviour
{
	#region Fields declarations
	/// <summary>
	/// Скрипт поведения врага.
	/// </summary>
	private MonsterBehaviour _enemyBehaviour;

	/// <summary>
	/// Скорость движения.
	/// </summary>
	private float _movementSpeed = 2f;

	/// <summary>
	/// Минимальное расстояние до препятствия.
	/// </summary>
	private float _minDistanceToObstacle = 1f;
	#endregion

	private void Start()
	{
		_enemyBehaviour = GetComponent<MonsterBehaviour>();
	}

	private void Update()
	{
		switch (_enemyBehaviour.BehaviourState)
		{
			case MonsterBehaviour.BehaviourStates.LostPlayer:
				Float();
				break;

			case MonsterBehaviour.BehaviourStates.DiscoveredPlayer:
				MoveToPlayer();
				break;

			case MonsterBehaviour.BehaviourStates.IdleWaiting:
				break;
		}
	}

	/// <summary>
	/// Движение врага в поисках игрока.
	/// </summary>
	private void Float()
	{
		transform.Translate(0, 0, _movementSpeed * Time.deltaTime);

		var ray = new Ray(transform.position, transform.forward);

		if (Physics.SphereCast(ray, 0.75f, out var hit) && hit.distance < _minDistanceToObstacle)
			transform.Rotate(0, Vector3.Angle(transform.forward, hit.normal) + Random.Range(-80, 80), 0);
	}

	/// <summary>
	/// Движение врага к игроку.
	/// </summary>
	private void MoveToPlayer()
	{
		transform.LookAt(PlayerManager.Instance.Focus.transform.position);

		if (Physics.Raycast(transform.position, _enemyBehaviour.VectorToPlayer, out RaycastHit rayHit, _enemyBehaviour.VectorToPlayer.magnitude))
		{
			if (rayHit.distance > _minDistanceToObstacle)
			{
				transform.Translate(0, 0, _movementSpeed * Time.deltaTime);
			}
		}
	}
}
