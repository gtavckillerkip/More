using System;

/// <summary>
/// Класс задания относительно существенного.
/// </summary>
public abstract class SubstantialTask : TaskBase
{
	/// <summary>
	/// Целевое количество.
	/// </summary>
	public int GoalAmount { get; private set; }

	/// <summary>
	/// Текущее количество.
	/// </summary>
	public int CurrentAmount { get; private set; }

	/// <summary>
	/// Конструктор.
	/// </summary>
	/// <param name="message"> Текст задания. </param>
	/// <param name="setup"> Настройка условий выполнения задания. </param>
	/// <param name="goalAmount"> Целевое количество. </param>
	/// <param name="initialAmount"> Начальное количество. </param>
	public SubstantialTask(string message, Action<TaskBase> setup, int goalAmount, int initialAmount) : base(message, setup)
	{
		GoalAmount = goalAmount;
		CurrentAmount = initialAmount;
	}

	/// <summary>
	/// Изменить количество.
	/// </summary>
	/// <param name="change"> Величина. </param>
	public void ChangeAmount(int value)
	{
		if (!IsCompleted)
		{
			IsCompleted = (CurrentAmount += value) == GoalAmount;

			if (IsCompleted)
			{
				OnCompleted();
			}
		}
	}
}
