using UnityEngine;

namespace More.UI
{
	public partial class PlayingBlockUI
	{
		#region Fields declarations
		private const string TASKS = "Tasks";

		/// <summary>
		/// Задания.
		/// </summary>
		private GameObject _tasks;
		#endregion

		/// <summary>
		/// Настройка заданий.
		/// </summary>
		private void SetupTasks()
		{
			_tasks = _content.transform.Find(TASKS).gameObject;

			_tasks.SetActive(true);
		}
	}
}
