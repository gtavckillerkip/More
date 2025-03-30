using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс для снарядов.
/// </summary>
public class Projectile : MonoBehaviour
{
	#region Definitions
	private delegate void SetupDelegate(GameObject projectileObject, ProjectilesWorker.SetupParameters parameters);
	#endregion

	#region Fields declarations
	private static readonly Dictionary<ProjectileBehaviourType, SetupDelegate> _projectileBehaviourTypeToSetup = new()
	{
		[ProjectileBehaviourType.ForwardFlying] =			(projectileObject, parameters) =>
		{
			var projectile = projectileObject.GetComponent<Projectile>();
			projectile.StartCoroutine(Live(projectileObject, projectile.ProjectileStats.LifeDistance / projectile.ProjectileStats.Speed));

			projectileObject.transform.rotation = (parameters as ProjectilesWorker.ForwardFlyingSetupParameters).Direction;
			var rigidbody = projectileObject.GetComponent<Rigidbody>();
			rigidbody.AddForce(projectileObject.transform.rotation * Vector3.forward * projectile.ProjectileStats.Speed, ForceMode.Impulse);
		},

		[ProjectileBehaviourType.TargetChasing] =			(projectileObject, parameters) =>
		{
			
		},

		[ProjectileBehaviourType.InstantlyAffecting] =	(projectileObject, parameters) =>
		{
			
		},
	};

	/// <summary>
	/// Поведение в пространстве.
	/// </summary>
	[SerializeField] private ProjectileBehaviourType _environmentalBehaviourType;

	/// <summary>
	/// Эффекты при воздействии.
	/// </summary>
	[SerializeField] private ProjectileEffectType[] _effectTypes;
	#endregion

	#region Properties declarations
	/// <summary>
	/// Характеристики снаряда.
	/// </summary>
	public ProjectileStats ProjectileStats { get; private set; }

	/// <summary>
	/// Создатель снаряда.
	/// </summary>
	public Character Initiator { get; private set; }

	/// <summary>
	/// Обладает ли силой.
	/// </summary>
	public bool IsStrong { get; private set; }

	/// <summary>
	/// Поведение в пространстве.
	/// </summary>
	public ProjectileBehaviourType EnvironmentalBehaviourType => _environmentalBehaviourType;

	/// <summary>
	/// Эффекты при воздействии.
	/// </summary>
	public ProjectileEffectType[] EffectTypes => _effectTypes;
	#endregion

	/// <summary>
	/// Настроить снаряд.
	/// </summary>
	/// <param name="initiator"> Создатель снаряда. </param>
	/// <param name="direction"> Направление. </param>
	public void Setup(ProjectilesWorker.SetupParameters setupParameters)
	{
		Initiator = setupParameters.Initiator;
		ProjectileStats = setupParameters.Stats;
		IsStrong = true;

		_projectileBehaviourTypeToSetup[_environmentalBehaviourType].Invoke(gameObject, setupParameters);
	}

	private static IEnumerator Live(GameObject gameObject, float time)
	{
		yield return new WaitForSeconds(time);

		if (gameObject != null)
		{
			Destroy(gameObject);
		}
	}

	private void Update()
	{
		
	}

	private void OnTriggerEnter(Collider other)
	{
		if (IsStrong && other.gameObject.TryGetComponent(out Character character))
		{
			character.ReceiveDamage(Initiator, ProjectileStats.Damage);
		}

		Destroy(gameObject);
	}
}
