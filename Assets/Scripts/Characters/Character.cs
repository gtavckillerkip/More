using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс персонажа.
/// </summary>
/// <remarks> Базовый класс для объектов, взаимодействующих с игрой и друг с другом (игрок, враги, прочее). </remarks>
public class Character : MonoBehaviour
{
	#region Fields declarations
	/// <summary>
	/// Здоровье уменьшено.
	/// </summary>
	public event Action HealthDecreased;

	/// <summary>
	/// Здоровье увеличено.
	/// </summary>
	public event Action HealthIncreased;

	/// <summary>
	/// Здоровье кончилось.
	/// </summary>
	public event Action HealthRunOut;
	#endregion

	#region Properties declarations
	/// <summary>
	/// Максимальное здоровье.
	/// </summary>
	public float MaxHealth { get; private set; }

	/// <summary>
	/// Здоровье.
	/// </summary>
	public float Health { get; private set; }

	/// <summary>
	/// Способности.
	/// </summary>
	public Dictionary<AbilityPointer, Ability> Abilities { get; private set; } = new();
	#endregion

	#region Methods
	/// <summary>
	/// Настройка характеристик.
	/// </summary>
	/// <param name="maxHealth"> Максимальное здоровье. </param>
	/// <param name="health"> Начальное здоровье. </param>
	/// <param name="attackSpeed"> Скорость атаки. </param>
	public void Setup(float maxHealth, float health)
	{
		MaxHealth = maxHealth;
		Health = health;
	}

	/// <summary>
	/// Получение урона.
	/// </summary>
	/// <param name="initiator"> Инициатор урона. </param>
	/// <param name="damage"> Урон. </param>
	public void ReceiveDamage(Character initiator, float damage)
	{
		Health -= damage;
		HealthDecreased?.Invoke();

		if (Health <= 0)
		{
			HealthRunOut?.Invoke();
		}
	}

	/// <summary>
	/// Получение лечения.
	/// </summary>
	/// <param name="initiator"> Инициатор лечения. </param>
	/// <param name="value"> Количество лечения. </param>
	public void ReceiveHeal(Character initiator, float value)
	{
		Health += value;

		if (Health > MaxHealth)
		{
			Health = MaxHealth;
		}

		HealthIncreased?.Invoke();
	}
	#endregion
}
