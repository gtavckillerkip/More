using System;
using System.Collections.Generic;

/// <summary>
/// Класс заданий с подзаданиями.
/// </summary>
public class MultiTask : TaskBase
{
	/// <summary>
	/// Подзадания.
	/// </summary>
	public List<TaskBase> SubTasks { get; private set; }

	/// <summary>
	/// Конструктор.
	/// </summary>
	/// <param name="message"> Текст задания. </param>
	/// <param name="setup"> Настройка условий выполнения задания. </param>
	/// <param name="subTasks"> Подзадания. </param>
	public MultiTask(string message, Action<TaskBase> setup, List<TaskBase> subTasks) : base(message, setup)
	{
		SubTasks = subTasks;
	}
}
