using UnityEngine;

/// <summary>
/// Класс для работы со снарядами.
/// </summary>
public static class ProjectilesWorker
{
	#region Definitions
	public class SetupParameters
	{
		public Character Initiator { get; private set; }

		public ProjectileStats Stats { get; private set; }

		public SetupParameters(Character initiator, ProjectileStats stats)
		{
			Initiator = initiator;
			Stats = stats;
		}
	}

	public class ForwardFlyingSetupParameters : SetupParameters
	{
		public Quaternion Direction { get; private set; }

		public ForwardFlyingSetupParameters(Character initiator, ProjectileStats stats, Quaternion direction) : base(initiator, stats) => Direction = direction;
	}

	public class TargetChasingSetupParameters : SetupParameters
	{
		public GameObject Target { get; private set; }

		public TargetChasingSetupParameters(Character initiator, ProjectileStats stats, GameObject target) : base(initiator, stats) => Target = target;
	}

	public class InstantlyAffectingSetupParameters : SetupParameters
	{
		public Character Target { get; private set; }

		public InstantlyAffectingSetupParameters(Character initiator, ProjectileStats stats, Character target) : base(initiator, stats) => Target = target;
	}
	#endregion

	public static void ReleaseProjectile(AbilityPointer currentAbility, Vector3 position, Quaternion rotation, Character initiator)
	{
		var projectilePrefab = initiator.Abilities[currentAbility].ProjectilePrefab;
		var projectile = Object.Instantiate(projectilePrefab, position, rotation);
		SetupParameters projectileSetupParameters = projectilePrefab.GetComponent<Projectile>().EnvironmentalBehaviourType switch
		{
			ProjectileBehaviourType.ForwardFlying		=> new ForwardFlyingSetupParameters(initiator, initiator.Abilities[currentAbility].ProjectileStats, rotation),
			ProjectileBehaviourType.TargetChasing		=> new TargetChasingSetupParameters(initiator, initiator.Abilities[currentAbility].ProjectileStats, null), // доработать (null)
			ProjectileBehaviourType.InstantlyAffecting	=> new InstantlyAffectingSetupParameters(initiator, initiator.Abilities[currentAbility].ProjectileStats, null), //
			_											=> throw new System.NotImplementedException()
		};

		projectile.GetComponent<Projectile>().Setup(projectileSetupParameters);
	}
}
