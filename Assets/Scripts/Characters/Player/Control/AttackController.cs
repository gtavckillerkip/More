using More.Basic.Management;
using System;
using UnityEngine;

/// <summary>
/// Контроллер атаки игрока.
/// </summary>
public class AttackController : MonoBehaviour
{
	#region Definitions
	public class ProjectileReleasedEventArgs : EventArgs
	{
		public ProjectileBehaviourType ProjectileBehaviourType;

		public ProjectileEffectType[] ProjectileEffectTypes;
	}

	/// <summary>
	/// Основные боевые действия.
	/// </summary>
	[Flags]
	public enum BasicBattleActions
	{
		/// <summary>
		/// Нет действия.
		/// </summary>
		None = 0,

		/// <summary>
		/// Прицеливание.
		/// </summary>
		Scope = 1,

		/// <summary>
		/// Одиночная атака.
		/// </summary>
		FireOnce = 2,

		/// <summary>
		/// Длительная атака.
		/// </summary>
		FireSpray = 4,
	}
	#endregion

	#region Fields declarations
	/// <summary>
	/// Максимальная дистанция от точки прицела вперёд.
	/// </summary>
	private const float AIM_MAX_FORWARD_DISTANCE = 100f;

	/// <summary>
	/// Установщик направления.
	/// </summary>
	private GameObject _directionSetter;

	/// <summary>
	/// Действия мыши.
	/// </summary>
	private BasicBattleActions _mouseActions;

	/// <summary>
	/// Контроллер способностей.
	/// </summary>
	private AbilitiesController _abilitiesController;

	/// <summary>
	/// Объект игрового персонажа.
	/// </summary>
	private GameObject _characterObject;

	/// <summary>
	/// Точка испускания снаряда.
	/// </summary>
	private GameObject _firingPoint;

	/// <summary>
	/// Под прицелом ли враг.
	/// </summary>
	private bool _enemySpotted;

	/// <summary>
	/// Активно ли прицеливание.
	/// </summary>
	private bool _isAimed;
	#endregion

	#region Events declarations
	public event Action Aimed;

	public event Action Unaimed;

	public event Action EnemySpotted;

	public event Action EnemyUnspotted;

	public event Action<ProjectileReleasedEventArgs> ProjectileReleased;
	#endregion

	private void Start()
	{
		_characterObject = PlayerManager.Instance.PlayerObject;
		_directionSetter = PlayerManager.Instance.DirectionSetter;
		_firingPoint = _characterObject.transform.Find("Firing Point").gameObject;

		_enemySpotted = false;

		_isAimed = false;

		_mouseActions = BasicBattleActions.None;

		_abilitiesController = PlayerManager.Instance.Controller.GetComponent<AbilitiesController>();

		InputSystem.Instance.PrimaryAttackPressed += PrimaryAttackPressedHandler;
		InputSystem.Instance.SecondaryAttackPressed += SecondaryAttackPressedHandler;
		InputSystem.Instance.SecondaryAttackReleased += SecondaryAttackReleasedHandler;
	}

	private void Update()
	{
		/* Enemy spotted */
		Physics.Raycast(_directionSetter.transform.position, _directionSetter.transform.rotation * Vector3.forward, out var hitInfo, float.MaxValue, -1, QueryTriggerInteraction.Ignore);
        if (_enemySpotted == true && hitInfo.collider != null && hitInfo.collider.GetComponent<Character>() == null)
        {
			_enemySpotted = false;
			EnemyUnspotted?.Invoke();
        }
        if (_enemySpotted == false && hitInfo.collider != null && hitInfo.collider.GetComponent<Character>() != null)
		{
			_enemySpotted = true;
			EnemySpotted?.Invoke();
		}
		/* ---------------- */

		/* Primary attack */
		if (_mouseActions.HasFlag(BasicBattleActions.FireOnce))
		{
			PrimaryAttack();
			_mouseActions &= ~BasicBattleActions.FireOnce;
		}
		/* ---------------- */

		/* Scoping */
		if (_mouseActions.HasFlag(BasicBattleActions.Scope) && !_isAimed)
		{
			_isAimed = true;
			Aimed?.Invoke();
		}
		if (!_mouseActions.HasFlag(BasicBattleActions.Scope) && _isAimed)
		{
			_isAimed = false;
			Unaimed?.Invoke();
		}
		/* ---------------- */
	}

	/// <summary>
	/// Основная атака.
	/// </summary>
	private void PrimaryAttack()
	{
		_characterObject.transform.rotation = Quaternion.Euler(0, _directionSetter.transform.eulerAngles.y, 0);

		var rayStart = _characterObject.transform.position + new Quaternion(0, _directionSetter.transform.rotation.y, 0, _directionSetter.transform.rotation.w) * new Vector3(0.5f, 1.6f, 0);
		var rayDir = _directionSetter.transform.rotation * Vector3.forward;
		var direction = Physics.Raycast(rayStart, rayDir, out var hitInfo, AIM_MAX_FORWARD_DISTANCE) ?
			hitInfo.point - _firingPoint.transform.position :
			rayStart + rayDir * AIM_MAX_FORWARD_DISTANCE - _firingPoint.transform.position;

		var character = _characterObject.GetComponent<Character>();
		ProjectilesWorker.ReleaseProjectile(_abilitiesController.CurrentAbility, _firingPoint.transform.position, Quaternion.LookRotation(direction), character);
		ProjectileReleased?.Invoke(
			new ProjectileReleasedEventArgs
			{
				ProjectileBehaviourType = character.Abilities[_abilitiesController.CurrentAbility].ProjectilePrefab.GetComponent<Projectile>().EnvironmentalBehaviourType,
				ProjectileEffectTypes = character.Abilities[_abilitiesController.CurrentAbility].ProjectilePrefab.GetComponent<Projectile>().EffectTypes
			}
		);
	}

	#region Events handlers
	private void PrimaryAttackPressedHandler()
	{
		_mouseActions |= BasicBattleActions.FireOnce;
	}

	private void SecondaryAttackPressedHandler()
	{
		_mouseActions |= BasicBattleActions.Scope;
	}

	private void SecondaryAttackReleasedHandler()
	{
		_mouseActions &= ~BasicBattleActions.Scope;
	}
	#endregion
}
