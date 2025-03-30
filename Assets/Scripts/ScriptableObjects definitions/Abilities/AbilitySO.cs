using UnityEngine;

/// <summary>
/// Класс-контейнер данных о способности.
/// </summary>
[CreateAssetMenu(menuName = "Scriptable objects/Abilities/Ability")]
public class AbilitySO : ScriptableObject
{
	public int CooldownTime;

	public ProjectileSO Projectile;
}
