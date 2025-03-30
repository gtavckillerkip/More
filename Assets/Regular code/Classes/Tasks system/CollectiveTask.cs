using System;

/// <summary>
/// Класс заданий сбора.
/// </summary>
public class CollectiveTask : SubstantialTask
{
	/// <summary>
	/// Конструктор.
	/// </summary>
	/// <param name="message"> Текст задания. </param>
	/// <param name="setup"> Настройка условий выполнения задания. </param>
	/// <param name="goalAmount"> Целевое количество. </param>
	/// <param name="initialAmount"> Начальное количество. </param>
	public CollectiveTask(string message, Action<TaskBase> setup, int goalAmount, int initialAmount) : base(message, setup, goalAmount, initialAmount)
	{

	}
}
