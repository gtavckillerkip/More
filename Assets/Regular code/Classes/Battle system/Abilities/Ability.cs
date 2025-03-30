using UnityEngine;

/// <summary>
/// Класс способности.
/// </summary>
public class Ability
{
	/// <summary>
	/// Конструктор.
	/// </summary>
	/// <param name="projectilePrefab"> Префаб снаряда. </param>
	/// <param name="projectileStats"> Характеристики снаряда. </param>
	/// <param name="cooldownTime"> Время перезарядки. </param>
    public Ability(GameObject projectilePrefab, ProjectileStats projectileStats, int cooldownTime)
    {
        ProjectilePrefab = projectilePrefab;
		ProjectileStats = projectileStats;
		CooldownTime = cooldownTime;
    }

    #region Properties declarations
    /// <summary>
    /// Префаб снаряда.
    /// </summary>
    public GameObject ProjectilePrefab { get; private set; }

	/// <summary>
	/// Характеристики снаряда.
	/// </summary>
	public ProjectileStats ProjectileStats { get; private set; }

	/// <summary>
	/// Время перезарядки.
	/// </summary>
	public int CooldownTime { get; private set; }
	#endregion
}
