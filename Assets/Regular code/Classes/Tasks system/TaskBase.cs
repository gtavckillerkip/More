using System;

/// <summary>
/// Базовый класс для заданий.
/// </summary>
public abstract class TaskBase
{
	/// <summary>
	/// Текст задания.
	/// </summary>
	public string Message { get; private set; }

	/// <summary>
	/// Завершено ли.
	/// </summary>
	public bool IsCompleted { get; protected set; }

	public event EventHandler Completed;

	/// <summary>
	/// Конструктор.
	/// </summary>
	/// <param name="message"> Текст задания. </param>
	/// <param name="setup"> Настройка условий выполнения задания. </param>
    public TaskBase(string message, Action<TaskBase> setup)
    {
        Message = message;
		IsCompleted = false;
		setup.Invoke(this);
    }

	#region Events invokers
	public void OnCompleted() => Completed?.Invoke(this, EventArgs.Empty);
	#endregion
}
