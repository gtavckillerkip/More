using System.Collections.Generic;
using System;
using UnityEngine;

namespace More.Basic.Management
{
	/// <summary>
	/// Менеджер заданий.
	/// </summary>
	public class TasksManager : SingletonMB<TasksManager>
	{
		#region Properties declarations
		/// <summary>
		/// Задания.
		/// </summary>
		public List<TaskBase> Tasks { get; private set; }
		#endregion

		/// <summary>
		/// Создание заданий.
		/// </summary>
		public void CreateTasks()
		{
			Tasks = new()
			{
				new EliminationTask("Eliminate 4 enemies", TasksHelper.TasksSetups.Level1.Task_1_setup, 4, 0),
				new CollectiveTask("Shoot 10 damage projectiles", TasksHelper.TasksSetups.Level1.Task_2_setup, 10, 0),
			};

			foreach (var task in Tasks)
			{
				task.Completed += TaskCompletedHandler;
			}
		}

		#region Events handlers
		public void TaskCompletedHandler(object sender, EventArgs e)
		{
			Debug.Log("task completed");
		}
		#endregion
	}
}
