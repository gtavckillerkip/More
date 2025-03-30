using System;

/// <summary>
/// Класс заданий уничтожения.
/// </summary>
public class EliminationTask : SubstantialTask
{
	/// <summary>
	/// Конструктор.
	/// </summary>
	/// <param name="message"> Текст задания. </param>
	/// <param name="setup"> Настройка условий выполнения задания. </param>
	/// <param name="goalAmount"> Целевое количество. </param>
	/// <param name="initialAmount"> Начальное количество. </param>
	public EliminationTask(string message, Action<TaskBase> setup, int goalAmount, int initialAmount) : base(message, setup, goalAmount, initialAmount)
	{

	}
}
