using UnityEngine;

/// <summary>
/// Класс-контейнер данных о снаряде.
/// </summary>
[CreateAssetMenu(menuName = "Scriptable objects/Projectiles/Projectile")]
public class ProjectileSO : ScriptableObject
{
	public ProjectileStats Stats;

	public GameObject Prefab;
}
